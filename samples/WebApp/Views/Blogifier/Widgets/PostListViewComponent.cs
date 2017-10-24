using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Views.Blogifier.Widgets
{
    [ViewComponent(Name = "PostList")]
    public class PostListViewComponent : ViewComponent
    {
        IUnitOfWork _db;

        public PostListViewComponent(IUnitOfWork db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(ListType listType, int count = 3)
        {
            IEnumerable<BlogPost> posts;
            switch (listType)
            {
                case ListType.FeaturedPosts:
                    posts = _db.BlogPosts.All()
                        .Where(p => p.Published > DateTime.MinValue && p.IsFeatured)
                        .OrderByDescending(p => p.Published)
                        .Take(count);
                    break;
                case ListType.RecentPosts:
                    posts = _db.BlogPosts.All()
                        .Where(p => p.Published > DateTime.MinValue)
                        .OrderByDescending(p => p.Published)
                        .Take(count);
                    break;
                default:
                    throw new ApplicationException("Unknown list type");
            }
            return View(posts);
        }
    }

    public enum ListType
    {
        FeaturedPosts,
        RecentPosts
    }
}