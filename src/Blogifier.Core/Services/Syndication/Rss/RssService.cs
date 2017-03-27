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

namespace Blogifier.Core.Services.Syndication.Rss
{
	public class RssService : IRssService
    {
        IUnitOfWork _db;
        string _root;

        public RssService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task Import(AdminSyndicationModel model, string root)
        {
            var blog = _db.Blogs.Single(b => b.Id == model.PublisherId);
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
                    var post = new Publication
                    {
                        PublisherId = model.PublisherId,
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

                    await AddCategories(item, model.PublisherId);
                }
            }
            catch { }
        }

        IList<FeedItem> GetFeedItems(string url)
        {
            try
            {
                var client = new HttpClient();
                var doc = new XDocument();

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
                var x = ex.Message;
                return new List<FeedItem>();
            }
        }

        async Task ImportImages(Publication post, AdminSyndicationModel model, IBlogStorage storage)
        {
            var imgLinks = GetImages(post.Content);
            if (imgLinks != null && imgLinks.Count > 0)
            {
                foreach (var img in imgLinks)
                {
                    try
                    {
                        var uri = GetUri(img, model.Domain, model.SubDomain);
                        var asset = await storage.UploadFromWeb(uri, _root);
                        asset.PublisherId = post.PublisherId;
                        asset.LastUpdated = SystemClock.Now();

                        post.Content = post.Content.Replace(uri.ToString(), asset.Url);

						// System.Diagnostics.Debug.WriteLine("ASSET :: " + asset.Path + " :: " + asset.Url + " :: " + asset.Image);

                        _db.Assets.Add(asset);
                        _db.Complete();
                    }
                    catch { }
                }
            }
        }

        async Task ImportAttachements(Publication post, AdminSyndicationModel model, IBlogStorage storage)
        {
            var links = GetAttachements(post.Content);
            if (links.Any())
            {
                foreach (var link in links)
                {
                    try
                    {
                        var uri = GetUri(link, model.Domain, model.SubDomain);
                        var asset = await storage.UploadFromWeb(uri, _root);
                        asset.PublisherId = post.PublisherId;
                        asset.AssetType = AssetType.Attachment;
                        asset.LastUpdated = SystemClock.Now();
                        post.Content = post.Content.ReplaceIgnoreCase(uri.ToString(), asset.Url);

                        _db.Assets.Add(asset);
                        _db.Complete();
                    }
                    catch { }
                }
            }
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
                    var blogCategory = _db.Categories.Single(c => c.Title == cat && c.PublisherId == blogId);
                    if (blogCategory == null)
                    {
                        var newCat = new Category
                        {
                            PublisherId = blogId,
                            Title = cat,
                            Slug = cat.ToSlug()
                        };
                        _db.Categories.Add(newCat);
                        _db.Complete();

                        blogCategory = _db.Categories.Single(c => c.Title == cat && c.PublisherId == blogId);
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
