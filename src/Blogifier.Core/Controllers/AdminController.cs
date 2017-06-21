using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Services.FileSystem;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

		public AdminController(IUnitOfWork db, IRssService rss, ILogger<AdminController> logger)
		{
			_db = db;
			_rss = rss;
            _logger = logger;
			_theme = "~/Views/Blogifier/Admin/" + ApplicationSettings.AdminTheme + "/";
		}

        public IActionResult Index()
		{
            var profile = GetProfile();

            if(profile == null)
                return RedirectToAction("Profile", "Admin");

            var categories = _db.Categories.CategoryList(c => c.ProfileId == profile.Id);

            var model = new AdminPostsModel { Profile = profile, CategoryList = categories };
            return View(_theme + "Index.cshtml", model);
		}

        [Route("files")]
        public IActionResult Files()
        {
            return View(_theme + "Files.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        [HttpGet]
		[Route("tools")]
		public IActionResult Tools()
		{
            var profile = GetProfile();

            if (profile == null)
                return RedirectToAction("Profile", "Admin");

            var disqus = _db.CustomFields.Single(f =>
                f.CustomKey == "disqus" &&
                f.CustomType == CustomType.Profile &&
                f.ParentId == profile.Id);

            if (disqus == null)
                disqus = new CustomField { CustomKey = "disqus", CustomType = CustomType.Profile, ParentId = profile.Id };

            var model = new AdminToolsModel
            {
                Profile = GetProfile(),
                RssImportModel = new RssImportModel(),
                DisqusModel = disqus
            };
			return View(_theme + "Tools.cshtml", model);
		}

        [HttpGet]
		[Route("profile")]
		public IActionResult Profile()
		{
			var profile = GetProfile();

			var storage = new BlogStorage("");

			var model = new AdminProfileModel {
                Profile = profile,
                BlogThemes = storage.GetThemes(ThemeType.Blog)
            };

			return View(_theme + "Profile.cshtml", model);
		}

		[HttpPost]
		[Route("profile")]
		public IActionResult Profile(AdminProfileModel model)
		{
            var profile = model.Profile;

            if(_db.Profiles.All().ToList().Count == 0)
            {
                model.Profile.IsAdmin = true;
            }

            if(profile.Id == 0)
            {
                profile.IdentityName = User.Identity.Name;
                profile.Slug = SlugFromTitle(profile.AuthorName);

                ModelState.Clear();
                TryValidateModel(model);
            }

            if (ModelState.IsValid)
            {
                if (profile.Id > 0)
                {
                    var existing = _db.Profiles.Single(b => b.Id == profile.Id);
                    existing.Title = profile.Title;
                    existing.Description = profile.Description;
                    existing.AuthorName = profile.AuthorName;
                    existing.AuthorEmail = profile.AuthorEmail;
                    existing.BlogTheme = profile.BlogTheme;
                    existing.Logo = profile.Logo;
                    existing.Avatar = profile.Avatar;
                    existing.Image = profile.Image;
                }
                else
                {
                    _db.Profiles.Add(profile);
                }
                _db.Complete();

                var updated = _db.Profiles.Single(b => b.IdentityName == profile.IdentityName);
                model.Profile = updated;

                ViewBag.Message = "Profile updated";
            }

            var storage = new BlogStorage("");
            model.BlogThemes = storage.GetThemes(ThemeType.Blog);

            return View(_theme + "Profile.cshtml", model);
		}

		[Route("settings")]
		public IActionResult Settings()
		{
			return View(_theme + "Settings.cshtml", new AdminBaseModel { Profile = GetProfile() });
		}

		#region Private members

		private Profile GetProfile()
		{
			return _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
		}

        private string SlugFromTitle(string title)
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
		
        #endregion
	}
}
