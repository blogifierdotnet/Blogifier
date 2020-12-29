using Blogifier.Core.Providers;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace Blogifier.Tests
{
	public class RssImportProviderTests
	{
      private readonly TestHelper _testHelper;
      private readonly string _feedUrl = "http://localhost/blog/feed/rss";

      public RssImportProviderTests()
		{
         _testHelper = new TestHelper();
      }

      [Fact]
      public async Task CanImportFromRssFeed()
      {
         var sut = GetSut();

         SyndicationFeed feed = SyndicationFeed.Load(XmlReader.Create(_feedUrl));

         var result = await sut.ImportSyndicationItem(feed.Items.First(), 1, feed.BaseUri);

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
