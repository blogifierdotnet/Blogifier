using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
    [Route("search")]
    public class SearchController : Controller
    {
        ISearchService _search;
        IUnitOfWork _db;
        private readonly string _themePattern = "~/Views/Blogifier/Blog/{0}/";
        string _theme;

        public SearchController(ISearchService search, IUnitOfWork db)
        {
            _search = search;
            _db = db;
            _theme = string.Format(_themePattern, ApplicationSettings.BlogTheme);
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            ViewBag.Term = HttpContext.Request.Form["term"];

            var model = new BlogPostsModel();
            model.Pager = new Pager(1);
            model.Posts = await _search.Find(model.Pager, ViewBag.Term);
            model.Categories = _db.Categories.CategoryMenu(c => c.PostCategories.Count > 0, 10).ToList();

            return View(_theme + "Search.cshtml", model);
        }

        [Route("{page:int}/{term}")]
        public async Task<IActionResult> PagedSearch(int page, string term)
        {
            ViewBag.Term = term;

            var model = new BlogPostsModel();
            model.Pager = new Pager(page);
            model.Posts = await _search.Find(model.Pager, ViewBag.Term);
            model.Categories = _db.Categories.CategoryMenu(c => c.PostCategories.Count > 0, 10).ToList();

            if (page < 1 || page > model.Pager.LastPage)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Search.cshtml", model);
        }
    }
}
