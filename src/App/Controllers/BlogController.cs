using Core;
using Core.Data;
using Core.Services;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class BlogController : Controller
    {
        IUnitOfWork _db;
        ISearchService _ss;

        public BlogController(IUnitOfWork db, ISearchService ss)
        {
            _db = db;
            _ss = ss;
        }

        public async Task<IActionResult> Index(int page = 1, string term = "")
        {
            var pager = new Pager(page);
            IEnumerable<PostItem> posts;

            if (string.IsNullOrEmpty(term))
            {
                posts = await _db.BlogPosts.Find(p => p.Published > DateTime.MinValue, pager);
            }
            else
            {
                posts = await _ss.Find(pager, term);
            }

            var model = new PostListModel { Posts = posts, Pager = pager };

            ViewBag.Logo = $"{Url.Content("~/")}{AppSettings.Logo}";
            ViewBag.Cover = $"{Url.Content("~/")}{AppSettings.Cover}";
            ViewBag.Title = AppSettings.Title;
            ViewBag.Description = AppSettings.Description;

            return View($"~/Views/Themes/{AppSettings.Theme}/Index.cshtml", model);
        }

        [Route("blog/{slug}")]
        public async Task<IActionResult> Single(string slug)
        {
            var posts = await _db.BlogPosts.Find(p => p.Slug == slug, new Pager(1));
            var post = posts.FirstOrDefault();

            post.Content = Markdown.ToHtml(post.Content);

            ViewBag.Logo = $"{Url.Content("~/")}{AppSettings.Logo}";
            ViewBag.Cover = string.IsNullOrEmpty(post.Cover) ? $"{Url.Content("~/")}{AppSettings.DefaultCover}" : $"{Url.Content("~/")}{post.Cover}";
            ViewBag.Title = post.Title;
            ViewBag.Description = post.Description;

            return View($"~/Views/Themes/{AppSettings.Theme}/Single.cshtml", post);
        }
    }
}