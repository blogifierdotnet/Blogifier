using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Blogifier.Core.Views.Blogifier.Widgets
{
    [ViewComponent(Name = "Blogifier.Core.Views.Blogifier.Widgets.FeaturedPosts")]
	public class FeaturedPostsViewComponent : ViewComponent
	{
        IUnitOfWork _db;

        public FeaturedPostsViewComponent(IUnitOfWork db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
		{
            IEnumerable<PostListItem> posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue && p.IsFeatured, new Common.Pager(1));
            return View(posts);
		}
	}
}
