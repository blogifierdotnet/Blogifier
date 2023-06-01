using Blogifier.Blogs;
using Blogifier.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Blogifier.Controllers;

public class FeedController : Controller
{
  private readonly ILogger _logger;
  private readonly BlogManager _blogManager;
  private readonly PostProvider _postProvider;
  private readonly MarkdigProvider _markdigProvider;

  public FeedController(
    ILogger<FeedController> logger,
    BlogManager blogManager,
    PostProvider postProvider,
    MarkdigProvider markdigProvider)
  {
    _logger = logger;
    _blogManager = blogManager;
    _postProvider = postProvider;
    _markdigProvider = markdigProvider;
  }

  [ResponseCache(Duration = 1200)]
  [HttpGet("feed")]
  public async Task<IActionResult> Rss()
  {
    var host = Request.Scheme + "://" + Request.Host;
    var data = await _blogManager.GetAsync();
    var posts = await _postProvider.GetAsync();
    var items = new List<SyndicationItem>();

    var publishedAt = DateTime.UtcNow;
    if (posts != null) foreach (var post in posts)
      {
        var url = $"{host}/posts/{post.Slug}";
        var description = _markdigProvider.ToHtml(post.Content);
        var item = new SyndicationItem(post.Title, description, new Uri(url), url, publishedAt)
        {
          PublishDate = publishedAt
        };
        items.Add(item);
      }
    var feed = new SyndicationFeed(data.Title, data.Description, new Uri(host), host, publishedAt)
    {
      Items = items
    };
    var settings = new XmlWriterSettings
    {
      Encoding = Encoding.UTF8,
      NewLineHandling = NewLineHandling.Entitize,
      NewLineOnAttributes = true,
      Indent = true
    };
    using var stream = new MemoryStream();
    using (var xmlWriter = XmlWriter.Create(stream, settings))
    {
      var rssFormatter = new Rss20FeedFormatter(feed, false);
      rssFormatter.WriteTo(xmlWriter);
      xmlWriter.Flush();
    }
    return File(stream.ToArray(), "application/xml; charset=utf-8");
  }
}
