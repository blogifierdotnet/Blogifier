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
  private readonly ImportManager _importManager;

  public ImportController(ImportManager importManager)
  {
    _importManager = importManager;
  }

  [HttpGet("rss")]
  public ImportDto Rss([FromQuery] ImportRssDto request, [FromServices] ImportRssProvider importRssProvider)
  {
    return importRssProvider.Analysis(request.FeedUrl);
  }

  [HttpPost("write")]
  public async Task<IEnumerable<PostEditorDto>> Write([FromBody] ImportDto request)
  {
    var userId = User.FirstUserId();
    return await _importManager.WriteAsync(request, userId);
  }
}
