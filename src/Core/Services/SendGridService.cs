using Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IEmailService
    {
        Task SendNewsletters(PostItem postItem, List<string> emails, string siteUrl);
        Task SendEmail(string to, string subject, string content);
    }

    public class SendGridService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        IStorageService _storage;
        IDataService _db;

        public SendGridService(IDataService db, IConfiguration config, ILogger<SendGridService> logger, IStorageService storage)
        {
            _db = db;
            _config = config;
            _logger = logger;
            _storage = storage;
        }

        public async Task SendNewsletters(PostItem post, List<string> emails, string siteUrl)
        {
            var blog = await _db.CustomFields.GetBlogSettings();
            foreach (var email in emails)
            {
                var subject = post.Title;
                var content = _storage.GetHtmlTemplate("newsletter");

                var htmlContent = string.Format(content,
                    blog.Title, // 0
                    blog.Logo,  // 1
                    blog.Cover, // 2
                    post.Title, // 3
                    post.Description, // 4 
                    post.Content, // 5
                    post.Slug, // 6
                    post.Published, // 7 
                    post.Cover, // 8
                    post.Author, // 9
                    siteUrl); // 10

                await SendEmail(email, subject, htmlContent);
            }
        }

        public async Task SendEmail(string to, string subject, string content)
        {
            var section = _config.GetSection(Constants.ConfigSectionKey);

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
