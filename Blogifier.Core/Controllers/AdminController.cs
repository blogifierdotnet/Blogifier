using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Blogifier.Core.Controllers
{
    [Authorize]
    [Route("admin")]
	public class AdminController : Controller
	{
		private readonly string _theme;
        IUnitOfWork _db;

		public AdminController(IUnitOfWork db, ISearchService search, ILogger<AdminController> logger)
		{
			_db = db;
			_theme = $"~/{ApplicationSettings.BlogAdminFolder}/";
		}

        [VerifyProfile]
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("~/admin/blogposts");
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
                profile.BlogTheme = BlogSettings.Theme;

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