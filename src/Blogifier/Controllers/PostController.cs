using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("post")]
public class PostController : Controller
{
  protected readonly ILogger _logger;
  protected readonly IMapper _mapper;
  protected readonly MainMamager _mainMamager;
  protected readonly PostManager _postManager;
  public PostController(
    ILogger<PageController> logger,
    IMapper mapper,
    MainMamager mainMamager,
    PostManager postManager)
  {
    _logger = logger;
    _mapper = mapper;
    _mainMamager = mainMamager;
    _postManager = postManager;
  }

  [HttpGet("{slug}")]
  public async Task<IActionResult> GetAsync([FromRoute] string slug)
  {
    var main = await _mainMamager.GetAsync();
    var postSlug = await _postManager.GetToHtmlAsync(slug);
    if (postSlug.Post.State == PostState.Draft)
    {
      if (User.Identity == null || User.FirstUserId() != postSlug.Post.User.Id)
        return Redirect("~/404");
    }
    else if (postSlug.Post.PostType == PostType.Page)
    {
      return Redirect($"~/page/{postSlug.Post.Slug}");
    }
    var categoriesUrl = Url.Content("~/category");
    var model = new PostModel(postSlug, categoriesUrl, main);
    return View($"~/Views/Themes/{main.Theme}/post.cshtml", model);
  }
}
