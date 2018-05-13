using Core.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class FileSystem
    {
        IStorageService _storage;
        static Uri _imgUri = new Uri("http://dnbe.net/v01/images/Picasa.png");
        static string _separator = System.IO.Path.DirectorySeparatorChar.ToString();

        public FileSystem()
        {
            _storage = new StorageService(null);
        }

        [Fact]
        public void VerifyStorageLocation()
        {
            var path = string.Format("App{0}wwwroot{0}data", _separator);
            Assert.EndsWith(path, _storage.Location);
        }

        [Fact]
        public async Task CanCreateDeleteFiles()
        {
            var result = await _storage.UploadFromWeb(_imgUri, "/");
            Assert.EndsWith("Picasa.png", result.Url, StringComparison.OrdinalIgnoreCase);
            _storage.DeleteFile("Picasa.png");

            _storage.CreateFolder("foo");
            result = await _storage.UploadFromWeb(_imgUri, "/", "foo");
            Assert.EndsWith("foo/Picasa.png", result.Url, StringComparison.OrdinalIgnoreCase);

            _storage.DeleteFolder("foo");
            var assets = _storage.GetAssets("");
            Assert.False(assets.Contains(_storage.Location + _separator + "foo"));
        }
    }
}
