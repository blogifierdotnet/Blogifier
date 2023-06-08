using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blogifier.Controllers;

public class SitemapController : ControllerBase
{
  private readonly PostProvider _postProvider;

  public SitemapController(PostProvider postProvider)
  {
    _postProvider = postProvider;
  }

  [Route("sitemap")]
  [Produces("text/xml")]
  public async Task<IActionResult> Sitemap()
  {
    var sitemapNamespace = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");
    var posts = await _postProvider.GetAsync();
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

  private string GetPostUrl(PostDto post)
  {
    string webRoot = Url.Content("~/");
    var sitemapBaseUri = $"{Request.Scheme}://{Request.Host}{webRoot}";
    return $"{sitemapBaseUri}posts/{post.Slug}";
  }

  private string GetPostDate(PostDto post)
  {
    return post.PublishedAt!.Value.ToString("yyyy-MM-ddTHH:mm:sszzz");
  }
}
