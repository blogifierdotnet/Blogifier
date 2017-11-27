using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Search
{
    public class SearchService : ISearchService
    {
        IUnitOfWork _db;

        public SearchService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<List<PostListItem>> Find(Pager pager, string term, string blogSlug = "")
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var results = new List<Result>();
            var list = new List<PostListItem>();

            IEnumerable<BlogPost> posts;
            if(string.IsNullOrEmpty(blogSlug))
                posts = _db.BlogPosts.AllIncluded(p => p.Published > DateTime.MinValue).ToList();
            else
                posts = _db.BlogPosts.AllIncluded(p => p.Published > DateTime.MinValue && p.Profile.Slug == blogSlug).ToList();

            foreach (var item in posts)
            {
                var rank = 0;
                var hits = 0;
                term = term.ToLower();

                if (item.Title.ToLower().Contains(term))
                {
                    hits = Regex.Matches(item.Title.ToLower(), term).Count;
                    rank += hits * 10;
                }
                if (item.Description.ToLower().Contains(term))
                {
                    hits = Regex.Matches(item.Description.ToLower(), term).Count;
                    rank += hits * 3;
                }
                if (item.Content.ToLower().Contains(term))
                {
                    rank += Regex.Matches(item.Content.ToLower(), term).Count;
                }

                if (rank > 0)
                {
                    results.Add(new Result { Rank = rank, Item = GetItem(item) });
                }
            }
            results = results.OrderByDescending(r => r.Rank).ToList();
            for (int i = 0; i < results.Count; i++)
            {
                list.Add(results[i].Item);
            }
            pager.Configure(list.Count);
            return await Task.Run(() => list.Skip(skip).Take(pager.ItemsPerPage).ToList());
        }

        #region Private methods

        private PostListItem GetItem(BlogPost post)
        {
            var item = new PostListItem
            {
                BlogPostId = post.Id,
                Slug = post.Slug,
                Title = post.Title,
                Image = string.IsNullOrEmpty(post.Image) ? BlogSettings.PostCover : post.Image,
                Content = post.Description,
                Published = post.Published,
                AuthorName = post.Profile.AuthorName,
                AuthorEmail = post.Profile.AuthorEmail,
                BlogSlug = post.Profile.Slug,
                PostViews = post.PostViews
            };
            return item;
        }

        #endregion
    }

    public class Result
    {
        public int Rank { get; set; }
        public PostListItem Item { get; set; }
    }
}
