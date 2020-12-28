using Blogifier.Models;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services
{
	public interface IFeedService
    {
        Task<IEnumerable<AtomEntry>> GetEntries(string type, string host, int count);
        Task<AtomEntry> GetEntry(PostItem post, string host);
    }

    public class FeedService : IFeedService
    {
        IDataService _db;

        public FeedService(IDataService db, IStorageService storage)
        {
            _db = db;
        }

        public async Task<IEnumerable<AtomEntry>> GetEntries(string type, string host, int count)
        {
            var items = new List<AtomEntry>();
            var posts = await _db.BlogPosts.GetList(count);

            foreach (var post in posts)
            {
                items.Add(await GetEntry(post, host));
            }

            return await Task.FromResult(items);
        }

        public async Task<AtomEntry> GetEntry(PostItem post, string host)
        {
            var item = new AtomEntry
            {
               Title = post.Title,
               Description = post.Content,
               Id = $"{host}posts/{post.Slug}",
               Published = post.Published,
               LastUpdated = post.Published,
               ContentType = "html",
               Summary = post.Description
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

            return await Task.FromResult(item);
        }
    }
}