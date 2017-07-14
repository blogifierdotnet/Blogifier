using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Social;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Blogifier.Core.Controllers.Api.Public
{
    [Route("blogifier/api/public/[controller]")]
    public class CategoryController : Controller
    {
        IUnitOfWork _db;
        ISocialService _social;
        ILogger _logger;

        public CategoryController(IUnitOfWork db, ISocialService social, ILogger<CategoryController> logger)
        {
            _db = db;
            _social = social;
            _logger = logger;
        }

        // GET blogifier/api/public/category/books/1
        /// <summary>
        /// Gets posts (by page) in a category
        /// </summary>
        [HttpGet("{slug}/{page}")]
        public BlogCategoryModel Get(string slug, int page = 1)
        {
            var pager = new Pager(page);
            var posts = _db.BlogPosts.ByCategory(slug, pager).Result;

            if (page < 1 || page > pager.LastPage)
                return null;

            var category = _db.Categories.Single(c => c.Slug == slug);

            var categories = _db.Categories.CategoryMenu(c => c.PostCategories.Count > 0, 10).ToList();
            var social = _social.GetSocialButtons(null).Result;

            return new BlogCategoryModel
            {
                Categories = categories,
                SocialButtons = social,
                Category = category,
                Posts = posts,
                Pager = pager
            };
        }
    }
}
