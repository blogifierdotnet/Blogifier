using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers
{
    [Route("blog")]
	public class BlogController : Controller
	{
		IUnitOfWork _db;
        private readonly ILogger _logger;
        private readonly string _theme;

		public BlogController(IUnitOfWork db, ILogger<BlogController> logger)
		{
			_db = db;
            _logger = logger;
			_theme = "~/Views/Blogifier/Themes/Blog/Standard/";
        }

		public IActionResult Index()
		{
			var posts = _db.BlogPosts.All();
			return View(_theme + "Index.cshtml", posts);
		}

        [Route("{slug}")]
        public async Task<IActionResult> SinglePublication(string slug)
        {
            var vm = new BlogPostDetailModel();
            vm.BlogPost = await _db.BlogPosts.SingleIncluded(p => p.Slug == slug);

            if (vm.BlogPost == null)
                return View("Error");

            vm.BlogPost.Content = MakeLinksAbsolute(vm.BlogPost.Content);
            vm.Profile = _db.Profiles.Single(b => b.Id == vm.BlogPost.ProfileId);
            vm.Categories = new List<SelectListItem>();

            if (vm.BlogPost.PostCategories != null && vm.BlogPost.PostCategories.Count > 0)
            {
                foreach (var pc in vm.BlogPost.PostCategories)
                {
                    var cat = _db.Categories.Single(c => c.Id == pc.CategoryId);
                    vm.Categories.Add(new SelectListItem { Value = cat.Slug, Text = cat.Title });
                }
            }
            return View(_theme + "Single.cshtml", vm);
        }

        [Route("rss")]
        public async Task<IActionResult> Rss()
        {
            var pubs = await _db.BlogPosts.Find(p => p.Published > DateTime.MinValue && p.Published < DateTime.UtcNow, new Pager(1));

            var absoluteUri = string.Concat(
                Request.Scheme, "://",
                Request.Host.ToUriComponent(),
                Request.PathBase.ToUriComponent());

            var feed = new Feed()
            {
                Title = ApplicationSettings.Title,
                Description = ApplicationSettings.Description,
                Link = new Uri(absoluteUri + "/rss"),
                Copyright = "(c) " + DateTime.Now.Year
            };

            foreach (var post in pubs)
            {
                var postDetails = _db.BlogPosts.Single(p => p.Slug == post.Slug);

                var item = new FeedItem()
                {
                    Title = post.Title,
                    Body = MakeLinksAbsolute(postDetails.Content),
                    Link = new Uri(absoluteUri + "/blog/" + post.Slug),
                    Permalink = absoluteUri + "/blog/" + post.Slug,
                    PublishDate = post.Published,
                    Author = new Author() { Name = post.Name, Email = post.AuthorEmail }
                };

                if (post.Categories != null && post.Categories.Count > 0)
                {
                    foreach (var cat in post.Categories)
                    {
                        item.Categories.Add(cat.Value);
                    }
                }

                item.Comments = new Uri(absoluteUri + "/blog/" + post.Slug);
                feed.Items.Add(item);
            }

            var rss = feed.Serialize();
            return Content(rss, "text/xml");
        }

        string MakeLinksAbsolute(string body)
        {
            var absoluteUri = string.Concat(
                Request.Scheme, "://",
                Request.Host.ToUriComponent(),
                Request.PathBase.ToUriComponent());

            return body.Replace("src=\"blog-uploads/", "src=\"" + absoluteUri + "/blog-uploads/");
        }
    }
}
