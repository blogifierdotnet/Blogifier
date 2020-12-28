using Blogifier.Core.Providers;
using System.Threading.Tasks;
using Xunit;

namespace Blogifier.Tests
{
   public class RssImportProviderTests
	{
      private readonly TestHelper _testHelper;
      private readonly string _rssFile;
      private readonly string _imgUrl = "http://via.placeholder.com/200X120";


      public RssImportProviderTests()
		{
         _testHelper = new TestHelper();
         _rssFile = $"{_testHelper.ContextRoot}tests{_testHelper.Slash}data{_testHelper.Slash}test3.xml";
      }

      [Fact]
      public async Task CanImportFromRssFeed()
      {
         var sut = GetSut();

         var result = await sut.Import(_rssFile, 1);

         Assert.NotNull(result);
      }

      IRssImportProvider GetSut()
		{
         var dbContext = _testHelper.GetDbContext();
         var storageProvider = new StorageProvider();

         return new RssImportProvider(dbContext, storageProvider);
      }
   }
}
