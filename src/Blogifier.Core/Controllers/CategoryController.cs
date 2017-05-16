using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Controllers
{
    [Route("category")]
	public class CategoryController : Controller
	{
		IUnitOfWork _db;
        ILogger _logger;
        private readonly string _themePattern = "~/Views/Blogifier/Blog/{0}/";
        string _theme;

		public CategoryController(IUnitOfWork db, ILogger<CategoryController> logger)
		{
			_db = db;
            _logger = logger;
			_theme = string.Format(_themePattern, ApplicationSettings.BlogTheme);
        }

        [Route("{slug}")]
        public IActionResult Category(string slug)
        {
            var pager = new Pager(1);
            var posts = _db.BlogPosts.ByCategory(slug, pager);
            var category = _db.Categories.Single(c => c.Slug == slug);
            return View(_theme + "Category.cshtml", new BlogCategoryModel { Category = category, Posts = posts.Result, Pager = pager });
        }

        [Route("{slug}/{page:int}")]
        public IActionResult PagedCategory(string slug, int page)
        {
            var pager = new Pager(page);
            var posts = _db.BlogPosts.ByCategory(slug, pager);
            var category = _db.Categories.Single(c => c.Slug == slug);
            return View(_theme + "Category.cshtml", new BlogCategoryModel { Category = category, Posts = posts.Result, Pager = pager });
        }
    }
}
