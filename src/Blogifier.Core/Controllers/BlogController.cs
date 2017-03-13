using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Core.Controllers
{
	[Route("blog")]
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
