using Blogifier.Blogs;
using Blogifier.Extensions;
using Blogifier.Models;
using Blogifier.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Blogifier.Controllers;

public class HomeController : Controller
{
  protected readonly BlogManager _blogManager;
  protected readonly BlogProvider _blogProvider;
  protected readonly PostProvider _postProvider;
  protected readonly FeedProvider _feedProvider;
  protected readonly AuthorProvider _authorProvider;
  protected readonly ThemeProvider _themeProvider;
  protected readonly ICompositeViewEngine _compositeViewEngine;

  public HomeController(
    BlogManager blogManager,
    BlogProvider blogProvider,
    PostProvider postProvider,
    FeedProvider feedProvider,
    AuthorProvider authorProvider,
    ThemeProvider themeProvider,
    ICompositeViewEngine compositeViewEngine)
  {
    _blogManager = blogManager;
    _blogProvider = blogProvider;
    _postProvider = postProvider;
    _feedProvider = feedProvider;
    _authorProvider = authorProvider;
    _themeProvider = themeProvider;
    _compositeViewEngine = compositeViewEngine;
  }

  public async Task<IActionResult> Index(int page = 1)
  {
    var data = await _blogManager.GetBlogDataAsync();
    var posts = await _blogManager.GetPostsAsync(page, data.ItemsPerPage);
    var request = HttpContext.Request;
    var url = $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}";
    var model = new IndexModel();
    return View($"~/Views/Themes/{model.Theme}/index.cshtml", model);
  }

  [HttpGet("/{slug}")]
  public async Task<IActionResult> Index(string slug)
  {
    if (!string.IsNullOrEmpty(slug)) return await GetSingleBlogPost(slug);
    return Redirect("~/");
  }

  [HttpPost]
  public async Task<IActionResult> Search(string term, int page = 1)
  {
    if (!string.IsNullOrEmpty(term))
    {
      var model = await GetBlogPosts(term, page);
      string viewPath = $"~/Views/Themes/{model.Blog.Theme}/search.cshtml";
      if (IsViewExists(viewPath))
        return View(viewPath, model);
      else
        return Redirect("~/home");
    }
    else
    {
      return Redirect("~/home");
    }
  }

  [HttpGet("categories/{category}")]
  public async Task<IActionResult> Categories(string category, int page = 1)
  {
    var model = await GetBlogPosts("", page, category);
    string viewPath = $"~/Views/Themes/{model.Blog.Theme}/category.cshtml";

    ViewBag.Category = category;

    if (IsViewExists(viewPath))
      return View(viewPath, model);

    return View($"~/Views/Themes/{model.Blog.Theme}/index.cshtml", model);
  }

  [HttpGet("posts/{slug}")]
  public async Task<IActionResult> Single(string slug)
  {
    return await GetSingleBlogPost(slug);
  }

  [HttpGet("error")]
  public async Task<IActionResult> Error()
  {
    try
    {
      var model = new PostModel();
      model.Blog = await _blogProvider.GetBlogItem();
      string viewPath = $"~/Views/Themes/{model.Blog.Theme}/404.cshtml";
      if (IsViewExists(viewPath)) return View(viewPath, model);
      return View($"~/Views/error.cshtml");
    }
    catch
    {
      return View($"~/Views/error.cshtml");
    }
  }

  [ResponseCache(Duration = 1200)]
  [HttpGet("feed/{type}")]
  public async Task<IActionResult> Rss(string type)
  {
    string host = Request.Scheme + "://" + Request.Host;
    var blog = await _blogProvider.GetBlog();

    var posts = await _feedProvider.GetEntries(type, host);
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

  private bool IsViewExists(string viewPath)
  {
    var result = _compositeViewEngine.GetView("", viewPath, false);
    return result.Success;
  }

  public async Task<IActionResult> GetSingleBlogPost(string slug)
  {
    try
    {
      ViewBag.Slug = slug;
      PostModel model = await _postProvider.GetPostModel(slug);

      // If unpublished and unauthorised redirect to error / 404.
      if (model.Post.Published == DateTime.MinValue && !User.Identity.IsAuthenticated)
      {
        return Redirect("~/error");
      }

      model.Blog = await _blogProvider.GetBlogItem();
      model.Post.Description = model.Post.Description.MdToHtml();
      model.Post.Content = model.Post.Content.MdToHtml();

      if (!model.Post.Author.Avatar.StartsWith("data:"))
        model.Post.Author.Avatar = Url.Content($"~/{model.Post.Author.Avatar}");

      if (model.Post.PostType == PostType.Page)
      {
        string viewPath = $"~/Views/Themes/{model.Blog.Theme}/page.cshtml";
        if (IsViewExists(viewPath))
          return View(viewPath, model);
      }

      return View($"~/Views/Themes/{model.Blog.Theme}/post.cshtml", model);
    }
    catch
    {
      return Redirect("~/error");
    }
  }

  private async Task<ListModel> GetBlogPosts(string term = "", int pager = 1, string category = "", string slug = "")
  {
    var model = new ListModel { };
    try
    {
      model.Blog = await _blogProvider.GetBlogItem();
    }
    catch
    {
      return null;
    }

    model.Pager = new Pager(pager, model.Blog.ItemsPerPage);

    if (!string.IsNullOrEmpty(category))
    {
      model.PostListType = PostListType.Category;
      model.Posts = await _postProvider.GetList(model.Pager, 0, category, "PF");
    }
    else if (string.IsNullOrEmpty(term))
    {
      model.PostListType = PostListType.Blog;
      if (model.Blog.IncludeFeatured)
        model.Posts = await _postProvider.GetList(model.Pager, 0, "", "FP");
      else
        model.Posts = await _postProvider.GetList(model.Pager, 0, "", "P");
    }
    else
    {
      model.PostListType = PostListType.Search;
      model.Blog.Title = term;
      model.Blog.Description = "";
      model.Posts = await _postProvider.Search(model.Pager, term, 0, "FP");
    }

    if (model.Pager.ShowOlder) model.Pager.LinkToOlder = $"?page={model.Pager.Older}";
    if (model.Pager.ShowNewer) model.Pager.LinkToNewer = $"?page={model.Pager.Newer}";

    return model;
  }
}
