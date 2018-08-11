using Core;
using Core.Data;
using Core.Helpers;
using Core.Services;
using Markdig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace App.Controllers
{
    public class BlogController : Controller
    {
        IDataService _db;
        IFeedService _ss;
        SignInManager<AppUser> _sm;

        public BlogController(IDataService db, IFeedService ss, SignInManager<AppUser> sm)
        {
            _db = db;
            _ss = ss;
            _sm = sm;
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

            SetViewBag();

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
        public async Task<IActionResult> Authors(string name, int page = 1)
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == name);

            var pager = new Pager(page);
            var posts = await _db.BlogPosts.GetList(p => p.Published > DateTime.MinValue && p.AuthorId == author.Id, pager);
            
            var model = new AuthorPostListModel { Author = author, Posts = posts, Pager = pager };

            SetViewBag();

            return View($"~/Views/Themes/{AppSettings.Theme}/Author.cshtml", model);
        }

        [Route("categories/{name}")]
        public async Task<IActionResult> Categories(string name, int page = 1)
        {
            var pager = new Pager(page);
            var posts = await _db.BlogPosts.GetListByCategory(name, pager);

            var model = new PostListModel { Posts = posts, Pager = pager };

            SetViewBag();
            ViewData["category"] = name;

            return View($"~/Views/Themes/{AppSettings.Theme}/Category.cshtml", model);
        }

        [Route("feed/{type}")]
        public async Task Rss(string type)
        {
            Response.ContentType = "application/xml";
            string host = Request.Scheme + "://" + Request.Host;

            using (XmlWriter xmlWriter = XmlWriter.Create(Response.Body, new XmlWriterSettings() { Async = true, Indent = true }))
            {
                var posts = await _ss.GetEntries(type, host);

                if (posts != null && posts.Count() > 0)
                {
                    var lastUpdated = posts.FirstOrDefault().Published;
                    var writer = await _ss.GetWriter(type, host, xmlWriter);

                    foreach (var post in posts)
                    {
                        post.Description = Markdown.ToHtml(post.Description);
                        await writer.Write(post);
                    }
                }
            }
        }

        [Route("error/{code:int}")]
        public IActionResult Index(int code)
        {
            SetViewBag();

            return View("~/Views/Shared/_Error.cshtml", code);
        }

        [HttpPost, Route("account/logout")]
        public async Task<IActionResult> Logout()
        {
            await _sm.SignOutAsync();
            return Redirect("~/");
        }

        void SetViewBag()
        {
            ViewBag.Logo = $"{Url.Content("~/")}{AppSettings.Logo}";
            ViewBag.Cover = $"{Url.Content("~/")}{AppSettings.Cover}";
            ViewBag.Title = AppSettings.Title;
            ViewBag.Description = AppSettings.Description;
        }
    }
}