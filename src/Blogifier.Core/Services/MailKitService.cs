using Blogifier.Core.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services
{
    public class MailKitService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IStorageService _storage;
        private readonly IDataService _db;

        public MailKitService(IDataService db, IConfiguration config, ILogger<SendGridService> logger, IStorageService storage)
        {
            _db = db;
            _config = config;
            _logger = logger;
            _storage = storage;
        }

        public Task<bool> SendEmail(string to, string subject, string content)
        {
            var section = _config.GetSection(Constants.ConfigSectionKey);

            if (section != null)
            {
                var apiKey = section.GetValue<string>("MailKitApiKey");
            }
            
            try
            {
                var message = new MimeMessage();
                var bodyBuilder = new BodyBuilder();

                // from
                message.From.Add(new MailboxAddress("test", "from_email@example.com"));
                // to
                message.To.Add(new MailboxAddress("test", "test@gmail.com"));

                message.Subject = "subject";
                bodyBuilder.HtmlBody = "html body";
                message.Body = bodyBuilder.ToMessageBody();

                var client = new SmtpClient();

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("test@gmail.com", "password");
                client.Send(message);
                client.Disconnect(true);


                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Task.FromResult(false);
            }
        }

        public Task<int> SendNewsletters(BlogPost postItem, List<string> emails, string siteUrl)
        {
            throw new NotImplementedException();
        }
    }
}
