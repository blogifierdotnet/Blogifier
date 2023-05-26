using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("post")]
public class PostController : Controller
{
  protected readonly ILogger _logger;
  protected readonly IMapper _mapper;
  protected readonly BlogManager _blogManager;
  protected readonly MarkdigProvider _markdigProvider;
  public PostController(
    ILogger<HomeController> logger,
    IMapper mapper,
    BlogManager blogManager,
    MarkdigProvider markdigProvider)
  {
    _logger = logger;
    _mapper = mapper;
    _blogManager = blogManager;
    _markdigProvider = markdigProvider;
  }

  [HttpGet("{slug}")]
  public async Task<IActionResult> GetAsync([FromRoute] string slug)
  {
    var data = await _blogManager.GetAsync();
    var postSlug = await _blogManager.GetPostAsync(slug);
    if (postSlug.Post.State == PostState.Draft)
    {
      if (User.Identity == null || User.FirstUserId() != postSlug.Post.UserId)
        return Redirect("~/404");
    }
    else if (postSlug.Post.PostType == PostType.Page)
    {
      return Redirect($"~/page/{postSlug.Post.Slug}");
    }
    var mainDto = _mapper.Map<MainDto>(data);
    var postSluDto = _mapper.Map<PostSlugDto>(postSlug);
    postSluDto.Post.ContentHtml = _markdigProvider.ToHtml(postSlug.Post.Content);
    postSluDto.Post.DescriptionHtml = _markdigProvider.ToHtml(postSlug.Post.Description);

    foreach (var related in postSlug.Related)
    {
      var relatedDto = postSluDto.Related.First(m => m.Id == related.Id);
      relatedDto.DescriptionHtml = _markdigProvider.ToHtml(related.Description);
    }
    var categoriesUrl = Url.Content("~/category");
    var model = new PostDataModel(postSluDto, categoriesUrl, mainDto);
    return View($"~/Views/Themes/{data.Theme}/post.cshtml", model);
  }
}
