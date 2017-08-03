using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Custom;
using Blogifier.Core.Services.Data;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
	public class BlogController : Controller
	{
        IRssService _rss;
        IDataService _ds;
        private readonly ILogger _logger;
        private readonly string _themePattern = "~/Views/Blogifier/Blog/{0}/";
        private readonly string _theme;

		public BlogController(IRssService rss, IDataService ds, ILogger<BlogController> logger)
		{
            _rss = rss;
            _ds = ds;
            _logger = logger;
			_theme = string.Format(_themePattern, ApplicationSettings.BlogTheme);
        }

        [Route("{page:int?}")]
        public IActionResult Index(int page = 1)
        {
            var model = _ds.GetPosts(page);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Index.cshtml", model);
        }

        [Route("{slug:author}/{page:int?}")]
        public IActionResult PostsByAuthor(string slug, int page = 1)
        {
            var model = _ds.GetPostsByAuthor(slug, page);
            if(model == null)
                return View(_theme + "Error.cshtml", 404);

            return View("~/Views/Blogifier/Blog/" + model.Profile.BlogTheme + "/Author.cshtml", model);
        }

        [Route("{slug:author}/{cat}/{page:int?}")]
        public IActionResult PostsByCategory(string slug, string cat, int page = 1)
        {
            var model = _ds.GetPostsByCategory(slug, cat, page);
            if(model == null)
                return View(_theme + "Error.cshtml", 404);

            return View("~/Views/Blogifier/Blog/" + model.Profile.BlogTheme + "/Category.cshtml", model);
        }

        [Route("{slug}")]
        public IActionResult SinglePublication(string slug)
        {
            var model = _ds.GetPostBySlug(slug);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View("~/Views/Blogifier/Blog/" + model.Profile.BlogTheme + "/Single.cshtml", model);
        }

        [Route("search/{term}/{page:int?}")]
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