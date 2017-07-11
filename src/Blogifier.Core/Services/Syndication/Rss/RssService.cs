using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Services.FileSystem;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blogifier.Core.Services.Syndication.Rss
{
    public class RssService : IRssService
    {
        IUnitOfWork _db;
        RssImportModel _model;
        string _root;
        private readonly AppLogger _logger;

        public RssService(IUnitOfWork db, ILogger<RssService> logger)
        {
            _db = db;
            _logger = new AppLogger(logger);
        }

        public async Task<HttpResponseMessage> Import(RssImportModel model, string root)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            _model = model;
            _root = root;

            if(model == null || string.IsNullOrEmpty(model.FeedUrl))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = "RSS feed URL is required";
                return response;
            }

            var blog = _db.Profiles.Single(b => b.Id == model.ProfileId);
            if (blog == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = Constants.ProfileNotFound;
                return response;
            }

            try
            {
                var storage = new BlogStorage(blog.Slug);
                var items = GetFeedItems(model.FeedUrl);

                _logger.LogInformation(string.Format("Start importing {0} posts", items.Count));

                foreach (var item in items)
                {
                    var content = item.Body.Length > item.Description.Length ? item.Body : item.Description;

                    var desc = content.StripHtml();
                    if (desc.Length > 300)
                        desc = desc.Substring(0, 300);

                    var post = new BlogPost
                    {
                        ProfileId = model.ProfileId,
                        Title = item.Title,
                        Slug = item.Title.ToSlug(),
                        Description = desc,
                        Content = content,
                        Published = item.PublishDate
                    };
                    if (model.ImportImages)
                    {
                        await ImportImages(post, storage);
                    }
                    if (model.ImportAttachements)
                    {
                        await ImportAttachements(post, storage);
                    }
                    _db.BlogPosts.Add(post);
                    _db.Complete();
                    _logger.LogInformation(string.Format("RSS item added : {0}", item.Title));

                    await AddCategories(item, model.ProfileId);
                }
                response.ReasonPhrase = string.Format("Imported {0} blog posts", items.Count);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(string.Format("Error importing RSS : {0}", ex.Message));

                response.StatusCode = HttpStatusCode.BadRequest;
                response.ReasonPhrase = ex.Message;
                return response;
            }
        }

        public string Display(string absoluteUri)
        {
            var pubs = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue && p.Published < DateTime.UtcNow, new Pager(1));

            var feed = new Feed()
            {
                Title = ApplicationSettings.Title,
                Description = ApplicationSettings.Description,
                Link = new Uri(absoluteUri + "/rss"),
                Copyright = "(c) " + DateTime.Now.Year
            };

            foreach (var post in pubs)
            {
                var postDetails = _db.BlogPosts.Single(p => p.Slug == post.Slug);

                var item = new FeedItem()
                {
                    Title = post.Title,
                    Body = postDetails.Content.Replace("src=\"blogifier/data/", "src=\"" + absoluteUri + "/blogifier/data/"),
                    Link = new Uri(absoluteUri + "/blog/" + post.Slug),
                    Permalink = absoluteUri + "/blog/" + post.Slug,
                    PublishDate = post.Published,
                    Author = new Author() { Name = post.AuthorName, Email = post.AuthorEmail }
                };

                if (post.Categories != null && post.Categories.Count > 0)
                {
                    foreach (var cat in post.Categories)
                    {
                        item.Categories.Add(cat.Value);
                    }
                }

                item.Comments = new Uri(absoluteUri + "/blog/" + post.Slug);
                feed.Items.Add(item);
            }

            return feed.Serialize();
        }

        IList<FeedItem> GetFeedItems(string url)
        {
            var client = new HttpClient();
            var doc = new XDocument();

            _logger.LogInformation("Importing RSS from feed: " + url);

            if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                var stream = client.GetStreamAsync(url);
                doc = XDocument.Load(stream.Result);
            }
            else
            {
                doc = XDocument.Load(url);
            }
            var ns = XNamespace.Get(@"http://purl.org/rss/1.0/modules/content/");
            var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel")
                .Elements().Where(i => i.Name.LocalName == "item")
                select new FeedItem
                {
                    Description = item.Element("description").Value,
                    Body = item.Element(ns + "encoded") == null ? "" : item.Element(ns + "encoded").Value,
                    Link = new Uri(item.Element("link").Value),
                    PublishDate = SystemClock.RssPubishedToDateTime(item.Element("pubDate").Value),
                    Title = item.Element("title").Value,
                    Categories = (from category in item.Elements("category") select category.Value).ToList()
                };
            return entries.ToList();
        }

        async Task ImportImages(BlogPost post, IBlogStorage storage)
        {
            var images = GetImages(post.Content);
            await AddAssets(images, post, storage, false);
        }

        async Task ImportAttachements(BlogPost post, IBlogStorage storage)
        {
            var attachements = GetAttachements(post.Content);
            await AddAssets(attachements, post, storage, true);
        }

        IList<string> GetImages(string html)
        {
            var links = new List<string>();
            string rgx = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";

            if (string.IsNullOrEmpty(html))
                return links;

            MatchCollection matchesImgSrc = Regex.Matches(html, rgx, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (matchesImgSrc != null)
            {
                foreach (Match m in matchesImgSrc)
                {
                    links.Add(m.Groups[1].Value);
                }
            }
            return links;
        }

        IList<string> GetAttachements(string html)
        {
            var links = new List<string>();
            string rgx = "<(a|link).*?href=(\"|')(.+?)(\"|').*?>";

            if (string.IsNullOrEmpty(html))
                return links;

            MatchCollection matches = Regex.Matches(html, rgx, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (matches != null)
            {
                foreach (Match m in matches)
                {
                    try
                    {
                        var link = m.Value.Replace("\">", "\"/>").ToLower();
                        var href = XElement.Parse(link).Attribute("href").Value;

                        if (ValidFileType(href))
                        {
                            links.Add(href);
                        }
                    }
                    catch { }
                }
            }
            return links;
        }

        async Task AddAssets(IList<string> assets, BlogPost post, IBlogStorage storage, bool isAttachement)
        {
            if (assets.Any())
            {
                foreach (var item in assets)
                {
                    try
                    {
                        var uri = GetUri(item, _model.Domain, _model.SubDomain);
                        var path = string.Format("{0}/{1}", post.Published.Year, post.Published.Month);

                        var asset = await storage.UploadFromWeb(uri, _root, path);
                        asset.ProfileId = post.ProfileId;
                        asset.LastUpdated = SystemClock.Now();

                        if(isAttachement)
                            asset.AssetType = AssetType.Attachment;

                        post.Content = post.Content.ReplaceIgnoreCase(uri.ToString(), asset.Url);

                        _db.Assets.Add(asset);
                        _db.Complete();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(string.Format("Error importing {0} : {1}", item, ex.Message));
                    }
                }
            }
        }

        Uri GetUri(string link, string domain, string subdomain)
        {
            var url = link;
            if (url.StartsWith("~"))
            {
                url = url.Replace("~", subdomain);
                url = string.Concat(domain, "/", url);
            }
            if (url.StartsWith("/") && !string.IsNullOrEmpty(domain))
            {
                url = string.Concat(domain, url);
            }
            return new Uri(url);
        }

        async Task AddCategories(FeedItem item, int blogId)
        {
            if (item.Categories != null && item.Categories.Count > 0)
            {
                var catIds = new List<string>();

                foreach (var cat in item.Categories)
                {
                    var blogCategory = _db.Categories.Single(c => c.Title == cat && c.ProfileId == blogId);
                    if (blogCategory == null)
                    {
                        var newCat = new Category
                        {
                            ProfileId = blogId,
                            Title = cat,
                            Slug = cat.ToSlug()
                        };
                        _db.Categories.Add(newCat);
                        _db.Complete();

                        blogCategory = _db.Categories.Single(c => c.Title == cat && c.ProfileId == blogId);
                    }
                    catIds.Add(blogCategory.Id.ToString());
                }
                var blogPost = _db.BlogPosts.Single(p => p.Slug == item.Title.ToSlug());
                await _db.BlogPosts.UpdatePostCategories(blogPost.Id, catIds);
            }
        }

        bool ValidFileType(string file)
        {
            var extentions = ApplicationSettings.SupportedStorageFiles.Split(',').ToList();
            foreach (var ext in extentions)
            {
                if (file.EndsWith("." + ext, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
