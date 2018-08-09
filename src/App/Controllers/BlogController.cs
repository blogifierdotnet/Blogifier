using Core;
using Core.Data;
using Core.Helpers;
using Core.Services;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class BlogController : Controller
    {
        IDataService _db;

        public BlogController(IDataService db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int page = 1, string term = "")
        {
            var pager = new Pager(page);
            IEnumerable<PostItem> posts;

            if (string.IsNullOrEmpty(term))
            {
                posts = await _db.BlogPosts.GetList(p => p.Published > DateTime.MinValue, pager);
            }
            else
            {
                posts = await _db.BlogPosts.Search(pager, term);
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
            var post = await _db.BlogPosts.GetItem(p => p.Slug == slug);

            post.Content = Markdown.ToHtml(post.Content);
            
            ViewBag.Logo = $"{Url.Content("~/")}{AppSettings.Logo}";
            ViewBag.Cover = string.IsNullOrEmpty(post.Cover) ? $"{Url.Content("~/")}{AppSettings.DefaultCover}" : $"{Url.Content("~/")}{post.Cover}";
            ViewBag.Title = post.Title;
            ViewBag.Description = post.Description;

            return View($"~/Views/Themes/{AppSettings.Theme}/Single.cshtml", post);
        }

        [Route("authors/{name}")]
        public async Task<IActionResult> Authors(string name, int page)
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == name);

            var pager = new Pager(page);
            var posts = await _db.BlogPosts.GetList(p => p.Published > DateTime.MinValue && p.AuthorId == author.Id, pager);
            
            var model = new AuthorPostListModel { Author = author, Posts = posts, Pager = pager };

            ViewBag.Logo = $"{Url.Content("~/")}{AppSettings.Logo}";
            ViewBag.Cover = $"{Url.Content("~/")}{AppSettings.Cover}";
            ViewBag.Title = AppSettings.Title;
            ViewBag.Description = AppSettings.Description;

            return View($"~/Views/Themes/{AppSettings.Theme}/Author.cshtml", model);
        }
    }
}