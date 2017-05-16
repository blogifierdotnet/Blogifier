using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
    [Route("blog")]
	public class BlogController : Controller
	{
		IUnitOfWork _db;
        IRssService _rss;
        private readonly ILogger _logger;
        private readonly string _themePattern = "~/Views/Blogifier/Blog/{0}/";
        private readonly string _theme;

		public BlogController(IUnitOfWork db, IRssService rss, ILogger<BlogController> logger)
		{
			_db = db;
            _rss = rss;
            _logger = logger;
			_theme = string.Format(_themePattern, Common.ApplicationSettings.BlogTheme);
        }

		public IActionResult Index()
		{
            var pager = new Pager(1);
            var posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue, pager);
            return View(_theme + "Index.cshtml", new BlogPostsModel { Posts = posts, Pager = pager });
        }

        [Route("{page:int}")]
        public IActionResult Index(int page)
        {
            var pager = new Pager(page);
            var posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Index.cshtml", new BlogPostsModel { Posts = posts, Pager = pager });
        }

        [Route("{slug}")]
        public async Task<IActionResult> SinglePublication(string slug)
        {
            var vm = new BlogPostDetailModel();
            vm.BlogPost = await _db.BlogPosts.SingleIncluded(p => p.Slug == slug);

            if (vm.BlogPost == null)
                return View("Error");

            vm.Profile = _db.Profiles.Single(b => b.Id == vm.BlogPost.ProfileId);
            vm.Categories = new List<SelectListItem>();

            if (vm.BlogPost.PostCategories != null && vm.BlogPost.PostCategories.Count > 0)
            {
                foreach (var pc in vm.BlogPost.PostCategories)
                {
                    var cat = _db.Categories.Single(c => c.Id == pc.CategoryId);
                    vm.Categories.Add(new SelectListItem { Value = cat.Slug, Text = cat.Title });
                }
            }
            return View("~/Views/Blogifier/Blog/" + vm.Profile.BlogTheme + "/Single.cshtml", vm);
        }

        [Route("rss")]
        public IActionResult Rss()
        {
            var absoluteUri = string.Concat(
                Request.Scheme, "://",
                Request.Host.ToUriComponent(),
                Request.PathBase.ToUriComponent());

            var rss = _rss.Display(absoluteUri);
            return Content(rss, "text/xml");
        }

        [Route("error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            return View(_theme + "Error.cshtml", statusCode);
        }
    }
}
