using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AnalyticsController : ControllerBase
	{
		private readonly IAnalyticsProvider _analyticsProvider;

		public AnalyticsController(IAnalyticsProvider analyticsProvider)
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
}
