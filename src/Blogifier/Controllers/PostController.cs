using AutoMapper;
using Blogifier.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("post")]
public class PostController : Controller
{
  protected readonly ILogger _logger;
  protected readonly IMapper _mapper;
  protected readonly BlogManager _blogManager;
  public PostController(
    ILogger<HomeController> logger,
    IMapper mapper,
    BlogManager blogManager)
  {
    _logger = logger;
    _mapper = mapper;
    _blogManager = blogManager;
  }

  [HttpGet("{slug}")]
  public async Task<IActionResult> Single(string slug)
  {
    var data = await _blogManager.GetAsync();
    throw new BlogNotIitializeException();
  }
}
