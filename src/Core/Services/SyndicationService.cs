using Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Core.Services
{
    public interface ISyndicationService
    {
        Task<IEnumerable<AtomEntry>> GetEntries(string type, string host);
        Task<ISyndicationFeedWriter> GetWriter(string type, string host, XmlWriter xmlWriter);

        Task ImportRss(IFormFile file, string user);
        Task ImportRss(string fileName, string user);
    }

    public class SyndicationService : ISyndicationService
    {
        IUnitOfWork _db;
        IStorageService _storage;
        string _user;
        string _siteUrl;

        public SyndicationService(IUnitOfWork db, IStorageService storage)
        {
            _db = db;
            _storage = storage;
        }

        public async Task<IEnumerable<AtomEntry>> GetEntries(string type, string host)
        {
            var items = new List<AtomEntry>();
            var posts = await _db.BlogPosts.Find(p => p.Published > DateTime.MinValue, new Pager(1));

            var user = _db.Authors.Find(a => a.IsAdmin).FirstOrDefault();

            foreach (var post in posts)
            {
                var item = new AtomEntry
                {
                    Title = post.Title,
                    Description = post.Content,
                    Id = $"{host}/blog/{post.Slug}",
                    Published = post.Published,
                    LastUpdated = post.Published,
                    ContentType = "html",
                };

                //foreach (string category in post.Categories)
                //{
                //    item.AddCategory(new SyndicationCategory(category));
                //}

                item.AddContributor(new SyndicationPerson(user.DisplayName, user.Email));
                item.AddLink(new SyndicationLink(new Uri(item.Id)));
                items.Add(item);
            }

            return await Task.FromResult(items);
        }

        public async Task<ISyndicationFeedWriter> GetWriter(string type, string host, XmlWriter xmlWriter)
        {
            var lastPost = _db.BlogPosts.All().OrderByDescending(p => p.Published).FirstOrDefault();

            if (lastPost == null)
                return null;

            if (type.Equals("rss", StringComparison.OrdinalIgnoreCase))
            {
                var rss = new RssFeedWriter(xmlWriter);
                await rss.WriteTitle(AppSettings.Title);
                await rss.WriteDescription(AppSettings.Description);
                await rss.WriteGenerator("Blogifier");
                await rss.WriteValue("link", host);
                return rss;
            }

            var atom = new AtomFeedWriter(xmlWriter);
            await atom.WriteTitle(AppSettings.Title);
            await atom.WriteId(host);
            await atom.WriteSubtitle(AppSettings.Description);
            await atom.WriteGenerator("Blogifier", "https://github.com/blogifierdotnet/Blogifier", "1.0");
            await atom.WriteValue("updated", lastPost.Published.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            return atom;
        }

        #region RSS Import

        public async Task ImportRss(string fileName, string user)
        {
            _user = user;
            var reader = new StreamReader(fileName, Encoding.UTF8);
            await Import(reader);
        }

        public async Task ImportRss(IFormFile file, string user)
        {
            _user = user;
            var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
            await Import(reader);
        }

        async Task Import(StreamReader reader)
        {
            using (var xmlReader = XmlReader.Create(reader, new XmlReaderSettings() { }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == SyndicationElementType.Link)
                    {
                        var link = await feedReader.ReadLink();
                        _siteUrl = link.Uri.ToString();
                    }

                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        try
                        {
                            var item = await feedReader.ReadItem();

                            PostItem post = new PostItem
                            {
                                Author = await _db.Authors.GetItem(a => a.UserName == _user),
                                Title = item.Title,
                                Description = item.Title,
                                Content = item.Description,
                                Slug = await GetSlug(item.Title),
                                Published = item.Published.DateTime,
                                Status = SaveStatus.Publishing
                            };

                            await ImportImages(post);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        async Task ImportImages(PostItem post)
        {
            var images = GetImages(post.Content);
            await SaveAssets(images, post, false);

            var converter = new ReverseMarkdown.Converter();
            post.Content = converter.Convert(post.Content);

            await _db.BlogPosts.SaveItem(post);
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
                        links.Add(href);
                    }
                    catch { }
                }
            }
            return links;
        }

        async Task SaveAssets(IList<string> assets, PostItem post, bool isAttachement)
        {
            if (assets.Any())
            {
                foreach (var item in assets)
                {
                    var uri = "";
                    var webRoot = "/";
                    try
                    {
                        uri = ValidateUrl(item);

                        var path = string.Format("{0}/{1}", post.Published.Year, post.Published.Month);

                        AssetItem asset;
                        if (uri.Contains("data:image"))
                        {
                            asset = await _storage.UploadBase64Image(uri, webRoot, path);
                        }
                        else
                        {
                            asset = await _storage.UploadFromWeb(new Uri(uri), webRoot, path);
                        }

                        post.Content = post.Content.ReplaceIgnoreCase(uri.ToString(), asset.Url);

                        _db.Complete();
                    }
                    catch (Exception)
                    {
                        //_logger.LogError(string.Format("Error importing [{0}] : {1}", item, ex.Message));
                    }
                }
            }
        }

        async Task<string> GetSlug(string title)
        {
            string slug = title.ToSlug();
            BlogPost post;

            post = _db.BlogPosts.Single(p => p.Slug == slug);

            if (post == null)
                return await Task.FromResult(slug);

            for (int i = 2; i < 100; i++)
            {
                post = _db.BlogPosts.Single(p => p.Slug == $"{slug}{i}");

                if (post == null)
                {
                    return await Task.FromResult(slug + i.ToString());
                }
            }

            return await Task.FromResult(slug);
        }

        string ValidateUrl(string link)
        {
            var url = link;
            var domain = "";
            if (url.StartsWith("~"))
            {
                url = url.Replace("~", domain);
            }
            if (url.StartsWith("/"))
            {
                url = string.Concat(domain, url);
            }
            return url;
        }

        #endregion
    }
}
