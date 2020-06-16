using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class DataServiceTests
    {
        [Fact]
        public async Task GetList_All_Published_Returns_Result()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = await sut.BlogPosts.GetList(x => x.Published > DateTime.MinValue, new Pager(1));

            // assert
            Assert.True(result.ToList().Count > 0);
        }

        private IDataService GetSut()
        {
            var helper = new DbHelper();
            var context = helper.GetDbContext();

            var customFieldRepository = new CustomFieldRepository(context);

            IPostRepository posts = new PostRepository(context, customFieldRepository);
            IAuthorRepository authors = new AuthorRepository(context);
            INewsletterRepository letters = new NewsletterRepository(context);
            ICustomFieldRepository custom = new CustomFieldRepository(context);

            return new DataService(context, posts, authors, null, null, custom, letters, null);
        }
    }
}