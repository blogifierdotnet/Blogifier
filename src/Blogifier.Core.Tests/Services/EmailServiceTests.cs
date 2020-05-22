using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class EmailServiceTests
    {
        private readonly string email = "test@test.com";

        [Fact]
        public async Task CanSendEmail()
        {
            var sut = GetSut();

            bool expected = await sut.SendEmail(email, "test", "testing blogifier");

            Assert.False(expected);
        }

        [Fact]
        public async Task CanSendNewsletters()
        {
            var sut = GetSut();
            var emails = new List<string> { email };

            BlogPost post = new BlogPost
            {
                Title = "test",
                Description = "test",
                Content = "test",
                Slug = "test",
                Published = DateTime.Now.AddDays(-2),
                Cover = "",
                AuthorId = 1
            };

            int expected = await sut.SendNewsletters(post, emails, "http://blogifier.net");

            Assert.Equal(0, expected);
        }

        #region MailKit

        [Fact]
        public async Task CanSendMailKitEmail()
        {
            var sut = GetMailKitService();

            bool expected = await sut.SendEmail(email, "test", "testing blogifier");

            Assert.False(expected);
        }

        #endregion

        private SendGridService GetSut()
        {
            var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlite("DataSource=Blog.db").Options);
            var customFieldRepository = new CustomFieldRepository(context);

            IPostRepository posts = new PostRepository(context, customFieldRepository);
            IAuthorRepository authors = new AuthorRepository(context);
            INewsletterRepository letters = new NewsletterRepository(context);
            ICustomFieldRepository custom = new CustomFieldRepository(context);
            IDataService ds = new DataService(context, posts, authors, null, null, custom, letters, null);

            var logger = new Mock<ILogger<SendGridService>>();
            var storage = new Mock<IStorageService>();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            return new SendGridService(ds, config, logger.Object, storage.Object);
        }

        private MailKitService GetMailKitService()
        {
            var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlite("DataSource=Blog.db").Options);
            var customFieldRepository = new CustomFieldRepository(context);

            IPostRepository posts = new PostRepository(context, customFieldRepository);
            IAuthorRepository authors = new AuthorRepository(context);
            INewsletterRepository letters = new NewsletterRepository(context);
            ICustomFieldRepository custom = new CustomFieldRepository(context);
            IDataService ds = new DataService(context, posts, authors, null, null, custom, letters, null);

            var logger = new Mock<ILogger<SendGridService>>();
            var storage = new Mock<IStorageService>();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            return new MailKitService(ds, config, logger.Object, storage.Object);
        }
    }
}
