using Core.Data;
using Core.Services;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Tests.Services
{
    public class SyndicationServiceTests
    {
        private readonly Mock<IFeedService> _syndicationService = new Mock<IFeedService>();
        private readonly Mock<IDataService> _unitOfWork = new Mock<IDataService>();
        private readonly Mock<IAuthorRepository> authorRepository = new Mock<IAuthorRepository>();
        private readonly Mock<IPostRepository> postRepository = new Mock<IPostRepository>();
        private readonly IStorageService _storage;

        static string _separator = System.IO.Path.DirectorySeparatorChar.ToString();
        
        public SyndicationServiceTests()
        {
            _storage = new StorageService(null);
        }

        private FeedService GetSut()
        {
            return new FeedService(_unitOfWork.Object, _storage); // _storageService.Object);
        }

        private void SetupDependencies()
        {
            authorRepository
                .Setup(x => x.GetItem(It.IsAny<Expression<Func<Author, bool>>>()))
                .Returns(Task.FromResult(new Author
                {
                    Id = 1,
                    AppUserName = "admin"
                    //Email = "admin@us.com"
                }));
            _unitOfWork.Setup(x => x.Authors).Returns(authorRepository.Object);

            postRepository
                .Setup(x => x.Single(It.IsAny<Expression<Func<BlogPost, bool>>>()))
                .Returns(new BlogPost
                {
                    Id = 1,
                    Title = "Post one",
                    Slug = "post-one"
                });
            _unitOfWork.Setup(x => x.BlogPosts).Returns(postRepository.Object);
        }
    }
}
