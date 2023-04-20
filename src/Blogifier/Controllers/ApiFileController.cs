using Blogifier.Core;
using Blogifier.Core.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.IO;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("api/file")]
[ApiController]
public class ApiFileController : ControllerBase
{
  private readonly IStorageProvider _storageProvider;
  public ApiFileController(IStorageProvider  storageProvider)
  {
    _storageProvider = storageProvider;
  }

  [HttpGet($"{BlogifierConstant.FileObjectPath}/{{**objectName}}")]
  [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
  [OutputCache(PolicyName = BlogifierConstant.OutputCacheExpire1)]
  public async Task<FileStreamResult> GetAsync([FromRoute] string objectName)
  {
    var memoryStream = new MemoryStream();
    var stat = await _storageProvider.GetObjectAsync(objectName, async (s, cancellationToken) => await s.CopyToAsync(memoryStream, cancellationToken));
    memoryStream.Position = 0;
    return File(memoryStream, stat.ContentType);
  }
}
