using Blogifier.Blogs;
using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("category")]
public class CategoryController : Controller
{
  private readonly MainMamager _mainMamager;
  private readonly PostProvider _postProvider;

  public CategoryController(
    MainMamager mainMamager,
    PostProvider postProvider)
  {
    _mainMamager = mainMamager;
    _postProvider = postProvider;
  }

  [HttpGet("{category}")]
  public async Task<IActionResult> Category([FromRoute] string category, [FromQuery] int page = 1)
  {
    var main = await _mainMamager.GetAsync();
    var pager = await _postProvider.GetByCategoryAsync(category, page, main.ItemsPerPage);
    pager.Configure(main.PathUrl, "page");
    var model = new CategoryModel(category, pager, main);
    return View($"~/Views/Themes/{main.Theme}/category.cshtml", model);
  }
}
