using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blogifier.Core.Controllers
{
    [Authorize]
    [Route("packages")]
	public class PackagesController : Controller
	{
		private readonly string _theme;
        private readonly ILogger _logger;
        private readonly ICompositeViewEngine _engine;
        IUnitOfWork _db;
        ISearchService _search;

		public PackagesController(IUnitOfWork db, ILogger<AdminController> logger, ICompositeViewEngine engine)
		{
			_db = db;
            _logger = logger;
            _engine = engine;
			_theme = $"~/{ApplicationSettings.BlogAdminFolder}/";
		}

        [VerifyProfile]
        [HttpGet("widgets")]
        public IActionResult Widgets()
        {
            var model = new AdminPackagesModel { Profile = GetProfile() };

            model.Packages = new List<PackageListItem>();

            foreach (var assembly in Configuration.GetAssemblies())
            {
                var name = assembly.GetName().Name;
                    
                if (name != "Blogifier.Core")
                {
                    var path = $"~/Views/Shared/Components/{name}/Settings.cshtml";
                    var view = _engine.GetView("", path, false);

                    var item = new PackageListItem
                    {
                        Title = name,
                        Description = name,
                        Version = assembly.GetName().Version.ToString()
                    };

                    try
                    {
                        Type t = assembly.GetType("PackageInfo");
                        if (t != null)
                        {
                            var info = (IPackageInfo)Activator.CreateInstance(t);
                            item = info.GetAttributes();
                        }
                    }
                    catch { }

                    var disabled = Disabled();
                    var maxLen = 70;

                    item.Description = item.Description.Length > maxLen ? item.Description.Substring(0, maxLen) + "..." : item.Description;
                    item.HasSettings = view.Success;
                    item.Enabled = disabled == null || !disabled.Contains(name);
                    model.Packages.Add(item);
                }
            }

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

        private Profile GetProfile()
        {
            return _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
        }

        List<string> Disabled()
        {
            var field = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.DisabledPackages);
            return string.IsNullOrEmpty(field) ? null : field.Split(',').ToList();
        }
    }
}