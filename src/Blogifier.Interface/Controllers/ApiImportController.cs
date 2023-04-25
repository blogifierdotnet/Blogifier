using Blogifier.Core.Data;
using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("api/import")]
[ApiController]
public class ApiImportController : ControllerBase
{
  private readonly AppDbContext _dbContext;
  private readonly IImportProvider _importProvider;

  public ApiImportController(AppDbContext dbContext, IImportProvider importProvider)
  {
    _dbContext = dbContext;
    _importProvider = importProvider;
  }

  [Authorize]
  [HttpGet("rss")]
  public List<Post> Rss([FromQuery] ImportRssModel request)
  {
    return _importProvider.Rss(request.FeedUrl);
  }

  [Authorize]
  [HttpPost("import")]
  public async Task<ActionResult<bool>> Import(Post post)
  {
    var author = await _dbContext.Authors.Where(a => a.Email == User.Identity!.Name).FirstAsync();
    var webRoot = Url.Content("~/");
    var success = await _importProvider.ImportPost(post);
    return success ? Ok() : BadRequest();
  }
}
