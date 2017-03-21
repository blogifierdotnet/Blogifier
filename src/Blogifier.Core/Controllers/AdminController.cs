using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Core.Controllers
{
	[Route("admin")]
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			var theme = "Standard";

			return View(string.Format("~/Views/Blogifier/Themes/Admin/{0}/Index.cshtml", theme));
		}
	}
}
