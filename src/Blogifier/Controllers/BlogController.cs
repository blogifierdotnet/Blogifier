using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Blogifier.Controllers
{
    public class BlogController : Controller
    {
        protected IDataService DataService;
        protected IStorageService StorageService;
        protected SignInManager<AppUser> SignInManager;
        protected IFeedService FeedService;

        public BlogController(IDataService data, IStorageService storage, SignInManager<AppUser> signInMgr, IFeedService feedService)
        {
            DataService = data;
            StorageService = storage;
            SignInManager = signInMgr;
            FeedService = feedService;
        }

        public async Task<IActionResult> Index(string term, int page = 1)
        {               
            var blog = await DataService.CustomFields.GetBlogSettings();
            var model = new ListModel { 
                Blog = blog, 
                PostListType = PostListType.Blog 
            };
            var pgr = new Pager(page, blog.ItemsPerPage);

            if (string.IsNullOrEmpty(term))
            {
                model.Posts = await DataService.BlogPosts.GetList(p => p.Published > DateTime.MinValue, pgr);
            }
            else
            {
                model.Posts = await DataService.BlogPosts.Search(pgr, term);
            }

            if (pgr.ShowOlder) pgr.LinkToOlder = $"blog?page={pgr.Older}";
            if (pgr.ShowNewer) pgr.LinkToNewer = $"blog?page={pgr.Newer}";

            model.Pager = pgr;

            if (!string.IsNullOrEmpty(term))
            {
                model.Blog.Title = term;
                model.Blog.Description = "";
                model.PostListType = PostListType.Search;
            }

            return View($"~/Views/Themes/{blog.Theme}/List.cshtml", model);
        }

        [HttpGet("categories/{name}")]
        public async Task<IActionResult> Categories(string name, int page = 1)
        {
            var blog = await DataService.CustomFields.GetBlogSettings();
            var model = new ListModel
            {
                Blog = blog,
                PostListType = PostListType.Category
            };
            var pgr = new Pager(page, blog.ItemsPerPage);

            model.Posts = await DataService.BlogPosts.GetList(p => p.Categories.Contains(name), pgr);

            if (pgr.ShowOlder) pgr.LinkToOlder = $"?page={pgr.Older}";
            if (pgr.ShowNewer) pgr.LinkToNewer = $"?page={pgr.Newer}";

            model.Pager = pgr;

            return View($"~/Views/Themes/{blog.Theme}/List.cshtml", model);
        }

        [HttpPost]
        public IActionResult Search(string term)
        {
            return Redirect($"/blog?term={term}");
        }

        [HttpGet("posts/{slug}")]
        public async Task<IActionResult> Single(string slug)
        {
            try
            {
                ViewBag.Slug = slug;
                var model = await DataService.BlogPosts.GetModel(slug);
                model.Blog = await DataService.CustomFields.GetBlogSettings();
                model.Post.Description = model.Post.Description.MdToHtml();
                model.Post.Content = model.Post.Content.MdToHtml();
                model.Disqus = DataService.CustomFields.GetCustomValue("disqus-key");

                return View($"~/Views/Themes/{model.Blog.Theme}/Post.cshtml", model);
            }
            catch
            {
                return Redirect("~/error");
            }
        }

        [ResponseCache(Duration = 1200)]
        [HttpGet("feed/{type}")]
        public async Task<IActionResult> Rss(string type)
        {
            string host = Request.Scheme + "://" + Request.Host;
            var blog = await DataService.CustomFields.GetBlogSettings();
            var posts = await FeedService.GetEntries(type, host);
            var items = new List<SyndicationItem>();

            var feed = new SyndicationFeed(
                blog.Title, 
                blog.Description, 
                new Uri(host), 
                host, 
                posts.FirstOrDefault().Published
            );

            if (posts != null && posts.Count() > 0)
            {
                foreach (var post in posts)
                {
                    var item = new SyndicationItem(
                        post.Title,
                        post.Description.MdToHtml(),
                        new Uri(post.Id),
                        post.Id,
                        post.Published
                    );
                    item.PublishDate = post.Published;
                    items.Add(item);
                }
            }
            feed.Items = items;

            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true
            };

            using (var stream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(stream, settings))
                {
                    var rssFormatter = new Rss20FeedFormatter(feed, false);
                    rssFormatter.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
                return File(stream.ToArray(), "application/xml; charset=utf-8");
            }
        }

        [HttpPost("upload/{uploadType}")]
        [Authorize]
        public async Task<ActionResult> Upload(IFormFile file, UploadType uploadType, int postId = 0)
        {
            var path = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Month);
            var asset = await StorageService.UploadFormFile(file, Url.Content("~/"), path);

            if(uploadType == UploadType.AppLogo)
            {
                var logo = DataService.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogLogo);
                if (logo == null)
                    DataService.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogLogo, Content = asset.Url });
                else
                    logo.Content = asset.Url;
                DataService.Complete();
            }
            if(uploadType == UploadType.AppCover)
            {
                var cover = DataService.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogCover);
                if (cover == null)
                    DataService.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogCover, Content = asset.Url });
                else
                    cover.Content = asset.Url;
                DataService.Complete();
            }
            if(uploadType == UploadType.PostCover)
            {
                await DataService.BlogPosts.SaveCover(postId, asset.Url);
                DataService.Complete();
            }
            if(uploadType == UploadType.Avatar)
            {
                var user = DataService.Authors.Single(a => a.AppUserName == User.Identity.Name);
                user.Avatar = asset.Url;
                DataService.Complete();
            }

            return Json(new { asset.Url });
        }

        [HttpPost]
        public IActionResult Subscribe(string email)
        {
            if (!string.IsNullOrEmpty(email) && email.IsEmail())
            {
                try
                {
                    var existing = DataService.Newsletters.Single(n => n.Email == email);
                    if (existing == null)
                    {
                        DataService.Newsletters.Add(new Newsletter { Email = email });
                        DataService.Complete();
                    }
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            return Ok();
        }

        [HttpGet("account/logout")]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return Redirect("~/blog");
        }
    }
}