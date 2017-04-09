using System.Threading.Tasks;
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
			_theme = "~/Views/Blogifier/Themes/Admin/Standard/";
		}

        public async Task<IActionResult> Index()
		{
            var profile = GetProfile();

            if(profile == null)
                return RedirectToAction("Profile", "Admin");

            var posts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id);
            var model = new AdminPostsModel { Profile = profile, BlogPosts = posts };

            if (posts.Any())
            {
                var firstPost = posts.ToList()[0];
                model.SelectedPost = await _db.BlogPosts.SingleIncluded(p => p.Slug == firstPost.Slug);
            }
            return View(_theme + "Index.cshtml", model);
		}

        [HttpGet]
        [Route("{slug}")]
        public async Task<IActionResult> Index(string slug)
        {
            var profile = GetProfile();

            if (profile == null)
                return RedirectToAction("Profile", "Admin");

            var model = new AdminPostsModel {
                Profile = profile,
                SelectedPost = await _db.BlogPosts.SingleIncluded(p => p.Slug == slug),
                BlogPosts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id)
            };

            return View(_theme + "Index.cshtml", model);
        }

        [HttpGet]
		[Route("syndication")]
		public IActionResult Syndication()
		{
			var model = new AdminSyndicationModel { Profile = GetProfile() };
			return View(_theme + "Syndication.cshtml", model);
		}

		[HttpPost]
		[Route("syndication")]
		public async Task<IActionResult> Syndication(AdminSyndicationModel model)
		{
			model.Profile = GetProfile();

			if (model.Profile == null)
				return View("Error");

			model.ProfileId = model.Profile.Id;
			await _rss.Import(model, Url.Content("~/"));

			return RedirectToAction("Index", "Admin");
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

            if(profile.Id == 0)
            {
                profile.IdentityName = User.Identity.Name;
                profile.BlogTheme = ApplicationSettings.AdminTheme;
                profile.Slug = BlogSlugFromTitle(profile.Title);

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

		[Route("about")]
		public IActionResult About()
		{
			return View(_theme + "About.cshtml", new AdminBaseModel { Profile = GetProfile() });
		}

		#region Private members

		private Profile GetProfile()
		{
			return _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
		}

		private string BlogSlugFromTitle(string title)
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
