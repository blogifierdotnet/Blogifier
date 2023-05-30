using Blogifier.Storages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.IO;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class StorageController : ControllerBase
{
  private readonly StorageProvider _storageManager;

  public StorageController(
    StorageProvider storageManager)
  {
    _storageManager = storageManager;
  }

  [HttpGet($"{BlogifierConstant.StorageObjectUrl}/{{**storageUrl}}")]
  [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
  [OutputCache(PolicyName = BlogifierConstant.OutputCacheExpire1)]
  public async Task<IActionResult> ObjectAsync([FromRoute] string storageUrl)
  {
    var memoryStream = new MemoryStream();
    var storage = await _storageManager.GetAsync(storageUrl,
      (stream, cancellationToken) => stream.CopyToAsync(memoryStream, cancellationToken));
    if (storage == null) return NotFound();
    memoryStream.Position = 0;
    return File(memoryStream, storage.ContentType);
  }
}
