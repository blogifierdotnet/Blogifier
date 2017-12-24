using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [Route("profile")]
        public async Task<IActionResult> Profile()
        {
            var model = new SettingsProfile();
            model.Profile = await GetProfile();
            
            if(model.Profile != null)
            {
                model.AuthorName = model.Profile.AuthorName;
                model.AuthorEmail = model.Profile.AuthorEmail;
                model.Avatar = model.Profile.Avatar;
                model.EmailEnabled = (await _db.CustomFields.GetValue(CustomType.Application, 0, Constants.SendGridApiKey)).Length > 0;
                model.CustomFields = await _db.CustomFields.GetUserFields(model.Profile.Id);
            }
            return View(_theme + "Profile.cshtml", model);
        }

        [HttpPost]
        [Route("profile")]
        public async Task<IActionResult> Profile(SettingsProfile model)
        {
            var profile = await GetProfile();
            if (ModelState.IsValid)
            {
                if (profile == null)
                {
                    profile = new Profile();

                    if (!await _db.Profiles.All().AnyAsync())
                    {
                        profile.IsAdmin = true;
                    }
                    profile.AuthorName = model.AuthorName;
                    profile.AuthorEmail = model.AuthorEmail;
                    profile.Avatar = model.Avatar;

                    profile.IdentityName = User.Identity.Name;
                    profile.Slug = await SlugFromTitle(profile.AuthorName);
                    profile.Title = BlogSettings.Title;
                    profile.Description = BlogSettings.Description;
                    profile.BlogTheme = BlogSettings.Theme;

                    await _db.Profiles.Add(profile);
                }
                else
                {
                    profile.AuthorName = model.AuthorName;
                    profile.AuthorEmail = model.AuthorEmail;
                    profile.Avatar = model.Avatar;
                }
                await _db.Complete();

                model.Profile = await GetProfile();

                // save custom fields
                if(profile.Id > 0 && model.CustomFields != null)
                {
                    await SaveCustomFields(model.CustomFields, profile.Id);
                }
                model.CustomFields = await _db.CustomFields.GetUserFields(model.Profile.Id);

                ViewBag.Message = "Profile updated";
            }
            return View(_theme + "Profile.cshtml", model);
        }

        [VerifyProfile]
        [Route("about")]
        public async Task<IActionResult> About()
        {
            return View(_theme + "About.cshtml", new AdminBaseModel { Profile = await GetProfile() });
        }

        [MustBeAdmin]
        [Route("general")]
        public async Task<IActionResult> General()
        {
            var profile = await GetProfile();
            var storage = new BlogStorage("");

            var model = new SettingsGeneral
            {
                Profile = profile,
                BlogThemes = BlogSettings.BlogThemes,
                Title = BlogSettings.Title,
                Description = BlogSettings.Description,
                BlogTheme = BlogSettings.Theme,
                Logo = BlogSettings.Logo,
                Avatar = ApplicationSettings.ProfileAvatar,
                Image = BlogSettings.Cover,
                EmailKey = await _db.CustomFields.GetValue(CustomType.Application, 0, Constants.SendGridApiKey),
                BlogHead = await _db.CustomFields.GetValue(CustomType.Application, 0, Constants.HeadCode),
                BlogFooter = await _db.CustomFields.GetValue(CustomType.Application, 0, Constants.FooterCode)
            };
            return View(_theme + "General.cshtml", model);
        }

        [HttpPost]
        [MustBeAdmin]
        [Route("general")]
        public async Task<IActionResult> General(SettingsGeneral model)
        {
            var storage = new BlogStorage("");
            model.BlogThemes = BlogSettings.BlogThemes;
            model.Profile = await GetProfile();

            if (ModelState.IsValid)
            {
                BlogSettings.Title = model.Title;
                BlogSettings.Description = model.Description;
                BlogSettings.Logo = model.Logo;
                ApplicationSettings.ProfileAvatar = model.Avatar;
                BlogSettings.Cover = model.Image;
                BlogSettings.Theme = model.BlogTheme;

                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.Title, model.Title);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.Description, model.Description);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ProfileLogo, model.Logo);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ProfileAvatar, model.Avatar);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ProfileImage, model.Image);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.BlogTheme, model.BlogTheme);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.SendGridApiKey, model.EmailKey);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.HeadCode, model.BlogHead);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.FooterCode, model.BlogFooter);

                model.Profile.BlogTheme = model.BlogTheme;

                await _db.Complete();

                ViewBag.Message = "Updated";
            }
            return View(_theme + "General.cshtml", model);
        }

        [MustBeAdmin]
        [Route("posts")]
        public async Task<IActionResult> Posts()
        {
            var profile = await GetProfile();

            var model = new SettingsPosts
            {
                Profile = profile,
                PostImage = BlogSettings.Cover,
                PostFooter = await _db.CustomFields.GetValue(CustomType.Application, 0, Constants.PostCode),
                ItemsPerPage = BlogSettings.ItemsPerPage
            };
            return View(_theme + "Posts.cshtml", model);
        }

        [HttpPost]
        [MustBeAdmin]
        [Route("posts")]
        public async Task<IActionResult> Posts(SettingsPosts model)
        {
            model.Profile = await GetProfile();

            if (ModelState.IsValid)
            {
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ItemsPerPage, model.ItemsPerPage.ToString());
                BlogSettings.ItemsPerPage = model.ItemsPerPage;

                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.PostImage, model.PostImage);
                BlogSettings.PostCover = model.PostImage;

                await _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.PostCode, model.PostFooter);

                await _db.Complete();

                ViewBag.Message = "Updated";
            }
            return View(_theme + "Posts.cshtml", model);
        }

        [MustBeAdmin]
        [Route("advanced")]
        public async Task<IActionResult> Advanced()
        {
            var profile = await GetProfile();

            var model = new SettingsAdvanced
            {
                Profile = profile
            };
            return View(_theme + "Advanced.cshtml", model);
        }

        async Task<Profile> GetProfile()
        {
            return await _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
        }

        async Task SaveCustomFields(Dictionary<string, string> fields, int profileId)
        {
            if(fields != null && fields.Count > 0)
            {
                foreach (var field in fields)
                {
                    await _db.CustomFields.SetCustomField(CustomType.Profile, profileId, field.Key, field.Value);
                }
            }
        }

        async Task<string> SlugFromTitle(string title)
        {
            var slug = title.ToSlug();
            if (await _db.Profiles.Single(b => b.Slug == slug) != null)
            {
                for (int i = 2; i < 100; i++)
                {
                    if (await _db.Profiles.Single(b => b.Slug == slug + i.ToString()) == null)
                    {
                        return slug + i.ToString();
                    }
                }
            }
            return slug;
        }
    }
}
