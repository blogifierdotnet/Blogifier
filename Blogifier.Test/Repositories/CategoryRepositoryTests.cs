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
    public class CategoryRepositoryTests
    {
        private readonly IEnumerable<Category> _categories = Enumerable.Range(1, 10)
            .Select(i => new Category
            {
                Id = i,
                Title = $"cat {i}",
                Slug = $"slug {i}"
            });

        private readonly IEnumerable<BlogPost> _blogPosts = Enumerable.Range(1, 10)
            .Select(i => new BlogPost
            {
                Id = i,
                Title = $"blogpost {i}"
            });

        private readonly IEnumerable<PostCategory> _postCategories = Enumerable.Range(1, 10)
            .Select(i => new PostCategory
            {
                Id = i,
                BlogPostId = i,
                CategoryId = i,
                LastUpdated = DateTime.Now.AddDays(-i)
            });

        [Fact]
        public async void Find_By_NotMatching_Id_Returns_0_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);
            var pager = new Pager(1);

            // act
            var result = await sut.Find(x => x.Id == 321, pager);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public async void Find_ById_Matching_1_Category_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);
            var pager = new Pager(1);

            // act
            var result = await sut.Find(x => x.Id == 1, pager);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void Find_WithNullPager_Returns_All_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.Find(x => x.Id == 1 || x.Id == 2, null);            

            // assert
            Assert.Equal(2, result.Count());
            await ClearMemoryDb(dbName);
        }

        [Fact]
        public async void Find_ByTitle_Matching_10_Cat_Returns_10_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.Find(x => x.Title.Contains("cat"), null);            

            // assert
            Assert.Equal(10, result.Count());
            await ClearMemoryDb(dbName);
        }

        [Fact]
        public async void Find_ByTitle_Returns_Ordered_Results()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);
            var pager = new Pager(1);

            // act
            var result = await sut.Find(x => x.Title.Contains("cat"), pager);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal("cat 1", result.First().Title);
            Assert.Equal("cat 9", result.Last().Title);
        }

        [Fact]
        public async void Find_ByTitle_Matching_10_Categs_With_1ItemPerPage_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);
            var pager = new Pager(1, 1);

            // act
            var result = await sut.Find(x => x.Title.Contains("cat"), pager);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void GetPostCategories_Having_IncorrectPostId_Returns_0_Items()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.PostCategories(-1);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public async void GetPostCategories_CorrectPostId_Returns_1_Result()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.PostCategories(3);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void GetPostCategories_CorrectPostId_Returns_Correct_Category()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.PostCategories(3);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal("3", result.First().Value);
            Assert.Equal("cat 3", result.First().Text);
        }

        [Fact]
        public async void GetCategoryList_IncorrectId_Returns_0_Items()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.CategoryList(x => x.Id == -1);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public async void GetCategoryList_CorrectId_Returns_1_Item()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.CategoryList(x => x.Id == 5);
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void GetCategoryList_MatchAllItems_Returns_All_Items()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.CategoryList(x => x.Title.Contains("cat"));
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async void GetCategoryList_Returns_Ordered_Items()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.CategoryList(x => x.Title.Contains("cat"));
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal("cat 1", result.First().Text);
            Assert.Equal("cat 9", result.Last().Text);
        }

        [Fact]
        public async void SingleIncluded_NotMatchingPredicate_Returns_0_Items()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.SingleIncluded(x => x.Title == "java");
            await ClearMemoryDb(dbName);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async void SingleIncluded_MatchingPredicate_Returns_1_Item()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.SingleIncluded(x => x.Title == "cat 1");
            await ClearMemoryDb(dbName);

            // assert
            Assert.Equal("cat 1", result.Title);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async void SingleIncluded_MatchingPredicate_Result_Contains_PostCategories()
        {
            // arrange
            var dbName = Guid.NewGuid().ToString();
            var db = await SetupMemoryDb(dbName);
            var sut = new CategoryRepository(db);

            // act
            var result = await sut.SingleIncluded(x => x.Title == "cat 5");
            await ClearMemoryDb(dbName);

            // assert
            Assert.NotNull(result.PostCategories);
        }

        [Fact]
        public async void RemoveCategoryFromPost_NonMatching_RemovesNone()
        {
            var dbName = Guid.NewGuid().ToString();
            try
            {
                // arrange
                var db = await SetupMemoryDb(dbName);
                var sut = new CategoryRepository(db);

                // act
                var result = await sut.RemoveCategoryFromPost(-1, -1);

                // assert
                Assert.False(result);
                Assert.Equal(10, await db.PostCategories.CountAsync());
            }
            finally
            {
                await ClearMemoryDb(dbName);
            }
        }

        [Fact]
        public async void RemoveCategoryFromPost_Matching_RemovesOne()
        {
            var dbName = Guid.NewGuid().ToString();
            try
            {
                // arrange
                var db = await SetupMemoryDb(dbName);
                var sut = new CategoryRepository(db);

                // act
                var result = await sut.RemoveCategoryFromPost(1, 1);
                await db.SaveChangesAsync();

                // assert
                Assert.True(result);
                Assert.Equal(9, await db.PostCategories.CountAsync());
            }
            finally
            {
                var options = new DbContextOptionsBuilder<BlogifierDbContext>()
                    .UseInMemoryDatabase(dbName).Options;

                using (var context = new BlogifierDbContext(options))
                {
                    context.Categories.RemoveRange(_categories);
                    context.BlogPosts.RemoveRange(_blogPosts);
                    context.PostCategories.RemoveRange(_postCategories.Where(pc => pc.Id != 1));
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task<BlogifierDbContext> SetupMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<BlogifierDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var context = new BlogifierDbContext(options);

            await context.Categories.AddRangeAsync(_categories);
            await context.BlogPosts.AddRangeAsync(_blogPosts);
            await context.PostCategories.AddRangeAsync(_postCategories);
            await context.SaveChangesAsync();

            return context;
        }

        private async Task ClearMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<BlogifierDbContext>()
               .UseInMemoryDatabase(dbName).Options;

            using (var context = new BlogifierDbContext(options))
            {
                context.Categories.RemoveRange(_categories);
                context.BlogPosts.RemoveRange(_blogPosts);
                context.PostCategories.RemoveRange(_postCategories);
                await context.SaveChangesAsync();
            }
        }
    }
}
