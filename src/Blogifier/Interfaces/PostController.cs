using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Models;
using Blogifier.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/post")]
[ApiController]
public class PostController : ControllerBase
{
  private readonly PostProvider _postProvider;
  protected readonly IMapper _mapper;
  protected readonly BlogManager _blogManager;

  public PostController(PostProvider postProvider, IMapper mapper, BlogManager blogManager)
  {
    _postProvider = postProvider;
    _mapper = mapper;
    _blogManager = blogManager;
  }

  [HttpGet("list/{filter}/{postType}")]
  public async Task<IEnumerable<PostItemDto>> GetPosts(PublishedStatus filter, PostType postType)
  {
    var posts = await _blogManager.GetPostsAsync(filter, postType);
    var postsDto = _mapper.Map<IEnumerable<PostItemDto>>(posts);
    return postsDto;
  }

  [HttpGet("list/search/{term}")]
  public async Task<ActionResult<List<Post>>> SearchPosts(string term)
  {
    return await _postProvider.SearchPosts(term);
  }

  [HttpGet("byslug/{slug}")]
  public async Task<ActionResult<Post?>> GetPostBySlug(string slug)
  {
    return await _postProvider.GetPostBySlug(slug);
  }

  [HttpGet("getslug/{title}")]
  public async Task<ActionResult<string>> GetSlug(string title)
  {
    return await _postProvider.GetSlugFromTitle(title);
  }

  [Authorize]
  [HttpPost("add")]
  public async Task<PostEditorDto> AddPostAsync([FromBody] PostEditorDto postDto)
  {
    var userId = User.FirstUserId();
    var post = _mapper.Map<Post>(postDto);
    post.UserId = userId;
    var result = await _blogManager.AddPostAsync(post);
    var resultDto = _mapper.Map<PostEditorDto>(result);
    return resultDto;
  }

  [Authorize]
  [HttpPut("update")]
  public async Task<ActionResult<PostEditorDto>> UpdatePostAsync(PostEditorDto postDto)
  {
    var userId = User.FirstUserId();
    var post = _mapper.Map<Post>(postDto);
    var result = await _blogManager.UpdatePostAsync(post, userId);
    var resultDto = _mapper.Map<PostEditorDto>(result);
    return resultDto;
  }

  [Authorize]
  [HttpPut("publish/{id:int}")]
  public async Task<ActionResult<bool>> PublishPost(int id, [FromBody] bool publish)
  {
    return await _postProvider.Publish(id, publish);
  }

  [Authorize]
  [HttpPut("featured/{id:int}")]
  public async Task<ActionResult<bool>> FeaturedPost(int id, [FromBody] bool featured)
  {
    return await _postProvider.Featured(id, featured);
  }

  [Authorize]
  [HttpDelete("{id:int}")]
  public async Task<ActionResult<bool>> RemovePost(int id)
  {
    return await _postProvider.Remove(id);
  }
}
