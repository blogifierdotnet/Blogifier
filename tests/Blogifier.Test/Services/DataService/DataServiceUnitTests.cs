using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Custom;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Syndication.Rss;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Blogifier.Test.Services.DataService
{
    public class DataServiceTests
    {
        [Fact]
        public void GetPosts_Page_SmallerThan_1_Returns_Null()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = sut.GetPosts(0);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void GetPosts_WithPager_Returns_AllPosts()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = sut.GetPosts(1);

            // assert
            Assert.Equal(result.Posts.Count(), 1);
        }

        [Fact]
        public void GetPosts_With_PubEquals_true_AuthorEmail_Is_EmptyString()
        {
            // arrange
            var sut = GetSut();
            // act
            var result = sut.GetPosts(1, true);

            // assert
            Assert.Empty(result.Posts.First().AuthorEmail);
        }

        [Fact]
        public void GetPostsByAuthor_Page_SmallerThan_1_Returns_Null()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = sut.GetPostsByAuthor("joe", -1);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void GetPostsByAuthor_WithPager_Returns_AllPosts()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = sut.GetPostsByAuthor("joe", 1);

            // assert
            Assert.Equal(result.Posts.Count(), 1);
        }

        [Fact]
        public void GetPostsByAuthor_With_PubEquals_false_AuthorEmail_Is_Not_Empty()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = sut.GetPostsByAuthor("joe", 1);

            // assert
            Assert.NotEmpty(result.Posts.First().AuthorEmail);
        }

        [Fact]
        public void GetPostsByAuthor_BlogCategoryModel_IsCreated_WithProvided_Fields()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = sut.GetPostsByAuthor("joe", 1);

            // assert
            Assert.Equal(result.Profile.Id, 1);
            Assert.Equal(result.Profile.AuthorName, "Joe");
            Assert.Equal(result.Profile.AuthorEmail, "test@test.com");
            Assert.Equal(result.Posts.First().AuthorName, "Joe");
            Assert.Equal(result.Posts.First().Title, "dotnet core");
            Assert.Equal(result.Posts.First().AuthorEmail, "test@test.com");
        }

        [Fact]
        public void SearchPosts_With_Page_GreaterThan_0_Result_IsNotNull()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = sut.SearchPosts("dotnet", 1);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void SearchPosts_With_PubEquals_true_AuthorEmail_Is_Empty()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = sut.SearchPosts("dotnet", 1, true);

            // assert
            Assert.Empty(result.Posts.First().AuthorEmail);
        }

        [Fact]
        public void SearchPosts_WithOneResult_Returns_OnePost()
        {
            // arrange
            var sut = GetSut();

            // act
            var result = sut.SearchPosts("dotnet", 1, true);

            // assert
            Assert.Equal(result.Posts.Count(), 1);
        }

        private static Core.Services.Data.DataService GetSut()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var postsRepository = new Mock<IPostRepository>();
            postsRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<BlogPost, bool>>>(), It.IsAny<Pager>()))
                .Returns(new List<PostListItem>
                {
                    new PostListItem { AuthorName = "Joe", Title = "dotnet core" , AuthorEmail = "test@test.com"}
                });
            unitOfWork.Setup(x => x.BlogPosts).Returns(postsRepository.Object);

            var profileRepository = new Mock<IProfileRepository>();
            profileRepository
                .Setup(x => x.Single(It.IsAny<Expression<Func<Profile, bool>>>()))
                .Returns(new Profile
                {
                    Id = 1, AuthorName = "Joe", AuthorEmail = "test@test.com"
                });
            unitOfWork.Setup(x => x.Profiles).Returns(profileRepository.Object);

            var custom = new Mock<ICustomService>();
            var rss = new Mock<IRssService>();
            var search = new Mock<ISearchService>();
            search
                .Setup(x => x.Find(It.IsAny<Pager>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new List<PostListItem>
                {
                   new PostListItem { AuthorName = "Joe", Title = "dotnet core" , AuthorEmail = "test@test.com"}
                }));

            return new Core.Services.Data.DataService(
                unitOfWork.Object, 
                custom.Object, 
                rss.Object, 
                search.Object);
        }
    }
}
