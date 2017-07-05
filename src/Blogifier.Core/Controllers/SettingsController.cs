using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Blogifier.Core.Controllers
{
    [Authorize]
    [Route("admin/[controller]")]
	public class SettingsController : Controller
	{
		IUnitOfWork _db;
        ILogger _logger;
        private readonly IConfiguration _config;
        string _theme;

		public SettingsController(IUnitOfWork db, IConfiguration config, ILogger<SettingsController> logger)
		{
			_db = db;
            _config = config;
            _logger = logger;
            _theme = string.Format("~/Views/Blogifier/Admin/{0}/Settings/", 
                ApplicationSettings.BlogTheme);
        }

        [VerifyProfile]
        [Route("basic")]
        public IActionResult Basic()
        {
            var profile = GetProfile();
            var storage = new BlogStorage("");

            var model = new AdminProfileModel
            {
                Profile = profile,
                BlogThemes = storage.GetThemes(ThemeType.Blog)
            };
            return View(_theme + "Basic.cshtml", model);
        }

        [VerifyProfile]
        [Route("application")]
        public IActionResult Application()
        {
            if (_config.GetConnectionString("BLOGIFIER") != null)
            {
                _logger.LogWarning("XXXXX -> BLOGIFIER -> " + _config.GetConnectionString("BLOGIFIER"));
            }

            if (_config.GetConnectionString("DefaultConnection") != null)
            {
                _logger.LogWarning("XXXXX -> DefaultConnection -> " + _config.GetConnectionString("DefaultConnection"));
            }

            return View(_theme + "Application.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        [VerifyProfile]
        [Route("disqus")]
        public IActionResult Disqus()
        {
            var profile = GetProfile();

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
            return View(_theme + "Disqus.cshtml", model);
        }

        [HttpPost]
        [Route("disqus")]
        public IActionResult Disqus(AdminToolsModel model)
        {
            model.Profile = GetProfile();

            var existing = _db.CustomFields.Single(f =>
                f.CustomKey == "disqus" &&
                f.CustomType == CustomType.Profile &&
                f.ParentId == model.Profile.Id);

            if (existing != null)
                model.DisqusModel.Id = existing.Id;

            if (model.DisqusModel.Id > 0)
            {
                existing.CustomValue = model.DisqusModel.CustomValue;
            }
            else
            {
                _db.CustomFields.Add(model.DisqusModel);
            }
            _db.Complete();

            ViewBag.Message = "Updated";
            return View(_theme + "Disqus.cshtml", model);
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            var profile = GetProfile();
            var storage = new BlogStorage("");

            var model = new AdminProfileModel
            {
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

            if (_db.Profiles.All().ToList().Count == 0)
            {
                model.Profile.IsAdmin = true;
            }

            if (profile.Id == 0)
            {
                profile.IdentityName = User.Identity.Name;
                profile.Slug = SlugFromTitle(profile.AuthorName);
                profile.BlogTheme = ApplicationSettings.BlogTheme;

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

        [VerifyProfile]
        [Route("import")]
        public IActionResult Import()
        {
            return View(_theme + "Import.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        Profile GetProfile()
        {
            return _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
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
