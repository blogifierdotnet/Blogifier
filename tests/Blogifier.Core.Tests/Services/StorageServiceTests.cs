using Blogifier.Core;
using Blogifier.Core.Services;
using Blogifier.Models;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class StorageServiceTests
    {
        IStorageService _storage;
        static string _img = "cover.png";
        static Uri _uri1 = new Uri("http://blogifier.net/admin/img/" + _img);
        static Uri _uri2 = new Uri("http://dnbe.net/v01/images/mp3player.png");
        static string _separator = System.IO.Path.DirectorySeparatorChar.ToString();
        private readonly Mock<ILogger<StorageService>> _logger = new Mock<ILogger<StorageService>>();
        private readonly Mock<IFeatureManager> _featureMgr = new Mock<IFeatureManager>();

        public StorageServiceTests()
        {
            _storage = new StorageService(null, _logger.Object, _featureMgr.Object);
        }

        [Fact]
        public void HaveValidLocation()
        {
            var path = string.Format("Blogifier{0}wwwroot{0}data", _separator);
            Assert.EndsWith(path, _storage.Location);
        }

        [Fact]
        public void CanGetThemes()
        {
            var themes = _storage.GetThemes();

            Assert.NotNull(themes);
        }

        [Fact]
        public async Task CanGetAssets()
        {
            // create folders and files
            _storage.CreateFolder("foo");
            _storage.CreateFolder("foo/bar");

            await _storage.UploadFromWeb(_uri1, "/", "foo");
            await _storage.UploadFromWeb(_uri2, "/", "foo/bar");

            // get all files from folder structure
            var assets = _storage.GetAssets("foo");

            Assert.NotNull(assets);
            Assert.NotEmpty(assets);

            // cleanup
            _storage.DeleteFolder("foo");

            var folder = System.IO.Path.Combine(_storage.Location, "foo");
            Assert.False(System.IO.Directory.Exists(folder));
        }

        [Fact]
        public async Task CanFindAssets()
        {
            AppSettings.ImageExtensions = "png,jpg,gif,bmp,tiff";

            var pager = new Pager(1);
            var assets = await _storage.Find(null, pager, "");

            Assert.NotNull(assets);
            Assert.NotEmpty(assets);
        }

        [Fact]
        public async Task CanCreateDeleteFile()
        {
            var result = await _storage.UploadFromWeb(_uri1, "/");
            Assert.True(System.IO.File.Exists(result.Path));

            _storage.DeleteFile(_img);
            Assert.False(System.IO.File.Exists(result.Path));
        }

        [Fact]
        public async Task CanCreateDeleteFolder()
        {
            var folder = System.IO.Path.Combine(_storage.Location, "foo");

            _storage.CreateFolder("foo");
            Assert.True(System.IO.Directory.Exists(folder));

            // not just emply folder
            await _storage.UploadFromWeb(_uri1, "/", "foo");

            _storage.DeleteFolder(folder);
            Assert.False(System.IO.Directory.Exists(folder));
        }

        [Theory]
        [InlineData("http://blogifier.net/admin/img/cover.png")]
        [InlineData("https://raw.githubusercontent.com/blogifierdotnet/Design/master/v1.5/01.jpg")]
        public async Task CanCreateCoverImagesWithThumbnails(string img)
        {
            AssetItem result = await _storage.UploadFromWeb(new Uri(img), "/");
            Assert.True(System.IO.File.Exists(result.Path));

            _storage.DeleteFile(result.Title);
            Assert.False(System.IO.File.Exists(result.Path));

            if (_featureMgr.Object.IsEnabledAsync(nameof(AppFeatureFlags.ThumbnailsEnabled)).Result)
            {
                string thumbPath = result.Path.Replace(result.Title, $"thumbs\\{result.Title}");
                Assert.True(System.IO.File.Exists(thumbPath));              

                _storage.DeleteFile(thumbPath);
                Assert.False(System.IO.File.Exists(thumbPath));
            }
        }

        [Theory]
        [InlineData("http://blogifier.net/admin/img/avatar.png")]
        public async Task SmallImagesShouldNotCreateThumbnails(string img)
        {
            AssetItem result = await _storage.UploadFromWeb(new Uri(img), "/");
            Assert.True(System.IO.File.Exists(result.Path));

            string thumbPath = result.Path.Replace(result.Title, $"thumbs\\{result.Title}");
            Assert.False(System.IO.File.Exists(thumbPath));

            _storage.DeleteFile(result.Title);
            Assert.False(System.IO.File.Exists(result.Path));
        }
    }
}