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
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
    [Authorize]
    [Route("admin/[controller]")]
    public class PostsController : Controller
	{
		private readonly string _theme;
        private readonly ILogger _logger;
        private readonly ICompositeViewEngine _engine;
        IUnitOfWork _db;
		IRssService _rss;
        ISearchService _search;

		public PostsController(IUnitOfWork db, IRssService rss, ISearchService search, ILogger<AdminController> logger, ICompositeViewEngine engine)
		{
			_db = db;
			_rss = rss;
            _search = search;
            _logger = logger;
            _engine = engine;
			_theme = $"~/{ApplicationSettings.BlogAdminFolder}/";
		}

        [VerifyProfile]
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string user = "0", string status = "A", string cats = "", string search = "")
		{
            var profile = GetProfile();
           
            var fields = await _db.CustomFields.GetCustomFields(CustomType.Profile, profile.Id);
            var pageSize = BlogSettings.ItemsPerPage;

            if (fields.ContainsKey(Constants.PostListSize))
                pageSize = int.Parse(fields[Constants.PostListSize]);

            var pager = new Pager(page, pageSize);        
            var model = new AdminPostsModel { Profile = profile };

            model.CustomFields = fields;

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
    }
}
