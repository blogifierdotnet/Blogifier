using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Newsletter
{
    [Route("blogifier/widgets/[controller]")]
    public class NewsletterController : Controller
    {
        IUnitOfWork _db;

        public NewsletterController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpPut("subscribe")]
        public async Task<int> Subscribe([FromBody]Subscriber item)
        {
            item.Created = SystemClock.Now();
            item.LastUpdated = SystemClock.Now();
            item.Active = true;

            var existing = _db.Subscribers.Find(s => s.Email == item.Email).FirstOrDefault();

            if(existing == null)
            {
                _db.Subscribers.Add(item);
            }
            else
            {
                existing.Created = SystemClock.Now();
                existing.LastUpdated = SystemClock.Now();
                item.Active = true;
            }

            return await _db.Complete();
        }
    }
}