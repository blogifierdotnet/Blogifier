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
  protected readonly ILogger _logger;
  protected readonly IMapper _mapper;
  protected readonly BlogManager _blogManager;

  public CategoryController(
    ILogger<CategoryController> logger,
    IMapper mapper,
    BlogManager blogManager)
  {
    _logger = logger;
    _mapper = mapper;
    _blogManager = blogManager;
  }


  [HttpGet("{category}")]
  public async Task<IActionResult> Category(string category, int page = 1)
  {
    var data = await _blogManager.GetAsync();
    var posts = await _blogManager.CategoryPostsAsync(category, page, data.ItemsPerPage);
    var mainDto = _mapper.Map<MainDto>(data);
    var postsDto = _mapper.Map<IEnumerable<PostItemDto>>(posts);
    var model = new SearchModel(postsDto, page, mainDto);
    return View($"~/Views/Themes/{data.Theme}/category.cshtml", model);
  }
}
