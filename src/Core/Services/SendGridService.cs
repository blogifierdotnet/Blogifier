using Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISendGridService
    {
        Task SendNewsletters(PostItem postItem, List<string> emails);
        Task SendEmail(string to, string subject, string content);
    }

    public class SendGridService : ISendGridService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public SendGridService(IConfiguration config, ILogger<SendGridService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendNewsletters(PostItem postItem, List<string> emails)
        {
            foreach (var email in emails)
            {
                var subject = "Newsletter: " + postItem.Title;
                var htmlContent = postItem.Description;

                await SendEmail(email, subject, htmlContent);
            }
        }

        public async Task SendEmail(string to, string subject, string content)
        {
            var section = _config.GetSection("Blogifier");

            if(section != null)
            {
                var apiKey = section.GetValue<string>("SendGridApiKey");

                if (!string.IsNullOrEmpty(apiKey))
                {
                    try
                    {
                        var client = new SendGridClient(apiKey);

                        var fromEmail = section.GetValue<string>("SendGridEmailFrom") ?? "admin@blog.com";
                        var fromName = section.GetValue<string>("SendGridEmailFromName") ?? "Blog admin";
                        var from = new EmailAddress(fromEmail, fromName);

                        var msg = MailHelper.CreateSingleEmail(from, new EmailAddress(to), subject, content.StripHtml(), content);
                        var response = await client.SendEmailAsync(msg);

                        if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            _logger.LogError("SendGrid service returned 'Unauthorized' - please verfiy SendGrid API key in configuration file");
                        }
                    }
                    catch (System.Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }
                
            }
            await Task.CompletedTask;
        }
    }
}
