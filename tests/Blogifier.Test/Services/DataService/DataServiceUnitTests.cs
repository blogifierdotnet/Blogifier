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
using Xunit;

namespace Blogifier.Test.Services.DataService
{
    public class DataServiceTests
    {
        [Fact]
        public void GetPosts_Page_SmallerThan_1_Returns_Null()
        {
            // arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var postsRepository = new Mock<IPostRepository>();
            postsRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<BlogPost, bool>>>(), It.IsAny<Pager>()))
                .Returns(new List<PostListItem> { new PostListItem { AuthorName = "Bob", Title = "dotnet core" } });
            unitOfWork.Setup(x => x.BlogPosts).Returns(postsRepository.Object);

            var custom = new Mock<ICustomService>();
            var rss = new Mock<IRssService>();
            var search = new Mock<ISearchService>();

            var sut = new Core.Services.Data.DataService(unitOfWork.Object, custom.Object, rss.Object, search.Object);

            // act
            var result = sut.GetPosts(0);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void GetPosts_WithPager_Returns_AllPosts()
        {
            // arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var postsRepository = new Mock<IPostRepository>();
            postsRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<BlogPost, bool>>>(), It.IsAny<Pager>()))
                .Returns(new List<PostListItem>
                {
                    new PostListItem { AuthorName = "Bob", Title = "dotnet core" }
                });
            unitOfWork.Setup(x => x.BlogPosts).Returns(postsRepository.Object);

            var custom = new Mock<ICustomService>();
            var rss = new Mock<IRssService>();
            var search = new Mock<ISearchService>();

            var sut = new Core.Services.Data.DataService(unitOfWork.Object, custom.Object, rss.Object, search.Object);

            // act
            var result = sut.GetPosts(1);

            // assert
            Assert.Equal(result.Posts.Count(), 1);
        }

        [Fact]
        public void GetPosts_With_PubEquals_true_AuthorEmail_Is_EmptyString()
        {
            // arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var postsRepository = new Mock<IPostRepository>();
            postsRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<BlogPost, bool>>>(), It.IsAny<Pager>()))
                .Returns(new List<PostListItem>
                {
                    new PostListItem { AuthorName = "Bob", Title = "dotnet core" , AuthorEmail = "test@test.com" }
                });
            unitOfWork.Setup(x => x.BlogPosts).Returns(postsRepository.Object);

            var custom = new Mock<ICustomService>();
            var rss = new Mock<IRssService>();
            var search = new Mock<ISearchService>();

            var sut = new Core.Services.Data.DataService(unitOfWork.Object, custom.Object, rss.Object, search.Object);

            // act
            var result = sut.GetPosts(1, true);

            // assert
            Assert.Equal(result.Posts.First().AuthorEmail, string.Empty);
        }
    }
}
