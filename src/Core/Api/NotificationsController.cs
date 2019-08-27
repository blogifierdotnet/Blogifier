using Core.Data;
using Core.Data.Models;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        IDataService _data;
        INotificationService _notes;

        public NotificationsController(IDataService data, INotificationService notes)
        {
            _data = data;
            _notes = notes;
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

        /// <summary>
        /// "Contact us" form notifications (CORS enabled)
        /// </summary>
        /// <param name="model">Contact model</param>
        /// <returns>Ok</returns>
        [HttpPost("contact")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Contact([FromBody]ContactModel model)
        {
            if (ModelState.IsValid)
            {
                await _notes.AddNotification(
                    AlertType.Contact,
                    0,
                    $"{model.Name}|{model.Email}",
                    model.Content.StripHtml()
                );
            }
            return Ok();
        }

        /// <summary>
        /// Get notifications by type (admins only)
        /// </summary>
        /// <param name="type">Notification type, like contact, newsletter etc</param>
        /// <param name="page">Page number</param>
        /// <returns>List of notifications</returns>
        [HttpGet("{type}")]
        [Administrator]
        public async Task<NotificationModel> GetNotifications(string type, int page = 1)
        {
            var pager = new Pager(page);
            IEnumerable<Notification> items;
            AlertType noteType = AlertType.System;

            if (type.ToUpper() == "CONTACT")
            {
                noteType = AlertType.Contact;
                items = await _data.Notifications.GetList(n => n.AlertType == noteType, pager);
            }
            else
            {
                await _notes.PullSystemNotifications();
                items = await _data.Notifications.GetList(n => n.AlertType == noteType && n.Active == true, pager);
                foreach (var item in items)
                {
                    item.Content = item.Content.MdToHtml();
                }
            }

            if (page < 1 || page > pager.LastPage)
                return null;

            return new NotificationModel
            {
                Notifications = items,
                Pager = pager
            };
        }

        /// <summary>
        /// Remove notification (admins only)
        /// </summary>
        /// <param name="id">Notification ID</param>
        /// <returns>Ok on success</returns>
        [Administrator]
        [HttpDelete("remove/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var notification = _data.Notifications.Single(n => n.Id == id);
                if (notification != null)
                {
                    if(notification.AlertType == AlertType.System)
                    {
                        // system notifications pulled from external sources
                        // if just remove it, it will be pulled again
                        // so just mark it as inactive instead
                        notification.Active = false;
                    }
                    else
                    {
                        _data.Notifications.Remove(notification);
                    }
                    _data.Complete();
                }
                return Ok(Resources.Removed);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
