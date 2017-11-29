using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.FileSystem
{
    public interface IBlogStorage
    {
        string Location { get; }
        void CreateFolder(string path);
        void DeleteFile(string path);
        void DeleteFolder(string path);
        IList<string> GetAssets(string path);
        Task<Asset> UploadFormFile(IFormFile file, string root, string path = "");
        Task<Asset> UploadBase64Image(string baseImg, string root, string path = "");
        Task<Asset> UploadFromWeb(Uri requestUri, string root, string path = "");
    }
}
