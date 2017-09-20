using Blogifier.Core.Common;
using Blogifier.Core.Data;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blogifier.Test.Repositories
{
    public class CategoryRepositoryTests
    {
        private readonly IEnumerable<Category> _categories = Enumerable.Range(1, 10)
        .Select(i => new Category
        {
            Id = i,
            Title = $"cat {i}",
            Slug = $"slug {i}"
        });

        [Fact]
        private void Find_By_NotMatching_Id_Returns_0_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new CategoryRepository(db);
            var pager = new Pager(1);

            // act
            var result = sut.Find(x => x.Id == 321, pager).ToList();
            ClearMemoryDb(dbName);

            // assert
            Assert.Equal(0, result.Count());
        }

        [Fact]
        private void Find_ById_Matching_1_Category_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new CategoryRepository(db);
            var pager = new Pager(1);

            // act
            var result = sut.Find(x => x.Id == 1, pager).ToList();
            ClearMemoryDb(dbName);

            // assert
            Assert.Equal(1, result.Count());
        }

        [Fact]
        private void Find_WithNullPager_Returns_All_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = sut.Find(x => x.Id == 1 || x.Id == 2, null).ToList();
            ClearMemoryDb(dbName);

            // assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        private void Find_ByTitle_Matching_10_Cat_Returns_10_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = sut.Find(x => x.Title.Contains("cat"), null).ToList();
            ClearMemoryDb(dbName);

            // assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        private void Find_ByTitle_Returns_Ordered_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new CategoryRepository(db);
            var pager = new Pager(1);

            // act
            var result = sut.Find(x => x.Title.Contains("cat"), pager).ToList();
            ClearMemoryDb(dbName);

            // assert
            Assert.Equal("cat 1", result.First().Title);
            Assert.Equal("cat 9", result.Last().Title);
        }

        [Fact]
        private void Find_ByTitle_Matching_10_Categs_With_1ItemPerPage_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new CategoryRepository(db);
            var pager = new Pager(1, 1);

            // act
            var result = sut.Find(x => x.Title.Contains("cat"), pager).ToList();
            ClearMemoryDb(dbName);

            // assert
            Assert.Equal(1, result.Count());
        }

        private BlogifierDbContext GetMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<BlogifierDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var context = new BlogifierDbContext(options);

            context.Categories.AddRange(_categories);
            context.SaveChanges();

            return context;
        }

        private void ClearMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<BlogifierDbContext>()
               .UseInMemoryDatabase(dbName).Options;

            using (var context = new BlogifierDbContext(options))
            {
                context.Categories.RemoveRange(_categories);
                context.SaveChanges();
            }
        }
    }
}
