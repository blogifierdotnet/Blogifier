using Blogifier.Core.Common;
using Blogifier.Core.Services.Data;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Controllers
{
    public class BlogController : Controller
	{
        IRssService _rss;
        IDataService _ds;
        private readonly ILogger _logger;
        private readonly string _theme;

		public BlogController(IRssService rss, IDataService ds, ILogger<BlogController> logger)
		{
            _rss = rss;
            _ds = ds;
            _logger = logger;
            _theme = $"~/{ApplicationSettings.BlogThemesFolder}/{ApplicationSettings.BlogTheme}/";
        }

        public IActionResult Index(int page = 1)
        {
            var model = _ds.GetPosts(page);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Index.cshtml", model);
        }

        [Route("{slug:author}")]
        public IActionResult PostsByAuthor(string slug, int page = 1)
        {
            var model = _ds.GetPostsByAuthor(slug, page);
            if(model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + model.Profile.BlogTheme + "/Author.cshtml", model);
        }

        [Route("category/{cat}")]
        public IActionResult AllPostsByCategory(string cat, int page = 1)
        {
            var model = _ds.GetAllPostsByCategory(cat, page);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + ApplicationSettings.AdminTheme + "/Category.cshtml", model);
        }

        [Route("{slug:author}/{cat}")]
        public IActionResult PostsByCategory(string slug, string cat, int page = 1)
        {
            var model = _ds.GetPostsByCategory(slug, cat, page);
            if(model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + model.Profile.BlogTheme + "/Category.cshtml", model);
        }

        [Route("{slug}")]
        public IActionResult SinglePublication(string slug)
        {
            var model = _ds.GetPostBySlug(slug);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + model.Profile.BlogTheme + "/Single.cshtml", model);
        }

        [Route("search/{term}")]
        public IActionResult PagedSearch(string term, int page = 1)
        {
            ViewBag.Term = term;
            var model = _ds.SearchPosts(term, page);

            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Search.cshtml", model);
        }

        [HttpPost]
        public IActionResult Search()
        {
            ViewBag.Term = HttpContext.Request.Form["term"];
            var model = _ds.SearchPosts(ViewBag.Term, 1);

            return View(_theme + "Search.cshtml", model);
        }

        [Route("rss/{slug:author?}")]
        public IActionResult Rss(string slug)
        {
            var absoluteUri = string.Concat(
                Request.Scheme, "://",
                Request.Host.ToUriComponent(),
                Request.PathBase.ToUriComponent());

            var x = slug;

            var rss = _rss.Display(absoluteUri, slug);
            return Content(rss, "text/xml");
        }

        [Route("error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            return View(_theme + "Error.cshtml", statusCode);
        }
    }
}