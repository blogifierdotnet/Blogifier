using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class EmailServiceTests
    {
        [Fact]
        public async Task CanSendEmail()
        {
            IEmailService sut = GetSut();

            bool expected = await sut.SendEmail("blog admin", "admin@blog.com", "test@test.com", "test", "testing");

            Assert.False(expected);
        }

        private IEmailService GetSut()
        {
            var helper = new DbHelper();
            var context = helper.GetDbContext();

            var custom = new CustomFieldRepository(context);
            var authors = new AuthorRepository(context);
            var ds = new DataService(context, null, authors, null, null, custom, null, null);

            EmailFactory factory = new EmailService(ds);
            return factory.GetEmailService();
        }
    }
}
