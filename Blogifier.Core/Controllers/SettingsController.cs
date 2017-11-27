using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        string _theme;

		public SettingsController(IUnitOfWork db, ILogger<SettingsController> logger)
		{
			_db = db;
            _logger = logger;
            _theme = $"~/{ApplicationSettings.BlogAdminFolder}/Settings/";
        }

        [VerifyProfile]
        [Route("application")]
        public IActionResult Application(int page = 1)
        {
            var pager = new Pager(page);
            var blogs = _db.Profiles.ProfileList(p => p.Id > 0, pager);

            var model = new AdminApplicationModel
            {
                Profile = GetProfile(),
                Blogs = blogs,
                Pager = pager
            };
            return View(_theme + "Application.cshtml", model);
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
                model.EmailEnabled = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.SendGridApiKey).Length > 0;
                model.CustomFields = _db.CustomFields.GetUserFields(model.Profile.Id).Result;
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
                    profile.Title = BlogSettings.Title;
                    profile.Description = BlogSettings.Description;
                    profile.BlogTheme = BlogSettings.Theme;

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
                if(profile.Id > 0 && model.CustomFields != null)
                {
                    SaveCustomFields(model.CustomFields, profile.Id);
                }
                model.CustomFields = _db.CustomFields.GetUserFields(model.Profile.Id).Result;

                ViewBag.Message = "Profile updated";
            }
            return View(_theme + "Profile.cshtml", model);
        }

        [VerifyProfile]
        [Route("import")]
        public IActionResult Import()
        {
            return View(_theme + "Import.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        [VerifyProfile]
        [Route("about")]
        public IActionResult About()
        {
            return View(_theme + "About.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        [VerifyProfile]
        [Route("general")]
        public IActionResult General()
        {
            var profile = GetProfile();
            var storage = new BlogStorage("");

            var model = new SettingsGeneral
            {
                Profile = profile,
                BlogThemes = storage.GetThemes(),
                Title = BlogSettings.Title,
                Description = BlogSettings.Description,
                BlogTheme = BlogSettings.Theme,
                Logo = BlogSettings.Logo,
                Avatar = ApplicationSettings.ProfileAvatar,
                Image = BlogSettings.Cover,
                EmailKey = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.SendGridApiKey),
                BlogHead = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.HeadCode),
                BlogFooter = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.FooterCode)
            };
            return View(_theme + "General.cshtml", model);
        }

        [HttpPost]
        [Route("general")]
        public IActionResult General(SettingsGeneral model)
        {
            var storage = new BlogStorage("");
            model.BlogThemes = storage.GetThemes();
            model.Profile = GetProfile();

            if (ModelState.IsValid)
            {
                BlogSettings.Title = model.Title;
                BlogSettings.Description = model.Description;
                BlogSettings.Logo = model.Logo;
                ApplicationSettings.ProfileAvatar = model.Avatar;
                BlogSettings.Cover = model.Image;
                BlogSettings.Theme = model.BlogTheme;

                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.Title, model.Title);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.Description, model.Description);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ProfileLogo, model.Logo);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ProfileAvatar, model.Avatar);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ProfileImage, model.Image);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.BlogTheme, model.BlogTheme);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.SendGridApiKey, model.EmailKey);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.HeadCode, model.BlogHead);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.FooterCode, model.BlogFooter);

                model.Profile.BlogTheme = model.BlogTheme;

                _db.Complete();

                ViewBag.Message = "Updated";
            }
            return View(_theme + "General.cshtml", model);
        }

        [VerifyProfile]
        [Route("posts")]
        public IActionResult Posts()
        {
            var profile = GetProfile();

            var model = new SettingsPosts
            {
                Profile = profile,
                PostImage = BlogSettings.Cover,
                PostFooter = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.PostCode),
                ItemsPerPage = BlogSettings.ItemsPerPage
            };
            return View(_theme + "Posts.cshtml", model);
        }

        [HttpPost]
        [Route("posts")]
        public IActionResult Posts(SettingsPosts model)
        {
            model.Profile = GetProfile();

            if (ModelState.IsValid)
            {
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ItemsPerPage, model.ItemsPerPage.ToString());
                BlogSettings.ItemsPerPage = model.ItemsPerPage;

                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.PostImage, model.PostImage);
                BlogSettings.PostCover = model.PostImage;

                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.PostCode, model.PostFooter);

                _db.Complete();

                ViewBag.Message = "Updated";
            }
            return View(_theme + "Posts.cshtml", model);
        }

        [VerifyProfile]
        [Route("advanced")]
        public IActionResult Advanced()
        {
            var profile = GetProfile();

            var model = new SettingsAdvanced
            {
                Profile = profile
            };
            return View(_theme + "Advanced.cshtml", model);
        }

        Profile GetProfile()
        {
            return _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
        }

        void SaveCustomFields(Dictionary<string, string> fields, int profileId)
        {
            if(fields != null && fields.Count > 0)
            {
                foreach (var field in fields)
                {
                    _db.CustomFields.SetCustomField(CustomType.Profile, profileId, field.Key, field.Value);
                }
            }
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
