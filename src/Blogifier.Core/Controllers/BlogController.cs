using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Core.Controllers
{
	[Route("blog")]
	public class BlogController : Controller
	{
		IUnitOfWork _db;
		private readonly string _theme;

		public BlogController(IUnitOfWork db)
		{
			_db = db;
			_theme = "~/Views/Blogifier/Themes/Blog/Standard/";
		}

		public IActionResult Index()
		{
			var posts = _db.Posts.All();
			return View(_theme + "Index.cshtml", posts);
		}
	}
}
