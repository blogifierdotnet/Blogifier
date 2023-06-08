using Blogifier.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/analytics")]
[ApiController]
public class AnalyticsController : ControllerBase
{
  private readonly AnalyticsProvider _analyticsProvider;

  public AnalyticsController(AnalyticsProvider analyticsProvider)
  {
    _analyticsProvider = analyticsProvider;
  }

  [Authorize]
  [HttpGet]
  public async Task<AnalyticsModel> GetAnalytics()
  {
    return await _analyticsProvider.GetAnalytics();
  }

  [Authorize]
  [HttpPut("displayType/{typeId:int}")]
  public async Task<ActionResult<bool>> SaveDisplayType(int typeId)
  {
    return await _analyticsProvider.SaveDisplayType(typeId);
  }

  [Authorize]
  [HttpPut("displayPeriod/{typeId:int}")]
  public async Task<ActionResult<bool>> SaveDisplayPeriod(int typeId)
  {
    return await _analyticsProvider.SaveDisplayPeriod(typeId);
  }
}
