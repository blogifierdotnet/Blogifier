using Core.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Data.Models
{
    public class NewsletterModel
    {
        public IEnumerable<Newsletter> Emails { get; set; }
        public Pager Pager { get; set; }
    }

    public class NotificationModel
    {
        public IEnumerable<Notification> Notifications { get; set; }
        public Pager Pager { get; set; }
    }

    public class ContactModel
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Content { get; set; }
    }
}