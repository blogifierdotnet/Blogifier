using Core.Data;
using Core.Data.Models;
using Core.Services;
using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services
{
    public class RssImportServiceTests
    {
        private readonly Mock<IRssImportService> _importService = new Mock<IRssImportService>();
        private readonly Mock<IStorageService> _storageService = new Mock<IStorageService>();
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IAuthorRepository> authorRepository = new Mock<IAuthorRepository>();
        private readonly Mock<IPostRepository> postRepository = new Mock<IPostRepository>();
        static string _separator = System.IO.Path.DirectorySeparatorChar.ToString();

        [Fact]
        public void DummyTest()
        {
            /*
            var db = GetMemoryDb("blogifier");
            var x = await db.Users.AllAsync(u => u.Id == "admin");

            SetupDependencies();
            var sut = GetSut();

            var result = sut.ImportFile("/home/ruslan/src/github/Blogifier/src/App/wwwroot/data/_test/be3.xml");
            */
            
            Assert.True(true);
        }

        private Core.Services.RssImportService GetSut()
        {
            return new Core.Services.RssImportService(
                _unitOfWork.Object, _storageService.Object, "admin");
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

            _storageService
                .Setup(x => x.UploadFromWeb(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new AssetItem { Url = "a/b", Path = "a/b" }));
        }

        /*
        private AppDbContext GetMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var context = new AppDbContext(options); 

            context.Users.Add(new AppUser { Id = "admin", UserName = "admin" });
            context.SaveChanges();

            return context;
        }
        */

    }
}