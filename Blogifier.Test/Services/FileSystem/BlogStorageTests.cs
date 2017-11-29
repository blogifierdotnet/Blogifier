using Blogifier.Core.Services.FileSystem;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Blogifier.Tests.Services.FileSystem
{
    public class BlogStorageTests
    {
        static Uri _imgUri = new Uri("http://dnbe.net/v01/images/Picasa.png");
        static BlogStorage _storage;
        static string _separator = System.IO.Path.DirectorySeparatorChar.ToString();

        public BlogStorageTests()
        {
            _storage = new BlogStorage("test");
        }

        [Fact]
        public void VerifyStorageLocation()
        {
            var path = string.Format("WebApp{0}wwwroot{0}blogifier{0}data{0}test", _separator);
            Assert.True(_storage.Location.EndsWith(path));
        }

        [Fact]
        public void CanCreateDeleteFolder()
        {
            // foo
            _storage.CreateFolder("foo");
            var result = _storage.GetAssets("");
            Assert.True(result.Contains(_storage.Location + string.Format("{0}foo", _separator)));

            _storage.DeleteFolder("foo");
            result = _storage.GetAssets("");
            Assert.False(result.Contains(_storage.Location + string.Format("{0}foo", _separator)));

            // foo/bar
            _storage.CreateFolder("foo/bar");
            result = _storage.GetAssets("foo");
            Assert.True(result.Contains(_storage.Location + string.Format("{0}foo{0}bar", _separator)));

            _storage.DeleteFolder("foo");
            result = _storage.GetAssets("foo");
            Assert.False(result.Contains(_storage.Location + string.Format("{0}foo{0}bar", _separator)));

            // foo\\bar
            _storage.CreateFolder("foo\\bar");
            result = _storage.GetAssets("foo");
            Assert.True(result.Contains(_storage.Location + string.Format("{0}foo{0}bar", _separator)));

            _storage.DeleteFolder("foo");
            result = _storage.GetAssets("foo");
            Assert.False(result.Contains(_storage.Location + string.Format("{0}foo{0}bar", _separator)));
        }

        [Fact]
        public async Task CanCreateDeleteFiles()
        {
            var result = await _storage.UploadFromWeb(_imgUri, "/");
            Assert.True(result.Url.EndsWith("Picasa.png", StringComparison.OrdinalIgnoreCase));
            _storage.DeleteFile("Picasa.png");

            _storage.CreateFolder("foo");
            result = await _storage.UploadFromWeb(_imgUri, "/", "foo");
            Assert.True(result.Url.EndsWith("foo/Picasa.png", StringComparison.OrdinalIgnoreCase));

            _storage.DeleteFolder("foo");
            var assets = _storage.GetAssets("");
            Assert.False(assets.Contains(_storage.Location + _separator + "foo"));
        }

        [Fact]
        public async Task CanGetAssets()
        {
            _storage.CreateFolder("foo");
            var img = await _storage.UploadFromWeb(_imgUri, "/", "foo");

            var result = _storage.GetAssets("");
            Assert.True(result.Count > 0);
            Assert.True(result[0].EndsWith(string.Format("{0}foo", _separator)));

            result = _storage.GetAssets("foo");
            Assert.True(result.Count > 0);
            Assert.True(result[0].EndsWith(string.Format("{0}foo{0}Picasa.png", _separator), StringComparison.OrdinalIgnoreCase));

            _storage.DeleteFolder("foo");
        }
    }
}