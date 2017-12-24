using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Search;
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
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();

        private readonly Mock<IPostRepository> _postsRepository = new Mock<IPostRepository>();

        private readonly Mock<IProfileRepository> profileRepository = new Mock<IProfileRepository>();

        private readonly Mock<ISearchService> _searchService = new Mock<ISearchService>();

        private readonly Mock<ICategoryRepository> _categoryRepository = new Mock<ICategoryRepository>();

        [Fact]
        public void GetPosts_Page_SmallerThan_1_Returns_Null()
        {
            // arrange
            SetupDependencies();
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
            SetupDependencies();
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
            SetupDependencies();
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
            SetupDependencies();
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
            SetupDependencies();
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
            SetupDependencies();
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
            SetupDependencies();
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
            SetupDependencies();
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
            SetupDependencies();
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
            SetupDependencies();
            var sut = GetSut();

            // act
            var result = sut.SearchPosts("dotnet", 1, true);

            // assert
            Assert.Equal(result.Posts.Count(), 1);
        }

        [Fact]
        public void GetPostBySlug_NoResult_Returns_Null()
        {
            // arrange
            SetupDependencies();
            _postsRepository.Setup(x => x.SingleIncluded(It.IsAny<Expression<Func<BlogPost, bool>>>()))
                .Returns(Task.FromResult<BlogPost>(null));
            _unitOfWork.Setup(x => x.BlogPosts).Returns(_postsRepository.Object);

            var sut = GetSut();

            // act
            var result = sut.GetPostBySlug("test");

            // assert
            Assert.Null(result);
        }

        //[Fact]
        //public void GetPostBySlug_WithResult_WithoutProfile_Returns_Error()
        //{
        //    // arrange
        //    SetupDependencies();
        //    _postsRepository.Setup(x => x.SingleIncluded(It.IsAny<Expression<Func<BlogPost, bool>>>()))
        //        .Returns(Task.FromResult(new BlogPost
        //        {
        //            Id = 1,
        //            Title = "c#"
        //        }));
        //    _unitOfWork.Setup(x => x.BlogPosts).Returns(_postsRepository.Object);

        //    var sut = GetSut();

        //    // act
        //    var ex = Assert.Throws<NullReferenceException>(() => sut.GetPostBySlug("test"));

        //    // assert
        //    Assert.Equal("Object reference not set to an instance of an object.", ex.Message);
        //}

        [Fact]
        public void GetPostBySlug_WithResult_BlogPost_IsReturned()
        {
            // arrange
            SetupDependencies();
            _postsRepository.Setup(x => x.SingleIncluded(It.IsAny<Expression<Func<BlogPost, bool>>>()))
                .Returns(Task.FromResult(new BlogPost
                {
                    Id = 1,
                    Title = "c#",
                    Profile = new Profile()
                }));
            _unitOfWork.Setup(x => x.BlogPosts).Returns(_postsRepository.Object);

            var sut = GetSut();

            // act
            var result = sut.GetPostBySlug("test");

            // assert
            Assert.Equal(1, result.BlogPost.Id);
            Assert.Equal("c#", result.BlogPost.Title);
        }


        [Fact]
        public void GetPostBySlug_WithResult_WithoutImage_DefaultImage_Is_Assigned()
        {
            // arrange
            SetupDependencies();
            _postsRepository.Setup(x => x.SingleIncluded(It.IsAny<Expression<Func<BlogPost, bool>>>()))
                .Returns(Task.FromResult(new BlogPost
                {
                    Id = 1,
                    Title = "c#",
                    Profile = new Profile()
                }));
            _unitOfWork.Setup(x => x.BlogPosts).Returns(_postsRepository.Object);

            var sut = GetSut();

            // act
            var result = sut.GetPostBySlug("c#");

            // assert
            Assert.Equal(BlogSettings.PostCover, result.BlogPost.Image);
        }

        [Fact]
        public void GetPostBySlug_WithResult_WithImage_ProfileImage_Is_Assigned()
        {
            // arrange
            SetupDependencies();
            _postsRepository.Setup(x => x.SingleIncluded(It.IsAny<Expression<Func<BlogPost, bool>>>()))
                .Returns(Task.FromResult(new BlogPost
                {
                    Id = 1,
                    Title = "c#",
                    Profile = new Profile
                    {
                        Image = "img1"
                    }
                }));
            _unitOfWork.Setup(x => x.BlogPosts).Returns(_postsRepository.Object);

            var sut = GetSut();

            // act
            var result = sut.GetPostBySlug("c#");

            // assert
            Assert.Equal("img1", result.BlogPost.Image);
        }

        [Fact]
        public void GetPostBySlug_WithResult_WithPostCategories_Categories_Are_Added()
        {
            // arrange
            SetupDependencies();
            _postsRepository.Setup(x => x.SingleIncluded(It.IsAny<Expression<Func<BlogPost, bool>>>()))
                .Returns(Task.FromResult(new BlogPost
                {
                    Id = 1,
                    Title = "c#",
                    PostCategories = new List<PostCategory>
                    {
                        new PostCategory
                        {
                            Category = new Category { }
                        }
                    },
                    Profile = new Profile()
                }));
            _unitOfWork.Setup(x => x.BlogPosts).Returns(_postsRepository.Object);

            var sut = GetSut();

            // act
            var result = sut.GetPostBySlug("c#");

            // assert
            Assert.Equal("slug1", result.BlogCategories.First().Value);
            Assert.Equal("123", result.BlogCategories.First().Text);
        }

        [Fact]
        public void GetPostBySlug_With_Pub_true_Profile_Has_EmptyProperties()
        {
            // arrange
            SetupDependencies();
            _postsRepository.Setup(x => x.SingleIncluded(It.IsAny<Expression<Func<BlogPost, bool>>>()))
                .Returns(Task.FromResult(new BlogPost
                {
                    Id = 1,
                    Title = "c#",
                    Profile = new Profile
                    {
                        AuthorEmail = "test@test.com",
                        IdentityName = "smith",
                        IsAdmin = true,
                        BlogPosts = new List<BlogPost>(),
                        Assets = new List<Asset>()
                    }
                }));
            _unitOfWork.Setup(x => x.BlogPosts).Returns(_postsRepository.Object);

            var sut = GetSut();

            // act
            var result = sut.GetPostBySlug("c#", true);

            // assert
            Assert.Equal("", result.Profile.AuthorEmail);
            Assert.Equal("", result.Profile.IdentityName);
            Assert.Equal(false, result.Profile.IsAdmin);
            Assert.Null(result.Profile.BlogPosts);
            Assert.Null(result.Profile.Assets);
        }

        private Core.Services.Data.DataService GetSut()
        {
            return new Core.Services.Data.DataService(
                _unitOfWork.Object,
                _searchService.Object);
        }

        private void SetupDependencies()
        {
            _postsRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<BlogPost, bool>>>(), It.IsAny<Pager>()))
                .Returns(new List<PostListItem>
                {
                    new PostListItem { AuthorName = "Joe", Title = "dotnet core" , AuthorEmail = "test@test.com"}
                });
            _postsRepository.Setup(x => x.SingleIncluded(It.IsAny<Expression<Func<BlogPost, bool>>>()))
                .Returns(Task.FromResult(new BlogPost
                {
                    Id = 1,
                    Profile = new Profile()
                }));

            _unitOfWork.Setup(x => x.BlogPosts).Returns(_postsRepository.Object);

            profileRepository
                .Setup(x => x.Single(It.IsAny<Expression<Func<Profile, bool>>>()))
                .Returns(new Profile
                {
                    Id = 1,
                    AuthorName = "Joe",
                    AuthorEmail = "test@test.com"
                });
            _unitOfWork.Setup(x => x.Profiles).Returns(profileRepository.Object);

            _categoryRepository
                .Setup(x => x.Single(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns(new Category
                {
                    Id = 203,
                    Slug = "slug1",
                    Title = "123"
                });
            _unitOfWork.Setup(x => x.Categories).Returns(_categoryRepository.Object);

            _searchService
                .Setup(x => x.Find(It.IsAny<Pager>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new List<PostListItem>
                {
                   new PostListItem { AuthorName = "Joe", Title = "dotnet core" , AuthorEmail = "test@test.com"}
                }));
        }
    }
}
