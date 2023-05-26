using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Models;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class HomeController : Controller
{
  protected readonly ILogger _logger;
  protected readonly IMapper _mapper;
  protected readonly BlogManager _blogManager;

  public HomeController(
    ILogger<HomeController> logger,
    IMapper mapper,
    BlogManager blogManager)
  {
    _logger = logger;
    _mapper = mapper;
    _blogManager = blogManager;
  }

  [HttpGet]
  public async Task<IActionResult> Index(int page = 1)
  {
    MainData data;
    try
    {
      data = await _blogManager.GetAsync();
    }
    catch (BlogNotIitializeException ex)
    {
      _logger.LogError(ex, "blgo not iitialize redirect");
      return Redirect("~/account/initialize");
    }

    var posts = await _blogManager.GetPostsAsync(page, data.ItemsPerPage);
    var mainDto = _mapper.Map<MainDto>(data);
    var postsDto = _mapper.Map<IEnumerable<PostItemDto>>(posts);
    var model = new IndexModel(postsDto, page, mainDto);
    return View($"~/Views/Themes/{data.Theme}/index.cshtml", model);
  }
}
