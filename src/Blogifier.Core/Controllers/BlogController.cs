using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Custom;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
	public class BlogController : Controller
	{
		IUnitOfWork _db;
        IRssService _rss;
        ISearchService _search;
        ICustomService _custom;
        private readonly ILogger _logger;
        private readonly string _themePattern = "~/Views/Blogifier/Blog/{0}/";
        private readonly string _theme;

		public BlogController(IUnitOfWork db, ISearchService search, IRssService rss, ICustomService custom, ILogger<BlogController> logger)
		{
			_db = db;
            _search = search;
            _rss = rss;
            _custom = custom;
            _logger = logger;
			_theme = string.Format(_themePattern, ApplicationSettings.BlogTheme);
        }

        [Route("{page:int?}")]
        public IActionResult Index(int page = 1)
        {
            var pager = new Pager(page);
            var posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Index.cshtml", new BlogPostsModel {
                CustomFields = new Dictionary<string, string>(), Posts = posts, Pager = pager });
        }

        [Route("{slug:author}/{page:int?}")]
        public IActionResult Author(string slug, int page = 1)
        {
            var pager = new Pager(page);
            var profile = _db.Profiles.Single(p => p.Slug == slug);
            var posts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id && p.Published > DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return View(_theme + "Error.cshtml", 404);

            var cf = _custom.GetProfileCustomFields(profile).Result;

            return View("~/Views/Blogifier/Blog/" + profile.BlogTheme + "/Author.cshtml", 
                new BlogAuthorModel { CustomFields = cf, Profile = profile, Posts = posts, Pager = pager });
        }

        [Route("{slug:author}/{cat}/{page:int?}")]
        public async Task<IActionResult> AuthorCategory(string slug, string cat, int page = 1)
        {
            var pager = new Pager(page);
            var profile = _db.Profiles.Single(p => p.Slug == slug);
            var posts = await _db.BlogPosts.ByCategory(cat, pager);

            if (page < 1 || page > pager.LastPage)
                return View(_theme + "Error.cshtml", 404);

            var category = _db.Categories.Single(c => c.Slug == cat && c.ProfileId == profile.Id);
            var custom = _custom.GetProfileCustomFields(profile).Result;

            return View("~/Views/Blogifier/Blog/" + profile.BlogTheme + "/Category.cshtml", 
                new BlogCategoryModel
                {
                    Profile = profile,
                    CustomFields = custom,
                    Category = category,
                    Posts = posts,
                    Pager = pager
                });
        }

        [Route("{slug}")]
        public async Task<IActionResult> SinglePublication(string slug)
        {
            var vm = new BlogPostDetailModel();
            vm.BlogPost = await _db.BlogPosts.SingleIncluded(p => p.Slug == slug && p.Published > DateTime.MinValue);

            if (vm.BlogPost == null)
                return View(_theme + "Error.cshtml", 404);

            vm.Profile = _db.Profiles.Single(b => b.Id == vm.BlogPost.ProfileId);

            if (string.IsNullOrEmpty(vm.BlogPost.Image))
            {
                vm.BlogPost.Image = ApplicationSettings.ProfileImage;
                if (!string.IsNullOrEmpty(ApplicationSettings.PostImage)) { vm.BlogPost.Image = ApplicationSettings.PostImage; }
                if (!string.IsNullOrEmpty(vm.Profile.Image)) { vm.BlogPost.Image = vm.Profile.Image; }
            }
            
            vm.BlogCategories = new List<SelectListItem>();
            if (vm.BlogPost.PostCategories != null && vm.BlogPost.PostCategories.Count > 0)
            {
                foreach (var pc in vm.BlogPost.PostCategories)
                {
                    var cat = _db.Categories.Single(c => c.Id == pc.CategoryId);
                    vm.BlogCategories.Add(new SelectListItem { Value = cat.Slug, Text = cat.Title });
                }
            }
            vm.CustomFields = _custom.GetProfileCustomFields(vm.Profile).Result;

            return View("~/Views/Blogifier/Blog/" + vm.Profile.BlogTheme + "/Single.cshtml", vm);
        }

        [Route("search/{term}/{page:int?}")]
        public async Task<IActionResult> PagedSearch(string term, int page = 1)
        {
            ViewBag.Term = term;

            var model = new BlogPostsModel();
            model.Pager = new Pager(page);
            model.Posts = await _search.Find(model.Pager, ViewBag.Term);
            model.CustomFields = _custom.GetProfileCustomFields(null).Result;

            if (page < 1 || page > model.Pager.LastPage)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Search.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Search()
        {
            ViewBag.Term = HttpContext.Request.Form["term"];

            var model = new BlogPostsModel();
            model.Pager = new Pager(1);
            model.Posts = await _search.Find(model.Pager, ViewBag.Term);
            model.CustomFields = _custom.GetProfileCustomFields(null).Result;

            return View(_theme + "Search.cshtml", model);
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