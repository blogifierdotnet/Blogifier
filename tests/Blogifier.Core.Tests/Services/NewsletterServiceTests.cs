using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class NewsletterServiceTests
    {
        [Fact]
        public async Task CanSendNewsletters()
        {
            var sut = GetSut();
            var emails = new List<string> { "test@test.com" };

            BlogPost post = new BlogPost
            {
                Title = "test title",
                Description = "test description",
                Content = "test content",
                Slug = "test-slug",
                Published = DateTime.Now.AddDays(-2),
                Cover = "",
                AuthorId = 1
            };

            int expected = await sut.SendNewsletters(post, emails, "http://blogifier.net");

            Assert.Equal(0, expected);
        }

        private INewsletterService GetSut()
        {
            var helper = new DbHelper();
            var context = helper.GetDbContext();

            var custom = new CustomFieldRepository(context);
            var ds = new DataService(context, null, null, null, null, custom, null, null);
            var logger = new Mock<ILogger<NewsletterService>>();
            var storage = new Mock<IStorageService>();

            return new NewsletterService(ds, storage.Object, logger.Object);
        }
    }
}
