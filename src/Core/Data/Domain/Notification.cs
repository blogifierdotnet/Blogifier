using System;

namespace Core.Data
{
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

    public class Newsletter
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }

    public enum AlertType
    {
        System, User, Contact
    }
}