using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Core.Controllers
{
	[Route("blog")]
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
			var theme = "Standard";

			return View(string.Format("~/Views/Blogifier/Themes/Blog/{0}/Index.cshtml", theme));
		}
	}
}
