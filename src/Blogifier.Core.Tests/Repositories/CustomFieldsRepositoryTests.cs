using Blogifier.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Blogifier.Core.Tests.Repositories
{
	public class CustomFieldsRepositoryTests
	{
		[Fact]
		public async Task CanSaveAndGetCustomField()
		{
			var db = GetSut();
			var sut = new CustomFieldRepository(db);

			sut.Add(new CustomField { AuthorId = 1, Name = "social|facebook|1", Content = "http://your.facebook.page.com" });
			await db.SaveChangesAsync();

			var result = sut.Single(f => f.Name.Contains("social|facebook"));
			Assert.NotNull(result);

			sut.Remove(result);
			await db.SaveChangesAsync();

			result = sut.Single(f => f.Name.Contains("social|facebook"));
			Assert.Null(result);
		}

		[Fact]
		public async Task CanSaveAndGetSocialField()
		{
			var db = GetSut();
			var sut = new CustomFieldRepository(db);

			await sut.SaveSocial(new SocialField { 
				AuthorId = 0, 
				Title = "Facebook", 
				Icon = "fa-facebook",
				Name = "social|facebook|1",
				Rank = 1, 
				Content = "http://your.facebook.page.com" 
			});

			var socials = await sut.GetSocial();
			Assert.NotNull(socials);

			var result = sut.Single(f => f.Name.Contains("social|facebook"));
			Assert.NotNull(result);

			sut.Remove(result);
			await db.SaveChangesAsync();

			result = sut.Single(f => f.Name.Contains("social|facebook"));
			Assert.Null(result);
		}

		private AppDbContext GetSut()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
					.UseSqlite("DataSource=Blog.db").Options;

			var context = new AppDbContext(options);

			return context;
		}
	}
}