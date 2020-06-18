using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Blogifier.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        IDataService _data;

        public AnalyticsController(IDataService data)
        {
            _data = data;
        }

        [HttpGet("totals")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StatsTotal>>> Totals([FromQuery] int days = 7)
        {
            try
            {
                IEnumerable<StatsTotal> stats = days == 0 ? _data.StatsRepository.All() :
                    _data.StatsRepository.Find(p => p.DateCreated >= SystemClock.Now().Date.AddDays(days));

                return Ok(await Task.FromResult(stats.OrderBy(s => s.DateCreated)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("counts")]
        [Authorize]
        public async Task<ActionResult<Totals>> Counts()
        {
            try
            {
                Totals totals = new Totals
                {
                    PostCount = _data.BlogPosts.All().Count(),
                    ViewsCount = _data.BlogPosts.All().Select(v => v.PostViews).Sum(),
                    DraftCount = _data.BlogPosts.Find(p => p.Published == DateTime.MinValue).Count(),
                    SubsriberCount = _data.Newsletters.All().Count()
                };
                return Ok(await Task.FromResult(totals));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("latestposts")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BlogPost>>> LatestPosts()
        {
            try
            {
                var posts = _data.BlogPosts.Find(p => p.Published > DateTime.MinValue)
                    .OrderByDescending(p => p.Published);

                return Ok(await Task.FromResult(posts));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
