using Blogifier.Blogs;
using Blogifier.Posts;
using Blogifier.Providers;
using Blogifier.Shared;
using Blogifier.Storages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/storage")]
[ApiController]
public class StorageController : ControllerBase
{
  private readonly StorageProvider _storageProvider;
  private readonly BlogManager _blogManager;
  private readonly PostProvider _postProvider;

  public StorageController(
    StorageProvider storageProvider,
    BlogManager blogManager,
    PostProvider postProvider)
  {
    _storageProvider = storageProvider;
    _blogManager = blogManager;
    _postProvider = postProvider;
  }

  [Authorize]
  [HttpGet("themes")]
  public async Task<IList<string>> GetThemes()
  {
    return await _storageProvider.GetThemesAsync();
  }

  [Authorize]
  [HttpPut("exists")]
  public async Task<IActionResult> FileExists([FromBody] string path)
  {
    return (await Task.FromResult(_storageProvider.FileExistsAsync(path))) ? Ok() : BadRequest();
  }

  [Authorize]
  [HttpPost("upload/{uploadType}")]
  public async Task<ActionResult> Upload(IFormFile file, UploadType uploadType, int postId = 0)
  {
    var userId = User.FirstUserId();
    var post = postId == 0 ? new Post() : await _postProvider.GetPostById(postId);

    var path = $"{userId}/{DateTime.Now.Year}/{DateTime.Now.Month}";
    var fileName = $"data/{path}/{file.FileName}";

    if (uploadType == UploadType.PostImage)
      fileName = Url.Content("~/") + fileName;

    if (await _storageProvider.UploadFormFileAsync(file, path))
    {
      var blog = await _blogManager.GetAsync();

      switch (uploadType)
      {
        //case UploadType.Avatar:
        //  author.Avatar = fileName;
        //  return (await _authorProvider.Update(author)) ? new JsonResult(fileName) : BadRequest();
        case UploadType.AppLogo:
          blog.Logo = fileName;
          await _blogManager.SetAsync(blog);
          return new JsonResult(fileName);
        case UploadType.PostCover:
          post.Cover = fileName;
          return new JsonResult(fileName);
        case UploadType.PostImage:
          return new JsonResult(fileName);
      }
      return Ok();
    }
    else
    {
      return BadRequest();
    }
  }
}
