using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Blogifier.Core.Controllers
{
    [Route("author")]
	public class AuthorController : Controller
	{
		IUnitOfWork _db;
        ILogger _logger;
        private readonly string _theme = "~/Views/Blogifier/Blog/{0}/";

		public AuthorController(IUnitOfWork db, ILogger<AuthorController> logger)
		{
			_db = db;
            _logger = logger;
        }

        [Route("{slug}/{page:int?}")]
        public IActionResult Index(string slug, int page = 1)
        {
            var pager = new Pager(page);
            var profile = _db.Profiles.Single(p => p.Slug == slug);
            var posts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id && p.Published > System.DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return View(string.Format(_theme, profile.BlogTheme) + "Error.cshtml", 404);

            var categories = _db.Categories.CategoryMenu(c => c.PostCategories.Count > 0 && c.ProfileId == profile.Id, 10).ToList();

            return View(string.Format(_theme, profile.BlogTheme) + "Author.cshtml", 
                new BlogAuthorModel { Categories = categories, Profile = profile, Posts = posts, Pager = pager });
        }
    }
}
