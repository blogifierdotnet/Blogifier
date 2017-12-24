using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Email
{
    public interface IEmailService
    {
        Task<bool> Enabled();
        Task Send(string to, string subject, string message, Profile profile = null);
    }

    public class SendGridService : IEmailService
    {
        private readonly IUnitOfWork _db;
        private readonly ILogger _logger;

        public async Task<bool> Enabled()
        {
            var customField = await _db.CustomFields.GetValue(CustomType.Application, 0, Constants.SendGridApiKey);
            return customField.Length > 0;
        }

        public SendGridService(IUnitOfWork db, ILogger<SendGridService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task Send(string to, string subject, string message, Profile profile = null)
        {
            // if no SendGrid API key set for application, service is not configured
            var apiKey = await _db.CustomFields.GetValue(CustomType.Application, 0, Constants.SendGridApiKey);
            if (string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError(Constants.SendGridNotConfigured);
                return;
            }

            var admin = await _db.Profiles.Where(p => p.IsAdmin).FirstOrDefaultAsync();
            var sentFrom = admin.AuthorEmail;

            // if user has own API key, use it instead of app API key
            if (profile != null)
            {
                var userKey = await _db.CustomFields.GetValue(CustomType.Profile, profile.Id, Constants.SendGridApiKey);
                if (!string.IsNullOrEmpty(userKey))
                    apiKey = userKey;

                sentFrom = profile.AuthorEmail;
            }

            await Execute(to, sentFrom, apiKey, subject, message);
        }

        static async Task Execute(string emailTo, string emailFrom, string apiKey, string subject, string message)
        {
            var from = new EmailAddress(emailFrom);
            var to = new EmailAddress(emailTo);
            var client = new SendGridClient(apiKey);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message.StripHtml(), message);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
