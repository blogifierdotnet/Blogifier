using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Newsletter
{
    [Route("blogifier/api/[controller]")]
    public class NewsletterController : Controller
    {
        IUnitOfWork _db;

        public NewsletterController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpPut]
        [Route("subscribe")]
        public async Task Subscribe([FromBody]CustomFieldItem item)
        {
            var field = _db.CustomFields.Single(f => f.CustomType == CustomType.Application && f.CustomKey == "NEWSLETTER");

            if (field != null)
            {
                field.CustomValue = field.CustomValue + "," + item.CustomValue;
                await _db.CustomFields.SetCustomField(CustomType.Application, 0, item.CustomKey, item.CustomValue);
            }
            else
            {
                var newField = new CustomField
                {
                    CustomKey = "NEWSLETTER",
                    Title = "NEWSLETTER",
                    CustomValue = item.CustomValue,
                    CustomType = CustomType.Application
                };
                _db.CustomFields.Add(newField);
                _db.Complete();
            }
        }
    }
}