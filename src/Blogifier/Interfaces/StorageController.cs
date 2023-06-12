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
  private readonly StorageProvider _storageProvider;

  public StorageController(
    StorageProvider storageProvider)
  {
    _storageProvider = storageProvider;
  }

  [HttpPut("exists")]
  public async Task<ActionResult> ExistsAsync([FromBody] string path)
  {
    if (await _storageProvider.ExistsFileAsync(path))
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
    return await _storageProvider.UploadAsync(currTime, userId, file);
  }
}
