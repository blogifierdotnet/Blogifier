using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.FileSystem;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace Blogifier.Core.Controllers.Api
{
    [Authorize]
    [Route("blogifier/api/[controller]")]
    public class ToolsController : Controller
    {
        private readonly string _theme;
        private readonly ILogger _logger;
        IUnitOfWork _db;
        IRssService _rss;

        public ToolsController(IUnitOfWork db, IRssService rss, ILogger<AdminController> logger)
        {
            _db = db;
            _rss = rss;
            _logger = logger;
            _theme = "~/Views/Blogifier/Admin/" + ApplicationSettings.AdminTheme + "/Tools.cshtml";
        }

        // PUT: api/tools/rssimport
        [HttpPut]
        [Route("rssimport")]
        public async Task<HttpResponseMessage> RssImport([FromBody]RssImportModel rss)
        {
            return await _rss.Import(rss, Url.Content("~/"));
        }

        Profile GetProfile()
        {
            try
            {
                return _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
            }
            catch
            {
                RedirectToAction("Login", "Account");
            }
            return null;
        }
    }
}
