using Blogifier.Core.Common;
using Blogifier.Core.Data;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Data.Repositories;
using Blogifier.Core.Services.FileSystem;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Blogifier.Test.Services.Syndication
{
    public class FeedParserTests
    {
        DbContextOptions<BlogifierDbContext> _options;

        public FeedParserTests()
        {
            var builder = new DbContextOptionsBuilder<BlogifierDbContext>();

            ApplicationSettings.DatabaseOptions(builder);

            _options = builder.Options;
        }

        [Theory()]
        [InlineData(@"SeedData\Feeds\BlogEngineRSS.xml")]
        [InlineData(@"SeedData\Feeds\WordPressRSS.xml")]
        public async Task CanParseRssFeed(string feed)
        {
            using (var context = new BlogifierDbContext(_options))
            {
                var storage = new BlogStorage("test");
                var path = Path.Combine(GetRoot(), feed);

                var uow = new UnitOfWork(context);

                var profile = await uow.Profiles.Single(p => p.IdentityName == "test");

                if(profile == null)
                {
                    profile = new Profile
                    {
                        IdentityName = "test",
                        AuthorName = "test",
                        AuthorEmail = "test@us.com",
                        Title = "test",
                        Slug = "test",
                        Description = "the test",
                        BlogTheme = "Standard"
                    };

                    await uow.Profiles.Add(profile);
                    await uow.Complete();
                }

                Assert.True(await context.Profiles.CountAsync() > 0);

                var service = new RssService(uow, null);

                var model = new RssImportModel
                {
                    FeedUrl = path,
                    ProfileId = profile.Id
                };

                var result = await service.Import(model);

                Assert.True(await context.BlogPosts.CountAsync() > 1);
            }

            Assert.NotNull(feed);
        }

        [Theory()]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData("Mon, 2 May 2016 06:48:00 -0700")]
        [InlineData("Mon, 02 May 2016 06:48:00 -0700")]
        public void CanConvertPuplishedDateToDateTime(string format)
        {
            DateTime dt = Convert.ToDateTime("05/02/2016 08:48:00.00");

            var result = SystemClock.RssPubishedToDateTime(format);

            if (format == null || format == "" || format == "     ")
            {
                Assert.Equal(DateTime.MinValue, result);
            }
            else
            {
                Assert.Equal(dt, result);
            }
        }

        string GetRoot()
        {
            var assembly = Assembly.Load(new AssemblyName("Blogifier.Test"));
            var uri = new UriBuilder(assembly.CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var root = Path.GetDirectoryName(path);

            return root.Substring(0, root.IndexOf("\\bin\\"));
        }
    }
}
