using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace Blogifier.Core.Controllers
{
	[Route("admin")]
	public class AdminController : Controller
	{
		private readonly IHostingEnvironment _hostingEnvironment;

		public AdminController(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		public IActionResult Index()
		{
			var theme = "Standard";

			string webRootPath = _hostingEnvironment.WebRootPath;
			string contentRootPath = _hostingEnvironment.ContentRootPath;

			var adminThemesDir = System.IO.Path.Combine(contentRootPath, "Views\\Blogifier\\Themes\\Admin");

			var dirInfo = new System.IO.DirectoryInfo(adminThemesDir);
			var themes = new List<string>();
			themes.Add("Standard");

			foreach (var item in dirInfo.GetDirectories())
			{
				System.Diagnostics.Debug.WriteLine(item.ToString());
				themes.Add(item.Name);
			}

			ViewBag.Themes = themes;

			return View(string.Format("~/Views/Blogifier/Themes/Admin/{0}/Index.cshtml", theme));
		}
	}
}
