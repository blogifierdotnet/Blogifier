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
    public class AuthorController : Controller
    {
        IUnitOfWork _db;
        ISocialService _social;
        ILogger _logger;

        public AuthorController(IUnitOfWork db, ISocialService social, ILogger<CategoryController> logger)
        {
            _db = db;
            _social = social;
            _logger = logger;
        }

        // GET blogifier/api/public/author/filip/1
        /// <summary>
        /// Gets posts (by page) by author
        /// </summary>
        [HttpGet("{slug}/{page}")]
        public BlogAuthorModel Get(string slug, int page = 1)
        {
            var pager = new Pager(page);
            var profile = _db.Profiles.Single(p => p.Slug == slug);
            var posts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id && p.Published > System.DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return null;

            var categories = _db.Categories.CategoryMenu(c => c.PostCategories.Count > 0 && c.ProfileId == profile.Id, 10).ToList();
            var social = _social.GetSocialButtons(profile).Result;

            return new BlogAuthorModel
            {
                Categories = categories,
                SocialButtons = social,
                Profile = profile,
                Posts = posts,
                Pager = pager
            };
        }
    }
}
