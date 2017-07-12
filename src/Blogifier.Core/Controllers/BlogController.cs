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
			_theme = string.Format(_themePattern, ApplicationSettings.BlogTheme);
        }

        [Route("{page:int?}")]
        public IActionResult Index(int page = 1)
        {
            var pager = new Pager(page);
            var posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue, pager);
            var categories = _db.Categories.All().OrderBy(c => c.Title).Take(10).ToList()
                .GroupBy(c => c.Title).Select(group => group.First())
                .Select(c => new SelectListItem { Text = c.Title, Value = c.Slug }).ToList();

            if (page < 1 || page > pager.LastPage)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Index.cshtml", new BlogPostsModel { Categories = categories, Posts = posts, Pager = pager });
        }

        [Route("{slug}")]
        public async Task<IActionResult> SinglePublication(string slug)
        {
            var vm = new BlogPostDetailModel();
            vm.BlogPost = await _db.BlogPosts.SingleIncluded(p => p.Slug == slug && p.Published > DateTime.MinValue);

            if (vm.BlogPost == null)
                return View(_theme + "Error.cshtml", 404);

            vm.Profile = _db.Profiles.Single(b => b.Id == vm.BlogPost.ProfileId);
            vm.BlogCategories = new List<SelectListItem>();

            if (vm.BlogPost.PostCategories != null && vm.BlogPost.PostCategories.Count > 0)
            {
                foreach (var pc in vm.BlogPost.PostCategories)
                {
                    var cat = _db.Categories.Single(c => c.Id == pc.CategoryId);
                    vm.BlogCategories.Add(new SelectListItem { Value = cat.Slug, Text = cat.Title });
                }
            }

            vm.Categories = _db.Categories.Find(c => c.ProfileId == vm.Profile.Id).OrderBy(c => c.Title).Take(10).ToList()
                .GroupBy(c => c.Title).Select(group => group.First())
                .Select(c => new SelectListItem { Text = c.Title, Value = c.Slug }).Distinct().ToList();

            vm.DisqusScript = _db.CustomFields.Single(f => 
                f.ParentId == vm.Profile.Id && 
                f.CustomKey == "disqus" && 
                f.CustomType == Data.Domain.CustomType.Profile);

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
