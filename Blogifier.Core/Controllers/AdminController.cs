using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return RedirectToAction("Index", "Content");
        }

        [VerifyProfile]
        [Route("files")]
        public async Task<IActionResult> Files(string search = "")
        {
            return View(_theme + "Files.cshtml", new AdminBaseModel { Profile = await GetProfile() });
        }

        [Route("setup")]
        public IActionResult Setup()
        {
            return View(_theme + "Setup.cshtml", new AdminSetupModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("setup")]
        public async Task<IActionResult> Setup(AdminSetupModel model)
        {
            if (ModelState.IsValid)
            {
                var profile = new Profile();

                if (!await _db.Profiles.All().AnyAsync())
                {
                    profile.IsAdmin = true;
                }
                profile.AuthorName = model.AuthorName;
                profile.AuthorEmail = model.AuthorEmail;
                profile.Title = model.Title;
                profile.Description = model.Description;

                profile.IdentityName = User.Identity.Name;
                profile.Slug = await SlugFromTitle(profile.AuthorName);
                profile.Avatar = ApplicationSettings.ProfileAvatar;
                profile.BlogTheme = BlogSettings.Theme;

                profile.LastUpdated = SystemClock.Now();

                await _db.Profiles.Add(profile);
                await _db.Complete();

                return RedirectToAction("Index");
            }
            return View(_theme + "Setup.cshtml", model);
        }

        private async Task<Profile> GetProfile()
        {
            return await _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
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