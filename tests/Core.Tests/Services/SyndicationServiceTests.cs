using Core.Data;
using Core.Data.Models;
using Core.Services;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class SyndicationServiceTests
    {
        private readonly Mock<ISyndicationService> _syndicationService = new Mock<ISyndicationService>();
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IAuthorRepository> authorRepository = new Mock<IAuthorRepository>();
        private readonly Mock<IPostRepository> postRepository = new Mock<IPostRepository>();
        private readonly IStorageService _storage;

        static string _separator = System.IO.Path.DirectorySeparatorChar.ToString();
        
        public SyndicationServiceTests()
        {
            _storage = new StorageService(null);
        }

        private SyndicationService GetSut()
        {
            return new SyndicationService(_unitOfWork.Object, _storage); // _storageService.Object);
        }

        private void SetupDependencies()
        {
            authorRepository
                .Setup(x => x.GetItem(It.IsAny<Expression<Func<AppUser, bool>>>()))
                .Returns(Task.FromResult(new AuthorItem
                {
                    Id = "admin",
                    UserName = "admin",
                    Email = "admin@us.com"
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

        //private AppDbContext GetMemoryDb(string dbName)
        //{
        //    var options = new DbContextOptionsBuilder<AppDbContext>()
        //        .UseInMemoryDatabase(dbName).Options;

        //    var context = new AppDbContext(options); 

        //    context.Users.Add(new AppUser { Id = "admin", UserName = "admin" });
        //    context.SaveChanges();

        //    return context;
        //}
    }
}
