using App;
using Core.Data;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class ImportServiceTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly Mock<IDataService> _unitOfWork = new Mock<IDataService>();
        private readonly Mock<IAuthorRepository> _authorRepository = new Mock<IAuthorRepository>();
        private readonly Mock<IPostRepository> _postsRepository = new Mock<IPostRepository>();
        private readonly Mock<IStorageService> _storageService = new Mock<IStorageService>();

        static string _separator = Path.DirectorySeparatorChar.ToString();

        public ImportServiceTests(WebApplicationFactory<Startup> factory)
        {
            
        }


        [Fact]
        public async Task CanImportFromRssFeed()
        {
            SetupDependencies();

            var sut = GetSut();

            var fileName = $"{GetAppRoot()}{_separator}_init{_separator}_test{_separator}be3.xml";
            var result = await sut.Import(fileName, "admin");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        private ImportService GetSut()
        {
            return new ImportService(_unitOfWork.Object, _storageService.Object);
        }

        private void SetupDependencies()
        {
            var author = new Author { Id = 1, AppUserName = "admin" };
            var postItem = new PostItem { Author = author, Title = "dotnet core", Description = "test@test.com" };
            var items = new List<PostItem>();
            items.Add(postItem);

            _postsRepository
                .Setup(x => x.GetList(It.IsAny<Expression<Func<BlogPost, bool>>>(), It.IsAny<Pager>()))
                .Returns(Task.FromResult(items.AsEnumerable()));
            _unitOfWork.Setup(x => x.BlogPosts).Returns(_postsRepository.Object);

            _authorRepository
                .Setup(x => x.GetItem(It.IsAny<Expression<Func<Author, bool>>>()))
                .Returns(Task.FromResult(new Author
                {
                    Id = 1,
                    AppUserName = "admin",
                    Email = "test@test.com"
                }));
            _unitOfWork.Setup(x => x.Authors).Returns(_authorRepository.Object);

            _storageService
                .Setup(x => x.UploadFromWeb(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((Uri u, string s1, string s2) => Task.FromResult(new AssetItem { Url = u.ToString() }));
        }

        string GetAppRoot()
        {
            Assembly assembly;
            var assemblyName = "Core.Tests";

            assembly = Assembly.Load(new AssemblyName(assemblyName));

            var uri = new UriBuilder(assembly.CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var root = Path.GetDirectoryName(path);
            root = root.Substring(0, root.IndexOf(assemblyName));
            root = root.Replace($"tests{_separator}", $"src{_separator}");

            root = Path.Combine(root, "App");
            root = Path.Combine(root, "wwwroot");
            root = Path.Combine(root, "data");

            return root;
        }

        private AppDbContext GetDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var context = new AppDbContext(options);           

            //context.Seed()
            //context.Users.Add(new AppUser { Id = "admin", UserName = "admin" });
            //context.SaveChanges();

            return context;
        }

        public AppDbContext Context => InMemoryContext();
        private AppDbContext InMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var context = new AppDbContext(options);

            return context;
        }

        public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }
    }
}