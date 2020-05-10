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

        private SendGridService GetSut()
        {
            var data = new Mock<IDataService>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=Blog.db").Options;

            var context = new AppDbContext(options);
            var customFieldRepository = new CustomFieldRepository(context);

            IPostRepository posts = new PostRepository(context, customFieldRepository);
            IAuthorRepository authors = new AuthorRepository(context);
            INewsletterRepository letters = new NewsletterRepository(context);
            ICustomFieldRepository custom = new CustomFieldRepository(context);

            IDataService ds = new DataService(context, posts, authors, null, null, custom, letters);

            var logger = new Mock<ILogger<SendGridService>>();
            var storage = new Mock<IStorageService>();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            return new SendGridService(ds, config, logger.Object, storage.Object);
        }
    }
}
