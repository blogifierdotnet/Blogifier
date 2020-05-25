using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Blogifier.Core.Services
{
    public class MailKitService : IEmailService
    {
        private readonly IDataService _db;

        public MailKitService(IDataService db)
        {
            _db = db;
        }

        public async Task<bool> SendEmail(string fromName, string fromEmail, string toEmail, string subject, string content)
        {          
            try
            {
                var model = await _db.CustomFields.GetEmailModel();
                var mailKit = model.MailKitModel;

                var message = new MimeMessage();
                var bodyBuilder = new BodyBuilder();

                message.From.Add(new MailboxAddress(fromName, fromEmail));
                message.To.Add(new MailboxAddress(toEmail));

                message.Subject = subject;
                bodyBuilder.HtmlBody = content;
                message.Body = bodyBuilder.ToMessageBody();

                var client = new SmtpClient();

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(mailKit.EmailServer, mailKit.Port, mailKit.Options);
                client.Authenticate(mailKit.EmailName, mailKit.EmailPassword);
                client.Send(message);
                client.Disconnect(true);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
