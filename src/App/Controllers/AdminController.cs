using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AdminController : Controller
    {
        IDataService _db;
        IImportService _feed;

        public AdminController(IDataService db, IImportService feed)
        {
            _db = db;
            _feed = feed;
        }

        public IActionResult Index()
        {
            return Redirect("~/admin/posts");
        }

        [HttpPost, Route("[controller]/importfeed")]
        public async Task<IEnumerable<ImportMessage>> ImportFeed(IFormFile file)
        {
            var author = _db.Authors.Single(a => a.AppUserName == User.Identity.Name);

            if(!author.IsAdmin)
                Redirect("~/pages/shared/_error/403");

            var webRoot = Url.Content("~/");

            return await _feed.Import(file, User.Identity.Name, webRoot);
        }

        [HttpDelete, Route("[controller]/notifications/remove/{id}")]
        public async Task RemoveNotification(int id)
        {
            var note = _db.Notifications.Single(n => n.Id == id);
            _db.Notifications.Remove(note);
            _db.Complete();
            await Task.CompletedTask;
        }
    }
}