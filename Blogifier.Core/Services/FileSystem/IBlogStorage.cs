using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        IList<SelectListItem> GetThemes();
        Task<Asset> UploadFormFile(IFormFile file, string root, string path = "");
        Task<Asset> UploadBase64Image(string baseImg, string root, string path = "");
        Task<Asset> UploadFromWeb(Uri requestUri, string root, string path = "");
    }
}
