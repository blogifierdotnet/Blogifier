using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace App.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BlogController : Controller
    {
        IFeedService _ss;
        SignInManager<AppUser> _sm;

        public BlogController(IFeedService ss, SignInManager<AppUser> sm)
        {
            _ss = ss;
            _sm = sm;
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