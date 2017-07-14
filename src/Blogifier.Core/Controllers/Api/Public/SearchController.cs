using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Social;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Blogifier.Core.Controllers.Api.Public
{
    [Route("blogifier/api/public/[controller]")]
    public class SearchController : Controller
    {
        ISearchService _search;
        IUnitOfWork _db;
        ISocialService _social;

        public SearchController(ISearchService search, IUnitOfWork db, ISocialService social)
        {
            _search = search;
            _db = db;
            _social = social;
        }

        // GET blogifier/api/public/search/1/foo%20bar
        /// <summary>
        /// Gets posts (by page) for a search term
        /// </summary>
        [HttpGet("{page:int}/{term}")]
        public BlogPostsModel Get(int page, string term)
        {
            var model = new BlogPostsModel();
            model.Pager = new Pager(page);
            model.Posts = _search.Find(model.Pager, term).Result;
            model.Categories = _db.Categories.CategoryMenu(c => c.PostCategories.Count > 0, 10).ToList();
            model.SocialButtons = _social.GetSocialButtons(null).Result;

            if (page < 1 || page > model.Pager.LastPage)
                return null;

            return model;
        }
    }
}
