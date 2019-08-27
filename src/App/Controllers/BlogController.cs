using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace App.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BlogController : Controller
    {
        IDataService _db;
        IFeedService _ss;
        SignInManager<AppUser> _sm;
        private readonly ICompositeViewEngine _viewEngine;

        public BlogController(IDataService db, IFeedService ss, SignInManager<AppUser> sm, ICompositeViewEngine viewEngine)
        {
            _db = db;
            _ss = ss;
            _sm = sm;
            _viewEngine = viewEngine;
        }

        [HttpGet("feed/{type}")]
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
                        post.Description = post.Description.MdToHtml();
                        await writer.Write(post);
                    }
                }
            }
        }

        [HttpGet("error/{code:int}")]
        public async Task<IActionResult> Error(int code)
        {
            var model = new PostModel();

            model.Blog = await _db.CustomFields.GetBlogSettings();
            model.Blog.Cover = $"{Url.Content("~/")}{model.Blog.Cover}";

            var viewName = $"~/Views/Shared/Error.cshtml";
            var result = _viewEngine.GetView("", viewName, false);

            if (result.Success)
            {
                return View(viewName, model);
            }
            else
            {
                return View("~/Views/Shared/_Error.cshtml", model);
            }
        }

        [HttpGet("admin")]
        [Authorize]
        public IActionResult Admin()
        {
            return Redirect("~/admin/posts");
        }

        [HttpPost("account/logout")]
        public async Task<IActionResult> Logout()
        {
            await _sm.SignOutAsync();
            return Redirect("~/");
        }
    }
}