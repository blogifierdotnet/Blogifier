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

            var posts = _db.BlogPosts.All().Where(p => p.Published > DateTime.MinValue).ToList();

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
                //if (i >= skip && i <= skip + pager.ItemsPerPage)
                //{
                    list.Add(results[i].Item);
                //}
            }
            pager.Configure(list.Count);
            return await Task.Run(() => list.Skip(skip).Take(pager.ItemsPerPage).ToList());
        }

        #region Private methods

        private List<PostListItem> GetItems(List<BlogPost> postList)
        {
            var posts = new List<PostListItem>();
            foreach (var p in postList)
            {
                posts.Add(GetItem(p));
            }
            return posts;
        }
        private PostListItem GetItem(BlogPost post)
        {
            var item = new PostListItem
            {
                BlogPostId = post.Id,
                Slug = post.Slug,
                Title = post.Title,
                Image = post.Image,
                Content = post.Description,
                Published = post.Published,
                AuthorName = post.Profile.AuthorName,
                AuthorEmail = post.Profile.AuthorEmail,
                BlogSlug = post.Profile.Slug,
                PostViews = post.PostViews,
                Categories = new List<SelectListItem>()
            };
            item.Categories = GetCategories(post);
            return item;
        }
        private List<SelectListItem> GetCategories(BlogPost post)
        {
            var catList = new List<SelectListItem>();
            if (post.PostCategories != null && post.PostCategories.Count > 0)
            {
                foreach (var pc in post.PostCategories)
                {
                    var cat = _db.Categories.Single(c => c.Id == pc.CategoryId);
                    catList.Add(new SelectListItem { Value = cat.Slug, Text = cat.Title });
                }
            }
            return catList;
        }

        #endregion
    }

    public class Result
    {
        public int Rank { get; set; }
        public PostListItem Item { get; set; }
    }
}
