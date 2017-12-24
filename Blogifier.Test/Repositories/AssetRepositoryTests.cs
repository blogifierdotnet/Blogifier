using Blogifier.Core.Common;
using Blogifier.Core.Data;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async void Find_By_NotMatching_Id_Returns_0_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1);

            // act 
            var result = await sut.Find(x => x.Id == -1, pager);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal(0, result.Count());
        }

        [Fact]
        public async void Find_ById_Matching_1_Asset_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1, 1);

            // act 
            var result = await sut.Find(x => x.Id == 8, pager);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal(8, result.First().Id);
        }

        [Fact]
        public async void Find_ByTitle_Matching_10_Assets_Returns_10_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db =await GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1);

            // act 
            var result = await sut.Find(x => x.Title.Contains("Asset"), pager);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async void Find_ByTitle_Returns_OrderedResults()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1);

            // act 
            var result = await sut.Find(x => x.Title.Contains("Asset"), pager);
            await ClearMemoryDb(dbName);

            // assert
            Assert.True(result.First().LastUpdated > result.Last().LastUpdated);
        }

        [Fact]
        public async void Find_ByTitle_Matching_10_Assets_With_1ItemPerPage_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await GetMemoryDb(dbName);
            var sut = new AssetRepository(db);
            var pager = new Pager(1, 1);

            // act 
            var result = await sut.Find(x => x.Title.Contains("Asset"), pager);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal(1, result.Count());
        }

        private async Task<BlogifierDbContext> GetMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<BlogifierDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var context = new BlogifierDbContext(options);

            await context.Assets.AddRangeAsync(_assets);
            await context.SaveChangesAsync();

            return context;
        }

        private async Task ClearMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<BlogifierDbContext>()
               .UseInMemoryDatabase(dbName).Options;

            using (var context = new BlogifierDbContext(options))
            {
                context.Assets.RemoveRange(_assets);
                await context.SaveChangesAsync();
            }
        }
    }
}
