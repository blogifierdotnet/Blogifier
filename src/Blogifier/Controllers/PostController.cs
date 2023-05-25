using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
  public async Task<IActionResult> GetAsync([FromRoute] string slug)
  {
    var data = await _blogManager.GetAsync();
    var post = await _blogManager.GetPostAsync(slug);
    if (post.State == PostState.Draft)
    {
      if (User.Identity == null || User.FirstUserId() != post.UserId)
        return Redirect("~/404");
    }
    //var model =
    throw new Exception();
  }
}
