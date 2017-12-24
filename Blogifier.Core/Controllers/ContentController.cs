using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
    [Authorize]
    [Route("admin/[controller]")]
    public class ContentController : Controller
	{
		private readonly string _theme;
        IUnitOfWork _db;
        ISearchService _search;

		public ContentController(IUnitOfWork db, ISearchService search)
		{
			_db = db;
            _search = search;
			_theme = $"~/{ApplicationSettings.BlogAdminFolder}/Content/";
		}

        [VerifyProfile]
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string user = "0", string status = "A", string cats = "", string search = "")
		{
            var profile = await GetProfile();
           
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
                userProfile = await _db.Profiles.Single(p => p.Id == int.Parse(user));

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
        public async Task<IActionResult> Editor(int id, string user = "0")
        {
            var profile = await GetProfile();
            var userProfile = profile;

            if (user != "0")
            {
                userProfile = await _db.Profiles.Single(p => p.Id == int.Parse(user));
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
                    post = await _db.BlogPosts.SingleIncluded(p => p.Id == id && p.Profile.Id == profile.Id);
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

        private async Task<Profile> GetProfile()
        {
            return await _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
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
