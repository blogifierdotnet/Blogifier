using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Data;
using Blogifier.Core.Services.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Blogifier.Core.Controllers.Api.Public
{
    [Route("blogifier/api/public/[controller]")]
    public class PostsController : Controller
    {
        IDataService _ds;

        public PostsController(IDataService ds)
        {
            _ds = ds;
        }

        // Remove potentially private information from the PostListItem for the public API
        IEnumerable<PostListItem> SantizePostListItems(IEnumerable<PostListItem> posts)
        {
            foreach (PostListItem post in posts)
            {
                post.AuthorEmail = "";
            }
            return posts;
        }

        // GET blogifier/api/public/posts
        // GET blogifier/api/public/posts?page=2
        public BlogPostsModel Get(int page = 1)
        {
            return _ds.GetPosts(page, true);
        }

        // GET blogifier/api/public/posts/author/filip-stanek
        // GET blogifier/api/public/posts/author/filip-stanek?page=2
        [HttpGet("[action]/{slug}")]
        public BlogAuthorModel Author(string slug, int page = 1)
        {
            return _ds.GetPostsByAuthor(slug, page, true);
        }

        // GET blogifier/api/public/posts/author/category/mobile
        // GET blogifier/api/public/posts/author/category/mobile?page=2
        [HttpGet("[action]/{auth}/{cat}")]
        public BlogCategoryModel Category(string auth, string cat, int page = 1)
        {
            return _ds.GetPostsByCategory(auth, cat, page, true);
        }

        // GET blogifier/api/public/posts/search/dot%20net
        // GET blogifier/api/public/posts/search/dot%20net?page=2
        [HttpGet("[action]/{term}")]
        public BlogPostsModel Search(string term, int page = 1)
        {
            return _ds.SearchPosts(term, page, true);
        }

        // GET blogifier/api/public/posts/post/running-local-web-pages-in-cefsharpwpf
        [HttpGet("[action]/{slug}")]
        public BlogPostDetailModel Post(string slug)
        {
            return _ds.GetPostBySlug(slug, true);
        }
    }
}