using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("search")]
public class SearchController : Controller
{
  private readonly MainMamager _mainMamager;
  private readonly PostProvider _postProvider;

  public SearchController(
    MainMamager mainMamager,
    PostProvider postProvider)
  {
    _mainMamager = mainMamager;
    _postProvider = postProvider;
  }

  [HttpPost]
  public async Task<IActionResult> Post(string term, int page = 1)
  {
    if (!string.IsNullOrEmpty(term))
    {
      var main = await _mainMamager.GetAsync();
      var posts = await _postProvider.SearchAsync(term, page, main.ItemsPerPage);
      var model = new SearchModel(posts, page, main);
      return View($"~/Views/Themes/{main.Theme}/search.cshtml", model);
    }
    else
    {
      return Redirect("~/");
    }
  }
}
