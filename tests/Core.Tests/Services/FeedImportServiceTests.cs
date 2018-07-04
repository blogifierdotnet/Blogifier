using App;
using Core.Data;
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
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class FeedImportServiceTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        Mock<UserManager<AppUser>> _um = new Mock<UserManager<AppUser>>();
        Mock<SignInManager<AppUser>> _sm = new Mock<SignInManager<AppUser>>();

        IUnitOfWork _db;
        IStorageService _ss;

        static string _separator = Path.DirectorySeparatorChar.ToString();

        private readonly WebApplicationFactory<Startup> _factory;

        public FeedImportServiceTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }



        [Fact]
        public void UnitCosts()
        {
            var inputFile = "Original.txt";
            var outputFile = "GuardRailMissing.txt";
            var existsFile = "GuardRailExists.txt";
            var errorFile = "Errors.txt";

            var lines = File.ReadAllLines(inputFile);

            for (var i = 0; i < lines.Length; i += 1)
            {
                var line = lines[i];

                try
                {
                    if (line.StartsWith("940"))
                    {
                        File.AppendAllText(existsFile, line + System.Environment.NewLine);
                    }
                    else
                    {
                        File.AppendAllText(outputFile, line + System.Environment.NewLine);
                    }
                }
                catch (System.Exception ex)
                {
                    File.AppendAllText(errorFile, line + " " + ex.Message + System.Environment.NewLine);
                }
            }

            Assert.True(true);
        }


        //[Fact]
        //public async Task CanImportFromRssFeed()
        //{
        //    var sut = GetSut();

        //    var fileName = $"{_ss.Location}{_separator}_init{_separator}_test{_separator}be3.xml";
        //    var result = await sut.Import(fileName, "admin");

        //    Assert.NotNull(result);
        //    Assert.NotEmpty(result);
        //}

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

            //AppSettings.DbOptions = options => options.UseInMemoryDatabase("abc");

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


    //public class TestingFunctionalTests : IClassFixture<WebApplicationFactory<Startup>>
    //{
    //    public HttpClient Client { get; }
    //    public WebApplicationFactory<Startup> Server { get; }

    //    public TestingFunctionalTests(WebApplicationFactory<Startup> server)
    //    {
    //        Client = server.CreateClient();
    //        Server = server;
    //    }

    //    [Fact]
    //    public async Task GetHomePage()
    //    {
    //        // Arrange & Act
    //        var response = await Client.GetAsync("/");

    //        // Assert
    //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //    }
    //}

}