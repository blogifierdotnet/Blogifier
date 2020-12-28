using Blogifier.Core.Providers;
using System.Threading.Tasks;
using Xunit;

namespace Blogifier.Tests
{
	public class StorageProviderTests
	{
      private readonly string _imgUrl = "https://user-images.githubusercontent.com/1932785/81506457-1611e580-92bc-11ea-927e-b826c56ba21b.png";

      [Fact]
      public async Task CanImportFromRssFeed()
      {
         var sut = GetSut();

         var result = await sut.UploadFromWeb(new System.Uri(_imgUrl), "/", "1/2020/12");

         Assert.True(result.Length > 0);
      }

      IStorageProvider GetSut()
      {
         return new StorageProvider();
      }
   }
}
