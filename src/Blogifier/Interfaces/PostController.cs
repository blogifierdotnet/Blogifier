using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[ApiController]
[Authorize]
[Route("api/post")]
public class PostController : ControllerBase
{
  private readonly PostProvider _postProvider;

  public PostController(PostProvider postProvider)
  {
    _postProvider = postProvider;
  }

  [HttpGet("list/{filter}/{postType}")]
  public async Task<IEnumerable<PostItemDto>> GetPosts(PublishedStatus filter, PostType postType)
  {
    return await _postProvider.GetAsync(filter, postType);
  }

  [HttpGet("list/search/{term}")]
  public async Task<ActionResult<List<Post>>> SearchPosts(string term)
  {
    return await _postProvider.SearchPosts(term);
  }

  [HttpGet("byslug/{slug}")]
  public async Task<PostEditorDto> GetPostBySlug(string slug)
  {
    return await _postProvider.GetEditorAsync(slug);
  }

  [HttpPost("add")]
  public async Task<PostEditorDto> AddPostAsync([FromBody] PostEditorDto post)
  {
    var userId = User.FirstUserId();
    return await _postProvider.AddAsync(post, userId);
  }

  [HttpPut("update")]
  public async Task<ActionResult<PostEditorDto>> UpdateAsync(PostEditorDto post)
  {
    var userId = User.FirstUserId();
    return await _postProvider.UpdateAsync(post, userId);
  }

  [HttpPut("publish/{id:int}")]
  public async Task<ActionResult<bool>> PublishPost(int id, [FromBody] bool publish)
  {
    return await _postProvider.Publish(id, publish);
  }

  [HttpPut("featured/{id:int}")]
  public async Task<ActionResult<bool>> FeaturedPost(int id, [FromBody] bool featured)
  {
    return await _postProvider.Featured(id, featured);
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult<bool>> RemovePost(int id)
  {
    return await _postProvider.Remove(id);
  }
}
