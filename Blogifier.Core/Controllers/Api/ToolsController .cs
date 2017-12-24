using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.FileSystem;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            var assets = _db.Assets.Find(a => a.ProfileId == id);
            _db.Assets.RemoveRange(assets);
            _db.Complete();
            _logger.LogInformation("Assets deleted");

            var categories = _db.Categories.Find(c => c.ProfileId == id);
            _db.Categories.RemoveRange(categories);
            _db.Complete();
            _logger.LogInformation("Categories deleted");

            var posts = _db.BlogPosts.Find(p => p.ProfileId == id);
            _db.BlogPosts.RemoveRange(posts);
            _db.Complete();
            _logger.LogInformation("Posts deleted");

            var fields = _db.CustomFields.Find(f => f.CustomType == CustomType.Profile && f.ParentId == id);
            _db.CustomFields.RemoveRange(fields);
            _db.Complete();
            _logger.LogInformation("Custom fields deleted");

            var profileToDelete = await _db.Profiles.Single(b => b.Id == id);

            var storage = new BlogStorage(profileToDelete.Slug);
            storage.DeleteFolder("");
            _logger.LogInformation("Storage deleted");

            _db.Profiles.Remove(profileToDelete);
            _db.Complete();
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