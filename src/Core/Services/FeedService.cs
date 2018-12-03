using Core.Helpers;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Core.Services
{
    public interface IFeedService
    {
        Task<IEnumerable<AtomEntry>> GetEntries(string type, string host);
        Task<ISyndicationFeedWriter> GetWriter(string type, string host, XmlWriter xmlWriter);
    }

    public class FeedService : IFeedService
    {
        IDataService _db;

        public FeedService(IDataService db, IStorageService storage)
        {
            _db = db;
        }

        public async Task<IEnumerable<AtomEntry>> GetEntries(string type, string host)
        {
            var items = new List<AtomEntry>();
            var posts = await _db.BlogPosts.GetList(p => p.Published > DateTime.MinValue, new Pager(1));

            foreach (var post in posts)
            {
                var item = new AtomEntry
                {
                    Title = post.Title,
                    Description = post.Content,
                    Id = $"{host}/posts/{post.Slug}",
                    Published = post.Published,
                    LastUpdated = post.Published,
                    ContentType = "html",
                };

                if (!string.IsNullOrEmpty(post.Categories))
                {
                    foreach (string category in post.Categories.Split(','))
                    {
                        item.AddCategory(new SyndicationCategory(category));
                    }
                }

                item.AddContributor(new SyndicationPerson(post.Author.DisplayName, post.Author.Email));
                item.AddLink(new SyndicationLink(new Uri(item.Id)));
                items.Add(item);
            }

            return await Task.FromResult(items);
        }

        public async Task<ISyndicationFeedWriter> GetWriter(string type, string host, XmlWriter xmlWriter)
        {
            var lastPost = _db.BlogPosts.All().OrderByDescending(p => p.Published).FirstOrDefault();
            var blog = await _db.CustomFields.GetBlogSettings();

            if (lastPost == null)
                return null;

            if (type.Equals("rss", StringComparison.OrdinalIgnoreCase))
            {
                var rss = new RssFeedWriter(xmlWriter);
                await rss.WriteTitle(blog.Title);
                await rss.WriteDescription(blog.Description);
                await rss.WriteGenerator("Blogifier");
                await rss.WriteValue("link", host);
                return rss;
            }

            var atom = new AtomFeedWriter(xmlWriter);
            await atom.WriteTitle(blog.Title);
            await atom.WriteId(host);
            await atom.WriteSubtitle(blog.Description);
            await atom.WriteGenerator("Blogifier", "https://github.com/blogifierdotnet/Blogifier", "1.0");
            await atom.WriteValue("updated", lastPost.Published.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            return atom;
        }
    }
}