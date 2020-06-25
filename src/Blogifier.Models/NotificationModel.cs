using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Models
{
    public class NotificationModel
    {
        public IEnumerable<Notification> Notifications { get; set; }
        public Pager Pager { get; set; }
    }

    public class Notification
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public AlertType AlertType { get; set; }
        public string Content { get; set; }
        public DateTime DateNotified { get; set; }
        public string Notifier { get; set; }
        public bool Active { get; set; }
    }

    public class ContactModel
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Content { get; set; }
    }

    public enum AlertType
    {
        System, User, Contact
    }
}