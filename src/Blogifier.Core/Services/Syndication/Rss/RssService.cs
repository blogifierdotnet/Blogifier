using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Services.FileSystem;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Services.Syndication.Rss
{
	public class RssService : IRssService
    {
        IUnitOfWork _db;
        string _root;
        private readonly AppLogger _logger;

        public RssService(IUnitOfWork db, ILogger<RssService> logger)
        {
            _db = db;
            _logger = new AppLogger(logger);
        }

        public async Task Import(AdminSyndicationModel model, string root)
        {
            var blog = _db.Profiles.Single(b => b.Id == model.ProfileId);
            if (blog == null)
                return;

            _root = root;

            var storage = new BlogStorage(blog.Slug);
            var items = GetFeedItems(model.FeedUrl);
            try
            {
                foreach (var item in items)
                {
                    var desc = item.Body.StripHtml();
                    if (desc.Length > 300)
                    {
                        desc = desc.Substring(0, 300);
                    }
                    var post = new BlogPost
                    {
                        ProfileId = model.ProfileId,
                        Title = item.Title,
                        Slug = item.Title.ToSlug(),
                        Description = desc,
                        Content = item.Body,
                        Published = item.PublishDate
                    };
                    if (model.ImportImages)
                    {
                        await ImportImages(post, model, storage);
                    }
                    if (model.ImportAttachements)
                    {
                        await ImportAttachements(post, model, storage);
                    }
                    _db.Posts.Add(post);
                    _db.Complete();
                    _logger.LogWarning(string.Format("RSS item added : {0}", item.Title));

                    await AddCategories(item, model.ProfileId);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(string.Format("Error importing RSS : {0}", ex.Message));
            }
        }

        IList<FeedItem> GetFeedItems(string url)
        {
            try
            {
                var client = new HttpClient();
                var doc = new XDocument();

                _logger.LogWarning("Importing RSS from feed: " + url);

                if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    var stream = client.GetStreamAsync(url);
                    doc = XDocument.Load(stream.Result);
                }
                else
                {
                    doc = XDocument.Load(url);
                }

                // RSS/Channel/item
                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                    select new FeedItem
                    {
                        Body = item.Elements().First(i => i.Name.LocalName == "description").Value,
                        Link = new Uri(item.Elements().First(i => i.Name.LocalName == "link").Value),
                        PublishDate = SystemClock.RssPubishedToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                        Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                        Categories = (from category in item.Elements("category") select category.Value).ToList()
                    };
                return entries.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error importing RSS feed {0} : {1}", url, ex.Message));
                return new List<FeedItem>();
            }
        }

        async Task ImportImages(BlogPost post, AdminSyndicationModel model, IBlogStorage storage)
        {
            var images = GetImages(post.Content);
            await AddAssets(images, post, storage, model, false);
        }

        async Task ImportAttachements(BlogPost post, AdminSyndicationModel model, IBlogStorage storage)
        {
            var attachements = GetAttachements(post.Content);
            await AddAssets(attachements, post, storage, model, true);
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

        async Task AddAssets(IList<string> assets, BlogPost post, IBlogStorage storage, AdminSyndicationModel model, bool isAttachement)
        {
            if (assets.Any())
            {
                foreach (var item in assets)
                {
                    try
                    {
                        var uri = GetUri(item, model.Domain, model.SubDomain);
                        var path = string.Format("{0}\\{1}", post.Published.Year, post.Published.Month);

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
                var blogPost = _db.Posts.Single(p => p.Slug == item.Title.ToSlug());
                await _db.Posts.UpdatePostCategories(blogPost.Id, catIds);
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
