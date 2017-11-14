using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
			_theme = $"~/{ApplicationSettings.BlogAdminFolder}/";
		}

        [VerifyProfile]
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string user = "0", string status = "A", string cats = "", string search = "")
		{
            var profile = GetProfile();

            var pageSize = ApplicationSettings.ItemsPerPage;
            var fields = await _db.CustomFields.GetCustomFields(CustomType.Profile, profile.Id);
            if (fields.ContainsKey(Constants.PostListSize))
                pageSize = int.Parse(fields[Constants.PostListSize]);

            var pager = new Pager(page, pageSize);
            
            var model = new AdminPostsModel { Profile = profile };

            model.CustomFields = await _db.CustomFields.GetCustomFields(CustomType.Profile, profile.Id);

            if (model.Profile.IsAdmin)
                model.Users = _db.Profiles.Find(p => p.IdentityName != model.Profile.IdentityName);

            var userProfile = model.Profile;
            if (user != "0" && profile.IsAdmin)
                userProfile = _db.Profiles.Single(p => p.Id == int.Parse(user));

            model.StatusFilter = GetStatusFilter(status);

            var selectedCategories = new List<string>();
            var dbCategories = new List<Category>();
            model.CategoryFilter = _db.Categories.CategoryList(c => c.ProfileId == userProfile.Id).ToList();
            if (!string.IsNullOrEmpty(cats))
            {
                selectedCategories = cats.Split(',').ToList();
                foreach (var ftr in model.CategoryFilter)
                {
                    if (selectedCategories.Contains(ftr.Value))
                    {
                        ftr.Selected = true;
                    }
                }
            }

            if (string.IsNullOrEmpty(search))
            {
                model.BlogPosts = _db.BlogPosts.ByFilter(status, selectedCategories, userProfile.Slug, pager).Result;
            }
            else
            {
                model.BlogPosts = _search.Find(pager, search, userProfile.Slug).Result;
            }
            
            model.Pager = pager;

            var anyPost = _db.BlogPosts.Find(p => p.ProfileId == userProfile.Id).FirstOrDefault();
            ViewBag.IsFirstPost = anyPost == null;

            return View(_theme + "Index.cshtml", model);
        }

        [VerifyProfile]
        [Route("editor/{id:int}")]
        public IActionResult Editor(int id, string user = "0")
        {
            var profile = GetProfile();
            var userProfile = profile;

            if (user != "0")
            {
                userProfile = _db.Profiles.Single(p => p.Id == int.Parse(user));
            }

            var post = new BlogPost();
            var categories = _db.Categories.CategoryList(c => c.ProfileId == userProfile.Id).ToList();

            if (id > 0)
            {
                if (profile.IsAdmin)
                {
                    post = _db.BlogPosts.SingleIncluded(p => p.Id == id).Result;
                }
                else
                {
                    post = _db.BlogPosts.SingleIncluded(p => p.Id == id && p.Profile.Id == profile.Id).Result;
                }
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
