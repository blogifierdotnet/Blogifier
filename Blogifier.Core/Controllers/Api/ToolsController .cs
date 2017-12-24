using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.FileSystem;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public ToolsController(IUnitOfWork db, IRssService rss, ILogger<AdminController> logger)
        {
            _db = db;
            _rss = rss;
            _logger = logger;
        }

        // PUT: api/tools/rssimport
        [HttpPut]
        [Route("rssimport")]
        public async Task<HttpResponseMessage> RssImport([FromBody]RssImportModel rss)
        {
            var profile = await GetProfile();
            rss.ProfileId = profile.Id;
            rss.Root = Url.Content("~/");
            
            return await _rss.Import(rss);
        }

        [HttpDelete("{id}")]
        [Route("deleteblog/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await GetProfile();

            if (!profile.IsAdmin || profile.Id == id)
                return NotFound();

            _logger.LogInformation(string.Format("Delete blog {0} by {1}", id, profile.AuthorName));

            var assets = await _db.Assets.Where(a => a.ProfileId == id).ToListAsync();
            _db.Assets.RemoveRange(assets);
            await _db.Complete();
            _logger.LogInformation("Assets deleted");

            var categories = await _db.Categories.Where(c => c.ProfileId == id).ToListAsync();
            _db.Categories.RemoveRange(categories);
            await _db.Complete();
            _logger.LogInformation("Categories deleted");

            var posts = await _db.BlogPosts.Where(p => p.ProfileId == id).ToListAsync();
            _db.BlogPosts.RemoveRange(posts);
            await _db.Complete();
            _logger.LogInformation("Posts deleted");

            var fields = await _db.CustomFields.Where(f => f.CustomType == CustomType.Profile && f.ParentId == id).ToListAsync();
            _db.CustomFields.RemoveRange(fields);
            await _db.Complete();
            _logger.LogInformation("Custom fields deleted");

            var profileToDelete = await _db.Profiles.Single(b => b.Id == id);

            var storage = new BlogStorage(profileToDelete.Slug);
            storage.DeleteFolder("");
            _logger.LogInformation("Storage deleted");

            _db.Profiles.Remove(profileToDelete);
            await _db.Complete();
            _logger.LogInformation("Profile deleted");

            return new NoContentResult();
        }

        async Task<Profile> GetProfile()
        {
            try
            {
                return await _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
            }
            catch
            {
                RedirectToAction("Login", "Account");
            }
            return null;
        }
    }
}