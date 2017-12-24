using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Settings(int page = 1, string search = "")
        {
            var profile = await _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
            var pager = new Pager(page);

            IEnumerable<Subscriber> emails;

            if (string.IsNullOrEmpty(search))
            {
                emails = _db.Subscribers.Find(s => s.Active, pager);
            }
            else
            {
                emails = _db.Subscribers.Find(s => s.Active && s.Email.Contains(search), pager);
            }
                        
            dynamic settings = new
            {
                Emails = emails,
                Pager = pager
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