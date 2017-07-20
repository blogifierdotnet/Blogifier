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
            var model = new SettingsCustom
            {
                Profile = GetProfile(),
                CustomFields = GetProfileCustomFields(profile.Id)
            };
            return View(_theme + "Custom.cshtml", model);
        }

        [HttpPost]
        [Route("custom")]
        public IActionResult Custom(SettingsCustom model)
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
            var model = new SettingsProfile();
            model.Profile = GetProfile();
            
            if(model.Profile != null)
            {
                model.AuthorName = model.Profile.AuthorName;
                model.AuthorEmail = model.Profile.AuthorEmail;
                model.Avatar = model.Profile.Avatar;
                model.CustomFields = GetProfileCustomFields(model.Profile.Id);
            }
            return View(_theme + "Profile.cshtml", model);
        }

        [HttpPost]
        [Route("profile")]
        public IActionResult Profile(SettingsProfile model)
        {
            var profile = GetProfile();
            if (ModelState.IsValid)
            {
                if (profile == null)
                {
                    profile = new Profile();

                    if (_db.Profiles.All().ToList().Count == 0)
                    {
                        profile.IsAdmin = true;
                    }
                    profile.AuthorName = model.AuthorName;
                    profile.AuthorEmail = model.AuthorEmail;
                    profile.Avatar = model.Avatar;

                    profile.IdentityName = User.Identity.Name;
                    profile.Slug = SlugFromTitle(profile.AuthorName);
                    profile.Title = ApplicationSettings.Title;
                    profile.Description = ApplicationSettings.Description;
                    profile.BlogTheme = ApplicationSettings.BlogTheme;

                    _db.Profiles.Add(profile);
                }
                else
                {
                    profile.AuthorName = model.AuthorName;
                    profile.AuthorEmail = model.AuthorEmail;
                    profile.Avatar = model.Avatar;
                }
                _db.Complete();

                model.Profile = GetProfile();

                // save custom fields
                if(profile.Id > 0)
                {
                    SaveCustomFields(model.CustomFields, profile.Id);
                }

                ViewBag.Message = "Profile updated";
            }
            return View(_theme + "Profile.cshtml", model);
        }

        [VerifyProfile]
        [Route("personal")]
        public IActionResult Personal()
        {
            if (ApplicationSettings.SingleBlog)
                return NotFound();

            var profile = GetProfile();
            var storage = new BlogStorage("");

            var model = new SettingsPersonal
            {
                Profile = profile,
                BlogThemes = storage.GetThemes(ThemeType.Blog)
            };
            if (profile != null)
            {
                model.Title = profile.Title;
                model.Description = profile.Description;
                model.BlogTheme = profile.BlogTheme;
                model.Image = profile.Image;
                model.Logo = profile.Logo;
            }
            return View(_theme + "Personal.cshtml", model);
        }

        [HttpPost]
        [Route("personal")]
        public IActionResult Personal(SettingsPersonal model)
        {
            var storage = new BlogStorage("");
            model.BlogThemes = storage.GetThemes(ThemeType.Blog);
            model.Profile = GetProfile();

            if (ModelState.IsValid)
            {
                model.Profile.Title = model.Title;
                model.Profile.Description = model.Description;
                model.Profile.BlogTheme = model.BlogTheme;
                model.Profile.Logo = model.Logo;
                model.Profile.Image = model.Image;

                _db.Complete();
                ViewBag.Message = "Updated";
            }
            return View(_theme + "Personal.cshtml", model);
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

        Dictionary<string, string> GetProfileCustomFields(int profileId)
        {
            var fields = new Dictionary<string, string>();
            foreach (var item in ApplicationSettings.SocialButtons)
            {
                fields.Add(item.Key, item.Value);
            }

            if (!fields.ContainsKey("disqus")) fields.Add("disqus", "");
            if (!fields.ContainsKey("Google")) fields.Add("Google", "");
            if (!fields.ContainsKey("Twitter")) fields.Add("Twitter", "");
            if (!fields.ContainsKey("Github")) fields.Add("Github", "");
            if (!fields.ContainsKey("Facebook")) fields.Add("Facebook", "");
            if (!fields.ContainsKey("Instagram")) fields.Add("Instagram", "");

            if (!fields.ContainsKey("Head")) fields.Add("Head", "");
            if (!fields.ContainsKey("Footer")) fields.Add("Footer", "");
            if (!fields.ContainsKey("Post")) fields.Add("Post", "");

            var dbFields = _db.CustomFields.Find(f => f.CustomType == CustomType.Profile && f.ParentId == profileId);
            if (dbFields != null && dbFields.Count() > 0)
            {
                foreach (var field in dbFields)
                {
                    if (fields.ContainsKey(field.CustomKey))
                    {
                        fields[field.CustomKey] = field.CustomValue;
                    }
                }
            }
            return fields;
        }

        void SaveCustomFields(Dictionary<string, string> fields, int profileId)
        {
            var updated = new List<string>();
            var dbFields = _db.CustomFields.Find(f => f.CustomType == CustomType.Profile && f.ParentId == profileId);

            // update existing DB fields
            if (dbFields != null && dbFields.Count() > 0)
            {
                foreach (var field in dbFields)
                {
                    if (fields.ContainsKey(field.CustomKey))
                    {
                        field.CustomValue = fields[field.CustomKey];
                        updated.Add(field.CustomKey);
                    }
                }
            }
            _db.Complete();

            // add new DB entries
            foreach (var item in fields)
            {
                if (!updated.Contains(item.Key))
                {
                    _db.CustomFields.Add(new CustomField
                    {
                        CustomKey = item.Key,
                        CustomValue = item.Value,
                        Title = item.Key,
                        CustomType = CustomType.Profile,
                        ParentId = profileId
                    });
                }
            }
            _db.Complete();
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
