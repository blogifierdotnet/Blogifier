using Core.Data;
using Core.Data.Models;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        IDataService _data;

        public NotificationsController(IDataService data)
        {
            _data = data;
        }

        /// <summary>
        /// List of newsletter subscriptions (admins only)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <returns>List of subscribers</returns>
        [HttpGet("subscriptions")]
        [Administrator]
        public async Task<NewsletterModel> GetSubscriptions(int page = 1)
        {
            var pager = new Pager(page, 20);
            IEnumerable<Newsletter> items;

            items = await _data.Newsletters.GetList(e => e.Id > 0, pager);

            if (page < 1 || page > pager.LastPage)
                return null;

            return new NewsletterModel
            {
                Emails = items,
                Pager = pager
            };
        }

        /// <summary>
        /// Subscribe to newsletter (CORS enabled)
        /// </summary>
        /// <param name="email">Subscriber email</param>
        /// <returns>Error message or empty string if successful</returns>
        [HttpPut("subscribe")]
        [EnableCors("AllowOrigin")]
        public IActionResult Subscribe(string email)
        {
            if (!string.IsNullOrEmpty(email) && email.IsEmail())
            {
                try
                {
                    var existing = _data.Newsletters.Single(n => n.Email == email);
                    if (existing == null)
                    {
                        _data.Newsletters.Add(new Newsletter { Email = email });
                        _data.Complete();
                    }
                }
                catch (System.Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            return Ok();
        }

        /// <summary>
        /// Unsubscribe from newsletter (admins only)
        /// </summary>
        /// <param name="email">Subscriber email</param>
        /// <returns>Ok on success</returns>
        [HttpPut("unsubscribe")]
        [Administrator]
        public IActionResult Unsubscribe(string email)
        {
            var existing = _data.Newsletters.Single(n => n.Email == email);
            if (existing != null)
            {
                _data.Newsletters.Remove(existing);
                _data.Complete();
            }
            return Ok();
        }
    }
}
