using Blogifier.Blogs;
using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
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
  public async Task<IActionResult> Post([FromQuery] string term, [FromQuery] int page = 1)
  {
    if (!string.IsNullOrEmpty(term))
    {
      var main = await _mainMamager.GetAsync();
      var pager = await _postProvider.GetSearchAsync(term, page, main.ItemsPerPage);
      pager.Configure(main.PathUrl, "page");
      var model = new SearchModel(pager, main);
      return View($"~/Views/Themes/{main.Theme}/search.cshtml", model);
    }
    else
    {
      return Redirect("~/");
    }
  }
}
