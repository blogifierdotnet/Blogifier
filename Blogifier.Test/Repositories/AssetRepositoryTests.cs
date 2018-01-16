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
    public class AssetRepositoryTests
    {
        private readonly IEnumerable<Asset> _assets = Enumerable.Range(1, 10)
                .Select(i => new Asset
                {
                    Id = i,
                    AssetType = AssetType.Image,
                    DownloadCount = i * 100,
                    LastUpdated = DateTime.Now.AddDays(-i),
                    Title = $"Asset {i}",
                    Length = i * 1000,
                    ProfileId = i * 2,
                    Path = $"asset_{i}",
                    Url = "..."
                });

        [Fact]
        public void Find_By_NotMatching_Id_Returns_0_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1);

            // act 
            var result = sut.Find(x => x.Id == -1, pager);
            ClearMemoryDb(dbName);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public void Find_ById_Matching_1_Asset_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1, 1);

            // act 
            var result = sut.Find(x => x.Id == 8, pager);
            ClearMemoryDb(dbName);

            // assert
            Assert.Equal(8, result.First().Id);
        }

        [Fact]
        public void Find_ByTitle_Matching_10_Assets_Returns_10_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1);

            // act 
            var result = sut.Find(x => x.Title.Contains("Asset"), pager);
            ClearMemoryDb(dbName);

            // assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public void Find_ByTitle_Returns_OrderedResults()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1);

            // act 
            var result = sut.Find(x => x.Title.Contains("Asset"), pager);
            ClearMemoryDb(dbName);

            // assert
            Assert.True(result.First().LastUpdated > result.Last().LastUpdated);
        }

        [Fact]
        public void Find_ByTitle_Matching_10_Assets_With_1ItemPerPage_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1, 1);

            // act 
            var result = sut.Find(x => x.Title.Contains("Asset"), pager);
            ClearMemoryDb(dbName);

            // assert
            Assert.Single(result);
        }

        private BlogifierDbContext GetMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<BlogifierDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var context = new BlogifierDbContext(options); 

            context.Assets.AddRange(_assets);
            context.SaveChanges();

            return context;
        }

        private void ClearMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<BlogifierDbContext>()
               .UseInMemoryDatabase(dbName).Options;

            using (var context = new BlogifierDbContext(options))
            {
                context.Assets.RemoveRange(_assets);
                context.SaveChanges();
            }
        }
    }
}
