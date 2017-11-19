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

        public NewsletterController(IUnitOfWork db)
        {
            _db = db;
        }

        [VerifyProfile]
        [HttpGet("admin/packages/widgets/Newsletter")]
        public IActionResult Settings()
        {
            //return View(
            //    "~/Views/Shared/Components/Newsletter/Settings.cshtml",
            //    new NewsletterSettingsModel
            //    {
            //        Profile = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name),
            //        Emails = Emails()
            //    }
            //);
            return View(
                "~/Views/Shared/Components/Newsletter/Settings.cshtml",
                new AdminBaseModel
                {
                    Profile = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name)
                }
            );
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

        List<string> Emails()
        {
            var field = _db.CustomFields.Single(f => f.CustomType == CustomType.Application && f.CustomKey == "NEWSLETTER");
            return field == null || string.IsNullOrEmpty(field.CustomValue) ? null : field.CustomValue.Split(',').ToList();
        }
    }
}

namespace Blogifier.Core.Data.Models
{
    public class NewsletterSettingsModel : AdminBaseModel
    {
        public IEnumerable<string> Emails { get; set; }
        public Pager Pager { get; set; }
    }
}