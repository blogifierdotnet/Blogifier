using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Controllers
{
    [Route("admin/settings")]
	public class SettingsController : Controller
	{
		IUnitOfWork _db;
        private readonly string _themePattern = "~/Views/Blogifier/Admin/{0}/Settings/";
        string _theme;

		public SettingsController(IUnitOfWork db, ILogger<AuthorController> logger)
		{
			_db = db;
			_theme = string.Format(_themePattern, ApplicationSettings.BlogTheme);
        }

        [Route("basic")]
        public IActionResult Basic()
        {
            return View(_theme + "Basic.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        [Route("general")]
        public IActionResult General()
        {
            return View(_theme + "General.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            return View(_theme + "Profile.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        [Route("import")]
        public IActionResult Import()
        {
            return View(_theme + "Import.cshtml", new AdminBaseModel { Profile = GetProfile() });
        }

        Profile GetProfile()
        {
            try
            {
                return _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
            }
            catch
            {
                RedirectToAction("Login", "Account");
            }
            return null;
        }
    }
}
