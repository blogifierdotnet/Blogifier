using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newsletter
{
    [Route("blogifier/widgets/[controller]")]
    public class NewsletterController : Controller
    {
        IUnitOfWork _db;
        static readonly string key = "NEWSLETTER";

        public NewsletterController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpPut("subscribe")]
        public async Task Subscribe([FromBody]CustomFieldItem item)
        {
            var emails = Emails();

            if (emails != null)
            {
                if (!emails.Contains(item.CustomValue))
                {
                    emails.Add(item.CustomValue + "|" + System.DateTime.UtcNow);
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
            var field = _db.CustomFields.GetValue(CustomType.Application, 0, key);
            return string.IsNullOrEmpty(field) ? null : field.Split(',').ToList();
        }
    }
}