using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers.Api
{
    [Authorize]
    [Route("blogifier/api/[controller]")]
    public class ToolsController : Controller
    {
        IUnitOfWork _db;
        IRssService _rss;

        public ToolsController(IUnitOfWork db, IRssService rss)
        {
            _db = db;
            _rss = rss;
        }

        // PUT: api/tools/rssimport
        [HttpPut]
        [Route("rssimport")]
        public async Task<HttpResponseMessage> RssImport([FromBody]RssImportModel rss)
        {
            return await _rss.Import(rss, Url.Content("~/"));
        }
    }
}