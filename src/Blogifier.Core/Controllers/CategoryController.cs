using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Social;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
    [Route("category")]
	public class CategoryController : Controller
	{
		IUnitOfWork _db;
        ISocialService _social;
        ILogger _logger;
        private readonly string _themePattern = "~/Views/Blogifier/Blog/{0}/Category.cshtml";
        string _theme;

		public CategoryController(IUnitOfWork db, ISocialService social, ILogger<CategoryController> logger)
		{
			_db = db;
            _social = social;
            _logger = logger;
			_theme = string.Format(_themePattern, ApplicationSettings.BlogTheme);
        }

        [Route("{slug}/{page:int?}")]
        public async Task<IActionResult> PagedCategoryAsync(string slug, int page = 1)
        {
            var pager = new Pager(page);
            var posts = await _db.BlogPosts.ByCategory(slug, pager);

            if (page < 1 || page > pager.LastPage)
                return View(_theme + "Error.cshtml", 404);

            var category = _db.Categories.Single(c => c.Slug == slug);

            var categories = _db.Categories.CategoryMenu(c => c.PostCategories.Count > 0, 10).ToList();
            var social = _social.GetSocialButtons(null).Result;

            return View(_theme, new BlogCategoryModel { Categories = categories,
                SocialButtons = social, Category = category, Posts = posts, Pager = pager });
        }
    }
}
