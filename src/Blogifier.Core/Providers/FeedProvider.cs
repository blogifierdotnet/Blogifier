using Blogifier.Shared;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IFeedProvider
	{
      Task<IEnumerable<AtomEntry>> GetEntries(string type, string host);
   }

	public class FeedProvider : IFeedProvider
	{
      protected readonly IPostProvider _postProvider;

		public FeedProvider(IPostProvider postProvider)
		{
         _postProvider = postProvider;
		}

      public async Task<IEnumerable<AtomEntry>> GetEntries(string type, string host)
      {
         var items = new List<AtomEntry>();
         var posts = await _postProvider.GetList(new Pager(1)); // _db.BlogPosts.GetList(p => p.Published > DateTime.MinValue, new Pager(1));

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

            //if (!string.IsNullOrEmpty(post.Categories))
            //{
            //   foreach (string category in post.Categories.Split(','))
            //   {
            //      item.AddCategory(new SyndicationCategory("category"));
            //   }
            //}

            item.AddContributor(new SyndicationPerson(post.Author.Email, post.Author.DisplayName));
            item.AddLink(new SyndicationLink(new Uri(item.Id)));
            items.Add(item);
         }

         return await Task.FromResult(items);
      }
   }
}
