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
using System.Threading.Tasks;
using System.Xml;

namespace Core.Services
{
    public interface ISyndication
    {
        Task<IEnumerable<AtomEntry>> GetEntries(string type, string host);
        Task<ISyndicationFeedWriter> GetWriter(string type, string host, XmlWriter xmlWriter);
        Task RssImport(IFormFile file, string userId);
    }

    public class SyndicationService : ISyndication
    {
        IUnitOfWork _db;

        public SyndicationService(IUnitOfWork db)
        {
            _db = db;
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

        public async Task RssImport(IFormFile file, string userId)
        {
            var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
            var converter = new ReverseMarkdown.Converter();

            using (var xmlReader = XmlReader.Create(reader, new XmlReaderSettings() {  }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    if(feedReader.ElementType == SyndicationElementType.Item)
                    {
                        try
                        {
                            var item = await feedReader.ReadItem();

                            PostItem post = new PostItem
                            {
                                Author = await _db.Authors.GetItem(a => a.Id == userId),
                                Content = converter.Convert(item.Description),
                                Title = item.Title,
                                Slug = await GetSlug(item.Title),
                                Published = item.Published.DateTime,
                                Description = item.Title,
                                Status = SaveStatus.Publishing
                            };

                            await _db.BlogPosts.SaveItem(post);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                        
                    }
                }
            }
        }

        public async Task<string> GetSlug(string title)
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
    }
}
