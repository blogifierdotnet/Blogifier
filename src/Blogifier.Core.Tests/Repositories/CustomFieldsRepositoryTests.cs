using Blogifier.Core.Data;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Repositories
{
    public class CustomFieldsRepositoryTests
	{
		[Fact]
		public async Task CanSaveAndGetCustomField()
		{
			var db = GetSut();
			var sut = new CustomFieldRepository(db);

			sut.Add(new CustomField { AuthorId = 1, Name = "social|foo|1", Content = "http://foo.com" });
			await db.SaveChangesAsync();

			var result = sut.Single(f => f.Name.Contains("social|foo"));
			Assert.NotNull(result);

			sut.Remove(result);
			await db.SaveChangesAsync();

			result = sut.Single(f => f.Name.Contains("social|foo"));
			Assert.Null(result);
		}

		[Fact]
		public async Task CanSaveAndGetSocialField()
		{
			var db = GetSut();
			var sut = new CustomFieldRepository(db);

			await sut.SaveSocial(new SocialField { 
				AuthorId = 0, 
				Title = "Foo", 
				Icon = "fa-facebook",
				Name = "social|foo|1",
				Rank = 1, 
				Content = "http://foo.com" 
			});

			var socials = await sut.GetSocial();
			Assert.NotNull(socials);

			var result = sut.Single(f => f.Name.Contains("social|foo"));
			Assert.NotNull(result);

			sut.Remove(result);
			await db.SaveChangesAsync();

			result = sut.Single(f => f.Name.Contains("social|foo"));
			Assert.Null(result);
		}

		private AppDbContext GetSut()
		{
			var helper = new DbHelper();
			return helper.GetDbContext();
		}
	}
}