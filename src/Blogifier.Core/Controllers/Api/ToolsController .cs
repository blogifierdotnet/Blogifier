using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

        // PUT: api/tools/disqus
        [HttpPut]
        [Route("disqus")]
        public HttpResponseMessage Disqus([FromBody]CustomField disqus)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            if (disqus.Id > 0)
            {
                var existing = _db.CustomFields.Single(f => f.Id == disqus.Id);
                existing.CustomValue = disqus.CustomValue;
            }
            else
            {
                _db.CustomFields.Add(disqus);
            }
            _db.Complete();

            response.ReasonPhrase = Constants.ItemSaved;
            return response;
        }
    }
}