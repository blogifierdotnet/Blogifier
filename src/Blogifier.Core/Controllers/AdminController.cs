using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blogifier.Core.Controllers
{
    [Authorize]
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

        [VerifyProfile]
        [HttpGet("{page:int?}")]
        public IActionResult Index(int page = 1, string search = "")
		{
            if (page == 0) page = 1;
            var pager = new Pager(page);
            var model = new AdminPostsModel { Profile = GetProfile() };

            model.StatusFilter = GetStatusFilter("A");
            model.CategoryFilter = _db.Categories.CategoryList(c => c.ProfileId == model.Profile.Id).ToList();

            if (string.IsNullOrEmpty(search))
                model.BlogPosts = _db.BlogPosts.Find(p => p.Profile.IdentityName == User.Identity.Name, pager);
            else
                model.BlogPosts = _search.Find(pager, search, model.Profile.Slug).Result;

            model.Pager = pager;

            return View(_theme + "Index.cshtml", model);
        }

        [HttpPost]
        public IActionResult Index(IFormCollection fc)
        {
            var pager = new Pager(1);
            var profile = GetProfile();
            var model = new AdminPostsModel { Profile = profile };

            var status = fc.ContainsKey("status-filter") ? fc["status-filter"].ToString() : "A";
            model.StatusFilter = GetStatusFilter(status);

            var selectedCategories = new List<string>();
            var dbCategories = new List<Category>();
            model.CategoryFilter = _db.Categories.CategoryList(c => c.ProfileId == model.Profile.Id).ToList();
            if (fc.ContainsKey("category-filter"))
            {
                selectedCategories = fc["category-filter"].ToList();
                foreach (var ftr in model.CategoryFilter)
                {
                    if (selectedCategories.Contains(ftr.Value))
                    {
                        ftr.Selected = true;
                    }
                }
            }
            model.BlogPosts = _db.BlogPosts.ByFilter(status, selectedCategories, profile.Slug, pager).Result;
            model.Pager = pager;

            return View(_theme + "Index.cshtml", model);
        }

        [VerifyProfile]
        [Route("editor/{id:int}")]
        public IActionResult Editor(int id)
        {
            var profile = GetProfile();

            List<SelectListItem> categories = null;
            var post = new BlogPost();

            categories = _db.Categories.CategoryList(c => c.ProfileId == profile.Id).ToList();

            if (id > 0)
            {
                post = _db.BlogPosts.SingleIncluded(p => p.Id == id && p.Profile.Id == profile.Id).Result;
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

        [VerifyProfile]
        [Route("files")]
        public IActionResult Files(string search = "")
        {
            return View(_theme + "Files.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        [Route("setup")]
        public IActionResult Setup()
        {
            return View(_theme + "Setup.cshtml", new AdminSetupModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("setup")]
        public IActionResult Setup(AdminSetupModel model)
        {
            if (ModelState.IsValid)
            {
                var profile = new Profile();

                if (_db.Profiles.All().ToList().Count == 0)
                {
                    profile.IsAdmin = true;
                }
                profile.AuthorName = model.AuthorName;
                profile.AuthorEmail = model.AuthorEmail;
                profile.Title = model.Title;
                profile.Description = model.Description;

                profile.IdentityName = User.Identity.Name;
                profile.Slug = SlugFromTitle(profile.AuthorName);
                profile.Avatar = ApplicationSettings.ProfileAvatar;
                profile.BlogTheme = ApplicationSettings.BlogTheme;

                profile.LastUpdated = SystemClock.Now();

                _db.Profiles.Add(profile);
                _db.Complete();

                return RedirectToAction("Index");
            }
            return View(_theme + "Setup.cshtml", model);
        }

        private Profile GetProfile()
        {
            return _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
        }

        List<SelectListItem> GetStatusFilter(string filter)
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "All", Value = "A", Selected = filter == "A" },
                new SelectListItem { Text = "Drafts", Value = "D", Selected = filter == "D" },
                new SelectListItem { Text = "Published", Value = "P", Selected = filter == "P" }
            };
        }

        string SlugFromTitle(string title)
        {
            var slug = title.ToSlug();
            if (_db.Profiles.Single(b => b.Slug == slug) != null)
            {
                for (int i = 2; i < 100; i++)
                {
                    if (_db.Profiles.Single(b => b.Slug == slug + i.ToString()) == null)
                    {
                        return slug + i.ToString();
                    }
                }
            }
            return slug;
        }
    }
}
