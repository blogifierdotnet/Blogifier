using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newsletter
{
    public class NewsletterController : Controller
    {
        IUnitOfWork _db;
        static readonly string key = "NEWSLETTER";

        public NewsletterController(IUnitOfWork db)
        {
            _db = db;
        }

        [VerifyProfile]
        [HttpGet("admin/packages/widgets/Newsletter")]
        public IActionResult Settings()
        {
            var profile = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);

            dynamic settings = new
            {
                Emails = Emails(),
                Pager = new Pager(1)
            };

            var model = new AdminSettingsModel { Profile = profile, Settings = settings };

            return View("~/Views/Shared/Components/Newsletter/Settings.cshtml", model);
        }

        [HttpPut("blogifier/api/newsletter/subscribe")]
        public async Task Subscribe([FromBody]CustomFieldItem item)
        {
            var emails = Emails();

            if (emails != null)
            {
                if (!emails.Contains(item.CustomValue))
                {
                    emails.Add(item.CustomValue);
                    await _db.CustomFields.SetCustomField(CustomType.Application, 0, item.CustomKey, string.Join(",", emails));
                }               
            }
            else
            {
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, item.CustomKey, item.CustomValue);
            }
        }

        [HttpPut("blogifier/api/newsletter/remove/{id}")]
        public async Task Remove(string id)
        {
            var emails = Emails();
            if (emails != null && emails.Contains(id))
            {
                emails.Remove(id);
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, key, string.Join(",", emails));
            }
        }

        List<string> Emails()
        {
            var field = _db.CustomFields.Single(f => f.CustomType == CustomType.Application && f.CustomKey == key);
            return field == null || string.IsNullOrEmpty(field.CustomValue) ? null : field.CustomValue.Split(',').ToList();
        }
    }
}