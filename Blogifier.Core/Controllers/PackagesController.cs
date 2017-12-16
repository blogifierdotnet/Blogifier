using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Packages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Themes()
        {
            var model = new AdminPackagesModel
            {
                Profile = GetProfile(),
                Packages = await _pkgs.Find(PackageType.Themes)
            };

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

        [VerifyProfile]
        [HttpGet("settings")]
        public async Task<IActionResult> Settings(string theme)
        {
            var zones = new List<ZoneViewModel>();

            var fields = await _db.CustomFields.GetCustomFields(CustomType.Application, 0);

            if (fields.Any())
            {
                foreach (var field in fields)
                {
                    if (field.Key.StartsWith(theme) && field.Key.Contains("-"))
                    {
                        // widget
                        var w = field.Key.Replace(theme + "-", "");
                    }

                    if (field.Key.StartsWith(theme) && field.Key.Contains(":"))
                    {
                        // zone
                        var z = field.Key.Replace(theme + ":", "");
                        var zone = new ZoneViewModel { Theme = theme, Zone = z };
                        zones.Add(zone);
                    }
                }
            }

            var model = new ThemeSettingsModel
            {
                Profile = GetProfile(),
                Zones = zones
            };

            return View($"{_theme}Packages/Settings.cshtml", model);
        }

        private Profile GetProfile()
        {
            return _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
        }
    }
}