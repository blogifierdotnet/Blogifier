using System;
using System.Linq;
using System.Xml.Linq;
using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Blogifier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blogifier.Controllers
{
    public class IntegrationController : ControllerBase
    {
        private readonly IDataService _data;
        private readonly IOptionsMonitor<AppItem> _appSettingsMonitor;

        public IntegrationController(IDataService data, IOptionsMonitor<AppItem> appSettingsMonitor)
        {
            _data = data;
            _appSettingsMonitor = appSettingsMonitor;
        }
        
        [Route("sitemap")]
        [Produces("text/xml")]
        public IActionResult Sitemap()
        {
            var sitemapNamespace = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");

            var posts = _data.BlogPosts.Find(p => p.Published > DateTime.MinValue).OrderByDescending(p => p.Published);

            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement(sitemapNamespace + "urlset", 
                    from post in posts
                    select new XElement(sitemapNamespace + "url", 
                        new XElement(sitemapNamespace + "loc", GetPostUrl(post)),
                        new XElement(sitemapNamespace + "lastmod", GetPostDate(post)),
                        new XElement(sitemapNamespace + "changefreq", "monthly")
                    )
                )
            );
            
            return Content(doc.Declaration + Environment.NewLine + doc, "text/xml");
        }

        public string GetPostUrl(BlogPost post)
        {
            var sitemapBaseUri = Request.ExtractAbsoluteUri();

            return $"{sitemapBaseUri}/posts/{post.Slug}";
        }

        public string GetPostDate(BlogPost post)
        {
            return post.Published.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }
    }
}