using Blogifier.Data;
using Blogifier.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/import")]
[Authorize]
[ApiController]
public class ImportController : ControllerBase
{
  private readonly AppDbContext _dbContext;
  private readonly ImportProvider _importProvider;

  public ImportController(AppDbContext dbContext, ImportProvider importProvider)
  {
    _dbContext = dbContext;
    _importProvider = importProvider;
  }

  [HttpGet("rss")]
  public ImportDto Rss([FromQuery] ImportRssDto request)
  {
    return _importProvider.Rss(request.FeedUrl);
  }

  [HttpPost("write")]
  public async Task<ActionResult<bool>> Write(Post post)
  {
    var author = await _dbContext.Authors.Where(a => a.Email == User.Identity!.Name).FirstAsync();
    var webRoot = Url.Content("~/");
    var success = await _importProvider.ImportPost(post);
    return success ? Ok() : BadRequest();
  }
}
