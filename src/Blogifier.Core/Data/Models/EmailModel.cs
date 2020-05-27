using MailKit.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Models
{
    public class EmailModel
    {
        public List<ProviderItem> Providers { get; set; }
        public EmailProvider SelectedProvider { get; set; }
        public string SendTo { get; set; }
    }

    public class SendGridModel
    {
        public bool Configured { get; set; }
        public string ApiKey { get; set; }
    }

    public class MailKitModel
    {
        public bool Configured { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public string EmailServer { get; set; }
        public int Port { get; set; }
        public SecureSocketOptions Options { get; set; }
    }

    public class ProviderItem
    {
        public string Key { get; set; }
        public string Label { get; set; }
    }

    public enum EmailProvider
    {
        SendGrid, MailKit
    }
}
