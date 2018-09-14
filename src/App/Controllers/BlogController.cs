using Core;
using Core.Data;
using Core.Helpers;
using Core.Services;
using Markdig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace App.Controllers
{
    public class BlogController : Controller
    {
        IDataService _db;
        IFeedService _ss;
        SignInManager<AppUser> _sm;
        private readonly ICompositeViewEngine _viewEngine;
        static readonly string _listView = $"~/Views/Themes/{AppSettings.Theme}/List.cshtml";

        public BlogController(IDataService db, IFeedService ss, SignInManager<AppUser> sm, ICompositeViewEngine viewEngine)
        {
            _db = db;
            _ss = ss;
            _sm = sm;
            _viewEngine = viewEngine;
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

            if (pager.ShowOlder) pager.LinkToOlder = $"blog?page={pager.Older}";
            if (pager.ShowNewer) pager.LinkToNewer = $"blog?page={pager.Newer}";

            var model = new ListModel {
                PostListType = PostListType.Blog,
                Posts = posts,
                Pager = pager
            };

            SetViewBag();

            if (!string.IsNullOrEmpty(term))
            {
                ViewBag.Title = term;
                ViewBag.Description = "";
                model.PostListType = PostListType.Search;
            }

            return View(_listView, model);
        }

        [Route("posts/{slug}")]
        public async Task<IActionResult> Single(string slug)
        {
            try
            {
                var model = await _db.BlogPosts.GetModel(slug);
                model.Post.Content = Markdown.ToHtml(model.Post.Content);

                ViewBag.Logo = $"{Url.Content("~/")}{AppSettings.Logo}";
                ViewBag.Cover = string.IsNullOrEmpty(model.Post.Cover) ? 
                    $"{Url.Content("~/")}{AppSettings.DefaultCover}" : 
                    $"{Url.Content("~/")}{model.Post.Cover}";
                ViewBag.Title = model.Post.Title;
                ViewBag.Description = model.Post.Description;

                return View($"~/Views/Themes/{AppSettings.Theme}/Post.cshtml", model);
            }
            catch
            {
                return Redirect("~/error/404");
            }
            
        }

        [Route("authors/{name}")]
        public async Task<IActionResult> Authors(string name, int page = 1)
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == name);

            var pager = new Pager(page);
            var posts = await _db.BlogPosts.GetList(p => p.Published > DateTime.MinValue && p.AuthorId == author.Id, pager);

            if (pager.ShowOlder) pager.LinkToOlder = $"authors/{name}?page={pager.Older}";
            if (pager.ShowNewer) pager.LinkToNewer = $"authors/{name}?page={pager.Newer}";

            var model = new ListModel {
                PostListType = PostListType.Author,
                Author = author,
                Posts = posts,
                Pager = pager
            };

            SetViewBag();

            ViewBag.Description = "";

            return View(_listView, model);
        }

        [Route("categories/{name}")]
        public async Task<IActionResult> Categories(string name, int page = 1)
        {
            var pager = new Pager(page);
            var posts = await _db.BlogPosts.GetListByCategory(name, pager);

            if (pager.ShowOlder) pager.LinkToOlder = $"categories/{name}?page={pager.Older}";
            if (pager.ShowNewer) pager.LinkToNewer = $"categories/{name}?page={pager.Newer}";

            var model = new ListModel {
                PostListType = PostListType.Category,
                Posts = posts,
                Pager = pager
            };

            SetViewBag();

            ViewBag.Category = name;
            ViewBag.Description = "";

            return View(_listView, model);
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
        public IActionResult Error(int code)
        {
            SetViewBag();

            var viewName = $"~/Views/Themes/{AppSettings.Theme}/Error.cshtml";
            var result = _viewEngine.GetView("", viewName, false);

            if (result.Success)
            {
                return View(viewName, code);
            }
            else
            {
                return View("~/Views/Shared/_Error.cshtml", code);
            }
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