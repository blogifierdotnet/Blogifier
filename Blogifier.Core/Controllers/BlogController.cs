using Blogifier.Core.Common;
using Blogifier.Core.Services.Data;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
            _theme = $"~/{ApplicationSettings.BlogThemesFolder}/{BlogSettings.Theme}/";
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await _ds.GetPosts(page);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Index.cshtml", model);
        }

        [Route("{slug:author}")]
        public async Task<IActionResult> PostsByAuthor(string slug, int page = 1)
        {
            var model = await _ds.GetPostsByAuthor(slug, page);
            if(model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + model.Profile.BlogTheme + "/Author.cshtml", model);
        }

        [Route("category/{cat}")]
        public async Task<IActionResult> AllPostsByCategory(string cat, int page = 1)
        {
            var model = await _ds.GetAllPostsByCategory(cat, page);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/{BlogSettings.Theme}/Category.cshtml", model);
        }

        [Route("{slug:author}/{cat}")]
        public async Task<IActionResult> PostsByCategory(string slug, string cat, int page = 1)
        {
            var model = await _ds.GetPostsByCategory(slug, cat, page);
            if(model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + model.Profile.BlogTheme + "/Category.cshtml", model);
        }

        [Route("{slug}")]
        public async Task<IActionResult> SinglePublication(string slug)
        {
            var model = await _ds.GetPostBySlug(slug);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + model.Profile.BlogTheme + "/Single.cshtml", model);
        }

        [Route("search/{term}")]
        public async Task<IActionResult> PagedSearch(string term, int page = 1)
        {
            ViewBag.Term = term;
            var model = await _ds.SearchPosts(term, page);

            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Search.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Search()
        {
            ViewBag.Term = HttpContext.Request.Form["term"];
            var model = await _ds.SearchPosts(ViewBag.Term, 1);

            return View(_theme + "Search.cshtml", model);
        }

        [Route("rss/{slug:author?}")]
        public async Task<IActionResult> Rss(string slug)
        {
            var absoluteUri = string.Concat(
                Request.Scheme, "://",
                Request.Host.ToUriComponent(),
                Request.PathBase.ToUriComponent());

            var x = slug;

            var rss = await _rss.Display(absoluteUri, slug);
            return Content(rss, "text/xml");
        }

        [Route("error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            return View(_theme + "Error.cshtml", statusCode);
        }
    }
}