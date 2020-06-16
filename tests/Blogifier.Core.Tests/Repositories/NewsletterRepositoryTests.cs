using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Repositories
{
    public class NewsletterRepositoryTests
    {
        [Fact]
        public async Task CanGetAndRemoveNewsletterFromDb()
        {
            var email = "test@test.com";
            var db = GetSut();
            var sut = new NewsletterRepository(db);

            sut.Add(new Newsletter { Email = email, Ip = "1.2.3", Created = SystemClock.Now() });
            db.SaveChanges();

            var result = await sut.GetList(x => x.Id > 0, new Pager(1));
            Assert.NotNull(result);
            int count = result.Count();

            var existing = sut.Single(x => x.Email == email);
            db.Newsletters.Remove(existing);
            db.SaveChanges();

            result = await sut.GetList(x => x.Id > 0, new Pager(1));
            Assert.True(result.Count() == count - 1);
        }

        private AppDbContext GetSut()
        {
            var helper = new DbHelper();
            return helper.GetDbContext();
        }
    }
}