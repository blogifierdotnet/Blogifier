using Blogifier.Shared;
using Blogifier.Storages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/storage")]
[ApiController]
[Authorize]
public class StorageController : ControllerBase
{
  private readonly IStorageProvider _storageProvider;
  private readonly StorageManager _storageManager;

  public StorageController(
    IStorageProvider storageProvider,
    StorageManager storageManager)
  {
    _storageProvider = storageProvider;
    _storageManager = storageManager;
  }

  [HttpPut("exists")]
  public async Task<ActionResult> ExistsAsync([FromBody] string slug)
  {
    if (await _storageProvider.ExistsAsync(slug))
    {
      return Ok();
    }
    return BadRequest();
  }

  [HttpPost("upload")]
  public async Task<StorageDto?> Upload([FromForm] IFormFile file)
  {
    var userId = User.FirstUserId();
    var currTime = DateTime.UtcNow;
    return await _storageManager.UploadAsync(currTime, userId, file);
  }
}
