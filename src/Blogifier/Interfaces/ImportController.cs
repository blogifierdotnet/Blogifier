using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/import")]
[Authorize]
[ApiController]
public class ImportController : ControllerBase
{
  private readonly ImportProvider _importProvider;

  public ImportController(ImportProvider importProvider)
  {
    _importProvider = importProvider;
  }

  [HttpGet("rss")]
  public ImportDto Rss([FromQuery] ImportRssDto request, [FromServices] ImportRssProvider importRssProvider)
  {
    return importRssProvider.Analysis(request.FeedUrl);
  }

  [HttpPost("write")]
  public async Task<IEnumerable<PostItemDto>> Write([FromBody] ImportDto request)
  {
    var userId = User.FirstUserId();
    var webRoot = Url.Content("~/");
    return await _importProvider.Write(request, webRoot, userId);
  }
}
