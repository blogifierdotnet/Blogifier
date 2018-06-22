using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class FeedImportServiceTests
    {
        Mock<UserManager<AppUser>> _um = new Mock<UserManager<AppUser>>();
        Mock<SignInManager<AppUser>> _sm = new Mock<SignInManager<AppUser>>();

        IUnitOfWork _db;
        IStorageService _ss;

        static string _separator = System.IO.Path.DirectorySeparatorChar.ToString();

        [Fact]
        public async Task CanImportFromRssFeed()
        {
            var sut = GetSut();

            var fileName = $"{_ss.Location}{_separator}_init{_separator}_test{_separator}be3.xml";
            var result = await sut.Import(fileName, "admin");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        private FeedImportService GetSut()
        {
            
            _db = new UnitOfWork(GetDb("blogifier"), TestUserManager<AppUser>(), null);
            _ss = new StorageService(null);

            return new FeedImportService(_db, _ss);
        }

        private AppDbContext GetDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var context = new AppDbContext(options);

            context.Users.Add(new AppUser { Id = "admin", UserName = "admin" });
            context.SaveChanges();

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