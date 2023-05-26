using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Models;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class HomeController : Controller
{
  private readonly ILogger _logger;
  private readonly MainMamager _mainMamager;
  private readonly BlogManager _blogManager;

  public HomeController(
    ILogger<HomeController> logger,
    MainMamager mainMamager,
    BlogManager blogManager)
  {
    _logger = logger;
    _mainMamager = mainMamager;
    _blogManager = blogManager;
  }

  [HttpGet]
  public async Task<IActionResult> Index(int page = 1)
  {
    MainDto main;
    try
    {
      main = await _mainMamager.GetAsync();
    }
    catch (BlogNotIitializeException ex)
    {
      _logger.LogError(ex, "blgo not iitialize redirect");
      return Redirect("~/account/initialize");
    }
    var posts = await _blogManager.GetPostsAsync(page, main.ItemsPerPage);
    var model = new IndexModel(posts, page, main);
    return View($"~/Views/Themes/{main.Theme}/index.cshtml", model);
  }
}
