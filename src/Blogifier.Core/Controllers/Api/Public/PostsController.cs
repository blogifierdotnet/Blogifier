using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blogifier.Core.Controllers.Api.Public
{
    [Route("blogifier/api/public/[controller]")]
    public class PostsController : Controller
    {
        ISearchService _search;
        IUnitOfWork _db;
        private readonly ILogger _logger;

        public PostsController(ISearchService search, IUnitOfWork db, ILogger<BlogController> logger)
        {
            _search = search;
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Remove potentially private information from the PostListItem for the public API
        /// </summary>
        IEnumerable<PostListItem> SantizePostListItems(IEnumerable<PostListItem> posts)
        {
            foreach(PostListItem post in posts)
            {
                post.AuthorEmail = "";
                post.Categories = null;
            }
            return posts;
        }

        // GET blogifier/api/public/posts
        // GET blogifier/api/public/posts?page=2
        /// <summary>
        /// Get posts by page
        /// </summary>
        public BlogPostsModel Get(int page = 1)
        {
            var pager = new Pager(page);
            IEnumerable<PostListItem> posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return null;

            posts = SantizePostListItems(posts);

            return new BlogPostsModel
            {
                Posts = posts,
                Pager = pager
            };
        }

        // GET blogifier/api/public/posts/author/filip-stanek
        // GET blogifier/api/public/posts/author/filip-stanek?page=2
        /// <summary>
        /// Get posts for author, by page
        /// </summary>
        [HttpGet("[action]/{slug}")]
        public BlogPostsModel Author(string slug, int page = 1)
        {
            var pager = new Pager(page);
            var profile = _db.Profiles.Single(p => p.Slug == slug);
            IEnumerable<PostListItem> posts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id && p.Published > System.DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return null;

            posts = SantizePostListItems(posts);

            return new BlogPostsModel
            {
                Posts = posts,
                Pager = pager
            };
        }

        // GET blogifier/api/public/posts/category/mobile
        // GET blogifier/api/public/posts/category/mobile?page=2
        /// <summary>
        /// Get posts in category, by page
        /// </summary>
        [HttpGet("[action]/{slug}")]
        public BlogPostsModel Category(string slug, int page = 1)
        {
            var pager = new Pager(page);
            IEnumerable<PostListItem> posts = _db.BlogPosts.ByCategory(slug, pager).Result;

            if (page < 1 || page > pager.LastPage)
                return null;

            posts = SantizePostListItems(posts);

            return new BlogPostsModel
            {
                Posts = posts,
                Pager = pager
            };
        }

        // GET blogifier/api/public/posts/search/dot%20net
        // GET blogifier/api/public/posts/search/dot%20net?page=2
        /// <summary>
        /// Search the posts for terms, by page
        /// </summary>
        [HttpGet("[action]/{term}")]
        public BlogPostsModel Search(string term, int page = 1)
        {
            var pager = new Pager(page);
            IEnumerable<PostListItem> posts = _search.Find(pager, term).Result;

            if (page < 1 || page > pager.LastPage)
                return null;

            posts = SantizePostListItems(posts);

            return new BlogPostsModel
            {
                Posts = posts,
                Pager = pager
            };
        }

        // GET blogifier/api/public/posts/post/running-local-web-pages-in-cefsharpwpf
        /// <summary>
        /// Get single post by slug
        /// </summary>
        [HttpGet("[action]/{slug}")]
        public BlogPost Post(string slug)
        {
            if (String.IsNullOrEmpty(slug))
                return null;

            BlogPost post = _db.BlogPosts.SingleIncluded(p => p.Slug == slug && p.Published > DateTime.MinValue).Result;

            if (post == null)
                return null;

            if (string.IsNullOrEmpty(post.Image))
                post.Image = ApplicationSettings.PostImage;

            // sanitize the post so that any potentially private information is not contained
            post.Profile.AuthorEmail = "";
            post.Profile.IdentityName = "";
            post.Profile.IsAdmin = false;

            // clear variables which will cause looping issues during serialization 
            post.Profile.BlogPosts = null;
            post.Profile.Assets = null;
            foreach(PostCategory category in post.PostCategories)
            {
                category.BlogPost = null;
            }

            return post;
        }
    }
}
