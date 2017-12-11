using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Newsletter
{
    [Route("blogifier/plugins/[controller]")]
    public class EmailerController : Controller
    {
        IUnitOfWork _db;

        public EmailerController(IUnitOfWork db)
        {
            _db = db;
        }

        [Authorize]
        [MustBeAdmin]
        [HttpGet("settings")]
        public IActionResult Settings(string search = "")
        {
            var profile = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);

            var emails = _db.Subscribers.All()
                .Where(s => s.Active)
                .OrderByDescending(s => s.LastUpdated);

            dynamic settings = new
            {
                Emails = emails,
                Pager = new Pager(1)
            };

            var info = new PackageInfo();

            var model = new AdminSettingsModel {
                Profile = profile,
                Settings = settings,
                PackageItem = info.GetAttributes()
            };

            return View("~/Views/Shared/Components/Emailer/Settings.cshtml", model);
        }

        [Authorize]
        [MustBeAdmin]
        [HttpPut("remove/{id}")]
        public void Remove(string id)
        {
            var existing = _db.Subscribers.Find(s => s.Email == id).FirstOrDefault();

            if (existing != null)
            {
                _db.Subscribers.Remove(existing);
                _db.Complete();
            }
        }
    }
}