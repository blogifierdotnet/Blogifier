using Core.Services;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace App.Controllers
{
    public class SharedController : Controller
    {
        ISyndicationService _feed;

        public SharedController(ISyndicationService feed)
        {
            _feed = feed;
        }

        [Route("/admin")]
        public IActionResult Admin()
        {
            return RedirectToAction(nameof(Index), nameof(Content));
        }

        [Route("/error/{code:int}")]
        public IActionResult Index(int code)
        {
            return View("~/Views/Shared/_Error.cshtml", code);
        }

        [Route("/feed/{type}")]
        public async Task Rss(string type)
        {
            Response.ContentType = "application/xml";
            string host = Request.Scheme + "://" + Request.Host;

            using (XmlWriter xmlWriter = XmlWriter.Create(Response.Body, new XmlWriterSettings() { Async = true, Indent = true }))
            {
                var posts = await _feed.GetEntries(type, host);

                if(posts != null && posts.Count() > 0)
                {
                    var lastUpdated = posts.FirstOrDefault().Published;
                    var writer = await _feed.GetWriter(type, host, xmlWriter);

                    foreach (var post in posts)
                    {
                        post.Description = Markdown.ToHtml(post.Description);
                        await writer.Write(post);
                    }
                }
            }
        }
    }
}