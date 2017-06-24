using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.FileSystem;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
    [Authorize]
    [VerifyProfile]
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
            if (profile == null)
                return RedirectToAction("Profile", "Admin");

            return View(_theme + "Index.cshtml", new AdminBaseModel { Profile = profile });
        }

        [Route("editor/{id:int}")]
        public IActionResult Editor(int id)
        {
            var profile = GetProfile();
            if (profile == null)
                return RedirectToAction("Profile", "Admin");

            IEnumerable<SelectListItem> categories = null;
            var post = new BlogPost();

            if (id > 0)
            {
                categories = _db.Categories.CategoryList(c => c.ProfileId == profile.Id);
                post = _db.BlogPosts.SingleIncluded(p => p.Id == id).Result;
            }        

            var model = new AdminEditorModel { Profile = profile, CategoryList = categories, BlogPost = post };
            return View(_theme + "Editor.cshtml", model);
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

		[Route("settings")]
		public IActionResult Settings()
		{
			return View(_theme + "Settings.cshtml", new AdminBaseModel { Profile = GetProfile() });
		}

		#region Private members

		private Profile GetProfile()
		{
			var profile = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);

            //if (profile == null)
            //    Profile();

            return profile;
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
