using Core.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class StorageServiceTests
    {
        IStorageService _storage;
        static string _img = "picasa.png";
        static Uri _imgUri = new Uri("http://dnbe.net/v01/images/" + _img);
        static string _separator = System.IO.Path.DirectorySeparatorChar.ToString();

        public StorageServiceTests()
        {
            _storage = new StorageService(null);
        }

        [Fact]
        public void HaveValidLocation()
        {
            var path = string.Format("App{0}wwwroot{0}data", _separator);
            Assert.EndsWith(path, _storage.Location);
        }

        [Fact]
        public void CanGetThemes()
        {
            var themes = _storage.GetThemes();

            Assert.NotNull(themes);
        }

        [Fact]
        public async Task CanCreateDeleteFile()
        {
            var result = await _storage.UploadFromWeb(_imgUri, "/");
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
            await _storage.UploadFromWeb(_imgUri, "/", "foo");

            _storage.DeleteFolder(folder);
            Assert.False(System.IO.Directory.Exists(folder));
        }
    }
}
