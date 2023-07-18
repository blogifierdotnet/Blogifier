using Blogifier.Blogs;
using Blogifier.Models;
using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class HomeController : Controller
{
  private readonly ILogger _logger;
  private readonly MainMamager _mainMamager;
  private readonly PostProvider _postProvider;

  public HomeController(
    ILogger<HomeController> logger,
    MainMamager mainMamager,
    PostProvider postProvider)
  {
    _logger = logger;
    _mainMamager = mainMamager;
    _postProvider = postProvider;
  }

  [HttpGet]
  public async Task<IActionResult> Index([FromQuery] int page = 1)
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
    var pager = await _postProvider.GetPostsAsync(page, main.ItemsPerPage);
    pager.Configure(main.PathUrl, "page");
    var model = new IndexModel(pager, main);
    return View($"~/Views/Themes/{main.Theme}/index.cshtml", model);
  }
}
