using Blogifier.Posts;
using Blogifier.Shared;
using Blogifier.Storages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

  [HttpGet("items/{filter}/{postType}")]
  public async Task<IEnumerable<PostItemDto>> GetItemsAsync([FromRoute] PublishedStatus filter, [FromRoute] PostType postType)
  {
    return await _postProvider.GetAsync(filter, postType);
  }

  [HttpGet("items/search/{term}")]
  public async Task<IEnumerable<PostItemDto>> GetSearchAsync([FromRoute] string term)
  {
    return await _postProvider.GetSearchAsync(term);
  }

  [HttpGet("byslug/{slug}")]
  public async Task<PostEditorDto> GetPostBySlug(string slug)
  {
    return await _postProvider.GetEditorAsync(slug);
  }

  [HttpPost("add")]
  [RequestSizeLimit(128 * 1024 * 1024)]
  public async Task<string> AddPostAsync([FromServices] StorageManager storageManager, [FromBody] PostEditorDto post)
  {
    var userId = User.FirstUserId();
    var uploadAt = DateTime.UtcNow;
    if (!string.IsNullOrEmpty(post.Cover))
    {
      var coverUrl = await storageManager.UploadImagesBase64(uploadAt, userId, post.Cover);
      post.Cover = coverUrl;
    }
    var uploadContent = await storageManager.UploadImagesBase64FoHtml(uploadAt, userId, post.Content);
    post.Content = uploadContent;
    return await _postProvider.AddAsync(post, userId);
  }

  [HttpPut("update")]
  [RequestSizeLimit(128 * 1024 * 1024)]
  public async Task UpdateAsync([FromServices] StorageManager storageManager, [FromBody] PostEditorDto post)
  {
    var userId = User.FirstUserId();
    var uploadAt = DateTime.UtcNow;
    if (!string.IsNullOrEmpty(post.Cover))
    {
      var coverUrl = await storageManager.UploadImagesBase64(uploadAt, userId, post.Cover);
      post.Cover = coverUrl;
    }
    var uploadContent = await storageManager.UploadImagesBase64FoHtml(uploadAt, userId, post.Content);
    post.Content = uploadContent;
    await _postProvider.UpdateAsync(post, userId);
  }

  [HttpPut("state/{id:int}")]
  public async Task StateAsynct([FromRoute] int id, [FromBody] PostState state)
  {
    await _postProvider.StateAsynct(id, state);
  }

  [HttpPut("state/{idsString}")]
  public async Task StateAsynct([FromRoute] string idsString, [FromBody] PostState state)
  {
    var ids = idsString.Split(',').Select(int.Parse);
    await _postProvider.StateAsynct(ids, state);
  }

  [HttpDelete("{id:int}")]
  public async Task DeleteAsync([FromRoute] int id)
  {
    await _postProvider.DeleteAsync(id);
  }

  [HttpDelete("{idsString}")]
  public async Task DeleteAsync([FromRoute] string idsString)
  {
    var ids = idsString.Split(',').Select(int.Parse);
    await _postProvider.DeleteAsync(ids);
  }
}
