using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("search")]
public class SearchController : Controller
{
  protected readonly ILogger _logger;
  protected readonly IMapper _mapper;
  protected readonly BlogManager _blogManager;

  public SearchController(
    ILogger<SearchController> logger,
    IMapper mapper,
    BlogManager blogManager)
  {
    _logger = logger;
    _mapper = mapper;
    _blogManager = blogManager;
  }

  [HttpPost]
  public async Task<IActionResult> Post(string term, int page = 1)
  {
    if (!string.IsNullOrEmpty(term))
    {
      var data = await _blogManager.GetAsync();
      var posts = await _blogManager.SearchPostsAsync(term, page, data.ItemsPerPage);
      var mainDto = _mapper.Map<MainDto>(data);
      var postsDto = _mapper.Map<IEnumerable<PostItemDto>>(posts);
      var model = new SearchModel(postsDto, page, mainDto);
      return View($"~/Views/Themes/{data.Theme}/search.cshtml", model);
    }
    else
    {
      return Redirect("~/");
    }
  }
}
