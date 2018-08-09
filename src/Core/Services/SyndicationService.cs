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
    public interface ISyndicationService
    {
        Task<IEnumerable<AtomEntry>> GetEntries(string type, string host);
        Task<ISyndicationFeedWriter> GetWriter(string type, string host, XmlWriter xmlWriter);
    }

    public class SyndicationService : ISyndicationService
    {
        IDataService _db;

        public SyndicationService(IDataService db, IStorageService storage)
        {
            _db = db;
        }

        public async Task<IEnumerable<AtomEntry>> GetEntries(string type, string host)
        {
            var items = new List<AtomEntry>();
            var posts = await _db.BlogPosts.GetList(p => p.Published > DateTime.MinValue, new Pager(1));

            //var user = _db.Authors.Find(a => a.IsAdmin).FirstOrDefault();

            //foreach (var post in posts)
            //{
            //    var item = new AtomEntry
            //    {
            //        Title = post.Title,
            //        Description = post.Content,
            //        Id = $"{host}/blog/{post.Slug}",
            //        Published = post.Published,
            //        LastUpdated = post.Published,
            //        ContentType = "html",
            //    };

            //    //foreach (string category in post.Categories)
            //    //{
            //    //    item.AddCategory(new SyndicationCategory(category));
            //    //}

            //    item.AddContributor(new SyndicationPerson(user.DisplayName, user.Email));
            //    item.AddLink(new SyndicationLink(new Uri(item.Id)));
            //    items.Add(item);
            //}

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
    }
}