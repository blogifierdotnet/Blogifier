using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blogifier.Core.Controllers
{
    [Authorize]
    [VerifyProfile]
    [Route("admin")]
	public class AdminController : Controller
	{
		private readonly string _theme;
        private readonly ILogger _logger;
        IUnitOfWork _db;
		IRssService _rss;
        ISearchService _search;

		public AdminController(IUnitOfWork db, IRssService rss, ISearchService search, ILogger<AdminController> logger)
		{
			_db = db;
			_rss = rss;
            _search = search;
            _logger = logger;
			_theme = "~/Views/Blogifier/Admin/" + ApplicationSettings.AdminTheme + "/";
		}

        [HttpGet("{page:int?}")]
        public IActionResult Index(int page = 1, string search = "", string filter = "")
		{
            if (page == 0) page = 1;
            var pager = new Pager(page);
            var model = new AdminPostsModel { Profile = GetProfile() };
            model.CategoryList = _db.Categories.CategoryList(c => c.ProfileId == model.Profile.Id).ToList();

            if (string.IsNullOrEmpty(search))
            {
                if(filter == "filterDraft")
                {
                    model.BlogPosts = _db.BlogPosts.Find(p => p.Profile.IdentityName == User.Identity.Name && p.Published == DateTime.MinValue, pager);
                }
                else if(filter == "filterPublished")
                {
                    model.BlogPosts = _db.BlogPosts.Find(p => p.Profile.IdentityName == User.Identity.Name && p.Published > DateTime.MinValue, pager);
                }
                else
                {
                    model.BlogPosts = _db.BlogPosts.Find(p => p.Profile.IdentityName == User.Identity.Name, pager);
                }
            }
            else
            {
                model.BlogPosts = _search.Find(pager, search, model.Profile.Slug).Result;
            }
            model.Pager = pager;

            return View(_theme + "Index.cshtml", model);
        }

        [Route("editor/{id:int}")]
        public IActionResult Editor(int id)
        {
            var profile = GetProfile();

            List<SelectListItem> categories = null;
            var post = new BlogPost();

            categories = _db.Categories.CategoryList(c => c.ProfileId == profile.Id).ToList();

            if (id > 0)
            {
                post = _db.BlogPosts.SingleIncluded(p => p.Id == id).Result;
            }

            if(post.PostCategories != null)
            {
                foreach (var pc in post.PostCategories)
                {
                    foreach (var cat in categories)
                    {
                        if (pc.CategoryId.ToString() == cat.Value)
                        {
                            cat.Selected = true;
                        }
                    }
                }
            }

            var model = new AdminEditorModel { Profile = profile, CategoryList = categories, BlogPost = post };
            return View(_theme + "Editor.cshtml", model);
        }

        [Route("files")]
        public IActionResult Files(string search = "")
        {
            return View(_theme + "Files.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

		private Profile GetProfile()
		{
			return _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
        }

        [Route("setup")]
        public IActionResult Setup()
        {
            return View(_theme + "Setup.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

    }
}
