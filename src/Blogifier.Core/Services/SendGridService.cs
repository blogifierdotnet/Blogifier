using Blogifier.Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services
{
    public interface IEmailService
    {
        Task<int> SendNewsletters(BlogPost postItem, List<string> emails, string siteUrl);
        Task<bool> SendEmail(string to, string subject, string content);
    }

    public class SendGridService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IStorageService _storage;
        private readonly IDataService _db;

        public SendGridService(IDataService db, IConfiguration config, ILogger<SendGridService> logger, IStorageService storage)
        {
            _db = db;
            _config = config;
            _logger = logger;
            _storage = storage;
        }

        public async Task<int> SendNewsletters(BlogPost post, List<string> emails, string siteUrl)
        {
            int sendCount = 0;
            try
            {
                var blog = await _db.CustomFields.GetBlogSettings();
                var author = _db.Authors.Single(a => a.Id == post.AuthorId);

                foreach (var email in emails)
                {
                    var subject = post.Title;
                    var content = _storage.GetHtmlTemplate("newsletter") ?? "<p>{3}</p>";

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
                        author.DisplayName, // 9
                        siteUrl); // 10

                    if (await SendEmail(email, subject, htmlContent))
                    {
                        sendCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
            return sendCount;
        }

        public async Task<bool> SendEmail(string to, string subject, string content)
        {
            var section = _config.GetSection(Constants.ConfigSectionKey);

            if(section != null)
            {
                var apiKey = section.GetValue<string>("SendGridApiKey");

                if (!string.IsNullOrEmpty(apiKey) && apiKey != "YOUR-SENDGRID-API-KEY")
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
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        return false;
                    }
                }
                else
                {
                    _logger.LogError("Email sevice is not configured");
                    return false;
                }
            }
            await Task.CompletedTask;
            return true;
        }
    }
}
