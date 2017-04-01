using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Controllers
{
    [Route("blog")]
	public class BlogController : Controller
	{
		IUnitOfWork _db;
        private readonly ILogger _logger;
        private readonly string _theme;

		public BlogController(IUnitOfWork db, ILogger<BlogController> logger)
		{
			_db = db;
            _logger = logger;
			_theme = "~/Views/Blogifier/Themes/Blog/Standard/";
		}

		public IActionResult Index()
		{
			var posts = _db.Posts.All();
			return View(_theme + "Index.cshtml", posts);
		}
	}
}
