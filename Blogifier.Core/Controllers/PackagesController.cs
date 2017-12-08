using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Packages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
    [Authorize]
    [Route("admin/[controller]")]
    public class PackagesController : Controller
	{
		private readonly string _theme;
        IUnitOfWork _db;
        IPackageService _pkgs;

		public PackagesController(IUnitOfWork db, IPackageService pkgs)
		{
			_db = db;
            _pkgs = pkgs;
			_theme = $"~/{ApplicationSettings.BlogAdminFolder}/";
		}

        [VerifyProfile]
        [HttpGet("widgets")]
        public async Task<IActionResult> Widgets()
        {
            var model = new AdminPackagesModel {
                Profile = GetProfile(),
                Packages = await _pkgs.Find(PackageType.Widgets)
            };
            return View($"{_theme}Packages/Widgets.cshtml", model);
        }

        [VerifyProfile]
        [HttpGet("themes")]
        public IActionResult Themes()
        {
            var model = new AdminPackagesModel { Profile = GetProfile() };
            model.Packages = new List<PackageListItem>();

            return View($"{_theme}Packages/Themes.cshtml", model);
        }

        [VerifyProfile]
        [HttpGet("plugins")]
        public async Task<IActionResult> Plugins()
        {
            var model = new AdminPackagesModel
            {
                Profile = GetProfile(),
                Packages = await _pkgs.Find(PackageType.Plugins)
            };

            return View($"{_theme}Packages/Plugins.cshtml", model);
        }

        private Profile GetProfile()
        {
            return _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
        }
    }
}