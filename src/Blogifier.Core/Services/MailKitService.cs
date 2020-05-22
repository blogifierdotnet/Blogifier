using Blogifier.Core.Data;
using MailKit.Net.Smtp;
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
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Tester", "tester@test.com"));
            mailMessage.To.Add(new MailboxAddress("test", "test@gmail.com"));
            mailMessage.Subject = "subject";
            mailMessage.Body = new TextPart("plain")
            {
                Text = "Hello, this is a test."
            };

            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect("smtp.gmail.com", 25, false);
                    smtpClient.Authenticate("test@gmail.com", "test");
                    smtpClient.Send(mailMessage);
                    smtpClient.Disconnect(true);
                }
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
