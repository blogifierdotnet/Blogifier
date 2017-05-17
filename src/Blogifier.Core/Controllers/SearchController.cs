using Blogifier.Core.Common;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Search;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
    [Route("search")]
    public class SearchController : Controller
    {
        ISearchService _search;
        private readonly string _themePattern = "~/Views/Blogifier/Blog/{0}/";
        string _theme;

        public SearchController(ISearchService search)
        {
            _search = search;
            _theme = string.Format(_themePattern, ApplicationSettings.BlogTheme);
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            ViewBag.Term = HttpContext.Request.Form["term"];

            var model = new BlogPostsModel();
            model.Pager = new Pager(1);
            model.Posts = await _search.Find(model.Pager, ViewBag.Term);

            return View(_theme + "Search.cshtml", model);
        }

        [Route("{page:int}/{term}")]
        public async Task<IActionResult> PagedSearch(int page, string term)
        {
            ViewBag.Term = term;

            var model = new BlogPostsModel();
            model.Pager = new Pager(page);
            model.Posts = await _search.Find(model.Pager, ViewBag.Term);

            if (page < 1 || page > model.Pager.LastPage)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Search.cshtml", model);
        }
    }
}
