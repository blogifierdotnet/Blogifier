using Blogifier.Blogs;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/analytics")]
[ApiController]
[Authorize]
public class AnalyticsController : ControllerBase
{
  private readonly AnalyticsProvider _analyticsProvider;
  public AnalyticsController(AnalyticsProvider analyticsProvider)
  {
    _analyticsProvider = analyticsProvider;
  }

  [HttpGet]
  public async Task<AnalyticsDto> GetAnalytics()
  {
    var blogs = await _analyticsProvider.GetPostSummaryAsync();
    return new AnalyticsDto { Blogs = blogs };
  }

  //[HttpPut("displayType/{typeId:int}")]
  //public async Task SaveDisplayType(int typeId)
  //{
  //  await _analyticsProvider.SaveDisplayType(typeId);
  //}

  //[HttpPut("displayPeriod/{typeId:int}")]
  //public async Task SaveDisplayPeriod(int typeId)
  //{
  //  await _analyticsProvider.SaveDisplayPeriod(typeId);
  //}
}
