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
            var widgets = new List<string>();

            var fields = await _db.CustomFields.GetCustomFields(CustomType.Application, 0);

            if (fields.Any())
            {
                foreach (var field in fields)
                {
                    var wKey = $"w:{theme}-";
                    var zKey = $"z:{theme}-";

                    if (field.Key.StartsWith(wKey))
                    {
                        widgets.Add(field.Key.Replace(wKey, ""));
                    }

                    if (field.Key.StartsWith(zKey))
                    {
                        var zValues = field.Key.Replace(zKey, "").Split('-');
                        if(zValues.Length == 2)
                        {
                            var zone = zValues[0];
                            var widget = zValues[1];

                            var zoneItem = zones.Where(z => z.Zone == zone).FirstOrDefault();

                            if(zoneItem == null)
                            {                              
                                zones.Add(new ZoneViewModel {
                                    Theme = theme, Zone = zone, Widgets = new List<string> { widget } }
                                );
                            }
                            else
                            {
                                zoneItem.Widgets.Add(widget);
                            }
                        }
                    }
                }
            }

            var model = new ThemeSettingsModel
            {
                Profile = GetProfile(),
                Zones = zones,
                Widgets = widgets
            };

            return View($"{_theme}Packages/Settings.cshtml", model);
        }

        private Profile GetProfile()
        {
            return _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
        }
    }
}