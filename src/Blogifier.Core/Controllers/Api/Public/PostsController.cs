using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Social;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blogifier.Core.Controllers.Api.Public
{
    [Route("blogifier/api/public/[controller]")]
    public class PostsController : Controller
    {
        IUnitOfWork _db;
        IRssService _rss;
        ISocialService _social;
        private readonly ILogger _logger;

        public PostsController(IUnitOfWork db, IRssService rss, ISocialService social, ILogger<BlogController> logger)
        {
            _db = db;
            _rss = rss;
            _social = social;
            _logger = logger;
        }
        
        // GET blogifier/api/public/posts
        // GET blogifier/api/public/posts?page=2
        /// <summary>
        /// Get posts by page
        /// </summary>
        public BlogBaseModel Get(int page = 1)
        {
            var pager = new Pager(page);
            var posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue, pager);
            var categories = _db.Categories.CategoryMenu(c => c.PostCategories.Count > 0, 10).ToList();
            var social = _social.GetSocialButtons(null).Result;

            if (page < 1 || page > pager.LastPage)
                return null;

            return new BlogPostsModel
            {
                Categories = categories,
                SocialButtons = social,
                Posts = posts,
                Pager = pager
            };
        }

        // GET blogifier/api/public/posts/running-local-web-pages-in-cefsharpwpf
        /// <summary>
        /// Get single post by slug
        /// </summary>
        [HttpGet("{slug}")]
        public BlogBaseModel Get(string slug)
        {
            if (String.IsNullOrEmpty(slug))
                return null;

            var vm = new BlogPostDetailModel();
            vm.BlogPost = _db.BlogPosts.SingleIncluded(p => p.Slug == slug && p.Published > DateTime.MinValue).Result;

            if (vm.BlogPost == null)
                return null;

            if (string.IsNullOrEmpty(vm.BlogPost.Image))
                vm.BlogPost.Image = ApplicationSettings.PostImage;

            vm.Profile = _db.Profiles.Single(b => b.Id == vm.BlogPost.ProfileId);
            vm.BlogCategories = new List<SelectListItem>();

            if (vm.BlogPost.PostCategories != null && vm.BlogPost.PostCategories.Count > 0)
            {
                foreach (var pc in vm.BlogPost.PostCategories)
                {
                    var cat = _db.Categories.Single(c => c.Id == pc.CategoryId);
                    vm.BlogCategories.Add(new SelectListItem { Value = cat.Slug, Text = cat.Title });
                }
            }

            vm.Categories = _db.Categories.CategoryMenu(c => c.PostCategories.Count > 0 && c.ProfileId == vm.Profile.Id, 10).ToList();
            vm.SocialButtons = _social.GetSocialButtons(vm.Profile).Result;

            vm.DisqusScript = _db.CustomFields.Single(f =>
                f.ParentId == vm.Profile.Id &&
                f.CustomKey == "disqus" &&
                f.CustomType == Data.Domain.CustomType.Profile);

            return vm;
        }
    }
}
