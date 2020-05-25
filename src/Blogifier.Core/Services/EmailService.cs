using Blogifier.Core.Data.Models;
using System;
using System.Threading.Tasks;

namespace Blogifier.Core.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string fromName, string fromEmail, string toEmail, string subject, string content);
    }

    public abstract class EmailFactory
    {
        public abstract IEmailService GetEmailService();
    }

    public class EmailService : EmailFactory
    {
        private readonly IDataService _db;

        public EmailService(IDataService db)
        {
            _db = db;
        }

        public override IEmailService GetEmailService()
        {
            var model = _db.CustomFields.GetEmailModel().Result;

            switch (model.SelectedProvider)
            {
                case EmailProvider.MailKit: return new MailKitService(_db);
                case EmailProvider.SendGrid: return new SendGridService(_db);
                default: throw new ArgumentException("Invalid email provider type", nameof(model.SelectedProvider));
            }
        }
    }
}
