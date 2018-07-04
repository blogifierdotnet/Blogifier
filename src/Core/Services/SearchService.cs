using Core.Data;
using Core.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<PostItem>> Find(Pager pager, string term, string blogSlug = "");
    }

    public class SearchService : ISearchService
    {
        IUnitOfWork _db;
        UserManager<AppUser> _um;

        public SearchService(IUnitOfWork db, UserManager<AppUser> um)
        {
            _db = db;
            _um = um;
        }

        // search always returns only published posts
        // for a search term and optional blog slug
        public async Task<IEnumerable<PostItem>> Find(Pager pager, string term, string blogSlug = "")
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var results = new List<Result>();
            var list = new List<PostItem>();

            IEnumerable<BlogPost> posts;
            if (string.IsNullOrEmpty(blogSlug))
                posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue).ToList();
            else
                posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue && p.Slug == blogSlug).ToList();

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

        PostItem GetItem(BlogPost p)
        {
            return new PostItem
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                Description = p.Description,
                Content = p.Content,
                Published = p.Published
                //Author = (from usr in _um.Users
                //    where usr.Id == p.UserId
                //    select new AuthorItem
                //    {
                //        Id = usr.Id,
                //        UserName = usr.UserName,
                //        DisplayName = usr.DisplayName,
                //        Avatar = usr.Avatar,
                //        Created = usr.Created
                //    }).FirstOrDefault()
            };
        }
    }

    public class Result
    {
        public int Rank { get; set; }
        public PostItem Item { get; set; }
    }
}