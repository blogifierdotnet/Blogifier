using Blogifier.Core.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services
{
    public interface INewsletterService
    {
        Task<int> SendNewsletters(BlogPost post, List<string> emails, string siteUrl, string fromName, string fromEmail);
    }

    public class NewsletterService : INewsletterService
    {
        private readonly IDataService _db;
        private readonly IStorageService _storage;
        private readonly ILogger<NewsletterService> _logger;

        public NewsletterService(IDataService db, IStorageService storage, ILogger<NewsletterService> logger)
        {
            _db = db;
            _storage = storage;
            _logger = logger;
        }

        public async Task<int> SendNewsletters(BlogPost post, List<string> emails, string siteUrl, string fromName, string fromEmail)
        {
            int sendCount = 0;
            try
            {
                var blog = await _db.CustomFields.GetBlogSettings();
                var author = _db.Authors.Single(a => a.Id == post.AuthorId);

                EmailFactory factory = new EmailService(_db);
                var emailService = factory.GetEmailService();

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

                    if (await emailService.SendEmail(fromName, fromEmail, email, subject, htmlContent))
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
    }
}
