using Blogifier.Core;
using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class StorageController : ControllerBase
{
  private readonly IStorageProvider _storageProvider;
  private readonly IAuthorProvider _authorProvider;
  private readonly IBlogProvider _blogProvider;
  private readonly IPostProvider _postProvider;

  public StorageController(
    IStorageProvider storageProvider,
    IAuthorProvider authorProvider,
    IBlogProvider blogProvider,
    IPostProvider postProvider)
  {
    _storageProvider = storageProvider;
    _authorProvider = authorProvider;
    _blogProvider = blogProvider;
    _postProvider = postProvider;
  }

  [HttpGet($"{BlogifierConstant.StorageObjectUrl}/{{**storageUrl}}")]
  [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
  [OutputCache(PolicyName = BlogifierConstant.OutputCacheExpire1)]
  public async Task<IActionResult> ObjectAsync([FromRoute] string storageUrl)
  {
    var memoryStream = new MemoryStream();
    var storage = await _storageProvider.GetAsync(storageUrl,
      (stream, cancellationToken) => stream.CopyToAsync(memoryStream, cancellationToken));
    if (storage == null) return NotFound();
    memoryStream.Position = 0;
    return File(memoryStream, storage.ContentType);
  }
}
