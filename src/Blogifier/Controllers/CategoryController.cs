using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("category")]
public class CategoryController : Controller
{
  private readonly ILogger _logger;
  private readonly IMapper _mapper;
  private readonly MainMamager _mainMamager;
  private readonly BlogManager _blogManager;

  public CategoryController(
    ILogger<CategoryController> logger,
    IMapper mapper,
    MainMamager mainMamager,
    BlogManager blogManager)
  {
    _logger = logger;
    _mapper = mapper;
    _mainMamager = mainMamager;
    _blogManager = blogManager;
  }

  [HttpGet("{category}")]
  public async Task<IActionResult> Category(string category, int page = 1)
  {
    var main = await _mainMamager.GetAsync();
    var posts = await _blogManager.CategoryPostsAsync(category, page, main.ItemsPerPage);
    var postsDto = _mapper.Map<IEnumerable<PostItemDto>>(posts);
    var model = new CategoryModel(category, postsDto, page, main);
    return View($"~/Views/Themes/{main.Theme}/category.cshtml", model);
  }
}
