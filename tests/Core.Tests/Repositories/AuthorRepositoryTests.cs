using Core.Data;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Repositories
{
    public class AuthorRepositoryTests
    {
        private readonly IEnumerable<Author> _authors = Enumerable.Range(1, 12)
            .Select(i => new Author
            {
                Id = i,
                AppUserName = $"test{i}"
            });

        [Fact]
        public async Task Can_Save_New_Author()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AuthorRepository(db);

            var author = new Author
            {
                Id = 25,
                AppUserName = "Test25"
            };

            // act
            await sut.Save(author);
            var result = await sut.GetItem(a => a.AppUserName == "Test25");
            ClearMemoryDb(dbName);

            // assert
            Assert.NotNull(result);
            Assert.True(result.Id == 25);
        }

        [Fact]
        public async Task Can_Remove_Author()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AuthorRepository(db);
            var author = new Author { Id = 25, AppUserName = "Test25" };

            // act
            await sut.Save(author);
            var result1 = await sut.GetItem(a => a.Id == 25);
            await sut.Remove(25);
            var result2 = await sut.GetItem(a => a.Id == 25);
            ClearMemoryDb(dbName);

            // assert
            Assert.NotNull(result1);
            Assert.Null(result2);
        }

        [Fact]
        public async Task GetItem_By_Matching_Id_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AuthorRepository(db);

            // act
            var result = await sut.GetItem(x => x.AppUserName == "test1");
            ClearMemoryDb(dbName);

            // assert
            Assert.NotNull(result);
            Assert.True(result.Id == 1);
        }

        [Fact]
        public async Task GetItems_By_NotMatching_Id_Returns_0_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AuthorRepository(db);
            var pager = new Pager(1);

            // act
            var result = await sut.GetList(x => x.Id == 123, pager);
            ClearMemoryDb(dbName);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetItems_By_Matching_Id_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AuthorRepository(db);
            var pager = new Pager(1);

            // act
            var result = await sut.GetList(x => x.Id == 1, pager);
            ClearMemoryDb(dbName);

            // assert
            Assert.True(result.Count() == 1);
        }

        [Fact]
        public async Task Get_First_Page_Returns_10_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AuthorRepository(db);
            var pager = new Pager(1);

            // act
            var result = await sut.GetList(x => x.Id > 0, pager);
            ClearMemoryDb(dbName);

            // assert
            Assert.True(result.Count() == 10);
        }

        private AppDbContext GetMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var context = new AppDbContext(options);

            context.Authors.AddRange(_authors);
            context.SaveChanges();

            return context;
        }

        private void ClearMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(dbName).Options;

            using (var context = new AppDbContext(options))
            {
                context.RemoveRange(_authors);
                context.SaveChanges();
            }
        }
    }
}