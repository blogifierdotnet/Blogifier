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
                ApplicationSettings.AdminTheme);
        }

        [VerifyProfile]
        [Route("custom")]
        public IActionResult Custom()
        {
            var profile = GetProfile();
            var fields = ApplicationSettings.SocialButtons;

            if (!fields.ContainsKey("disqus")) fields.Add("disqus", "");
            if (!fields.ContainsKey("Google")) fields.Add("Google", "");
            if (!fields.ContainsKey("Twitter")) fields.Add("Twitter", "");
            if (!fields.ContainsKey("Github")) fields.Add("Github", "");
            if (!fields.ContainsKey("Facebook")) fields.Add("Facebook", "");
            if (!fields.ContainsKey("Instagram")) fields.Add("Instagram", "");

            var dbFields = _db.CustomFields.Find(f => f.CustomType == CustomType.Profile && f.ParentId == profile.Id);
            if (dbFields != null && dbFields.Count() > 0)
            {
                foreach (var field in dbFields)
                {
                    if(fields.ContainsKey(field.CustomKey))
                    {
                        fields[field.CustomKey] = field.CustomValue;
                    }
                }
            }
            var model = new AdminToolsModel
            {
                Profile = GetProfile(),
                CustomFields = fields
            };
            return View(_theme + "Custom.cshtml", model);
        }

        [HttpPost]
        [Route("custom")]
        public IActionResult Custom(AdminToolsModel model)
        {
            model.Profile = GetProfile();

            var updated = new List<string>();
            var dbFields = _db.CustomFields.Find(f => f.CustomType == CustomType.Profile && f.ParentId == model.Profile.Id);

            // update existing DB fields
            if (dbFields != null && dbFields.Count() > 0)
            {
                foreach (var field in dbFields)
                {
                    if (model.CustomFields.ContainsKey(field.CustomKey))
                    {
                        field.CustomValue = model.CustomFields[field.CustomKey];
                        updated.Add(field.CustomKey);
                    }
                }
            }
            _db.Complete();

            // add new DB entries
            foreach (var item in model.CustomFields)
            {
                if (!updated.Contains(item.Key))
                {
                    _db.CustomFields.Add(new CustomField {
                        CustomKey = item.Key,
                        CustomValue = item.Value,
                        Title = item.Key,
                        CustomType = CustomType.Profile,
                        ParentId = model.Profile.Id
                    });
                }
            }
            _db.Complete();

            ViewBag.Message = "Updated";
            return View(_theme + "Custom.cshtml", model);
        }

        [VerifyProfile]
        [Route("application")]
        public IActionResult Application()
        {
            return View(_theme + "Application.cshtml", new AdminBaseModel { Profile = GetProfile() });
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
