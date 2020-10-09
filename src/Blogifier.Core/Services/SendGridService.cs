using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Blogifier.Core.Services
{
    public class SendGridService : IEmailService
    {
        private readonly IDataService _db;

        public SendGridService(IDataService db)
        {
            _db = db;
        }

        public async Task<string> SendEmail(string fromName, string fromEmail, string toEmail, string subject, string content)
        {
            try
            {
                var model = await _db.CustomFields.GetSendGridModel();
                var client = new SendGridClient(model.ApiKey);
                var from = new EmailAddress(fromEmail, fromName);
                var msg = MailHelper.CreateSingleEmail(from, new EmailAddress(toEmail), subject, content.StripHtml(), content);
                var response = await client.SendEmailAsync(msg);

                if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return "SendGrid service returned 'Unauthorized' - please verfiy SendGrid API key in configuration file";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
