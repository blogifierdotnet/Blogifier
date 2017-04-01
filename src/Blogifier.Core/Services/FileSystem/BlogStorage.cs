using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.FileSystem
{
	public enum ThemeType
	{
		Blog, Admin
	}

    public class BlogStorage : IBlogStorage
    {
        string _blogSlug;
        string _separator = Path.DirectorySeparatorChar.ToString();
		string _uploadFolder = ApplicationSettings.BlogStorageFolder;

        public BlogStorage(string blogSlug)
        {
            // can be null when blog not yet created
            // and called to get themes for new profile
            if (!string.IsNullOrEmpty(blogSlug))
            {
                _blogSlug = blogSlug;

                if (!Directory.Exists(Location))
                    CreateFolder("");
            }
        }

        public string Location
        {
            get
            {
				var path = Path.Combine(ApplicationSettings.WebRootPath, _uploadFolder);
				if (!string.IsNullOrEmpty(_blogSlug))
                {
                    path = Path.Combine(path, _blogSlug);
                }
                return path;
            }
        }

        public IList<SelectListItem> GetThemes(ThemeType themeType)
        {
            var themes = new List<SelectListItem>();
            themes.Add(new SelectListItem { Value = "Standard", Text = "Standard" });

            var path = Path.Combine(GetRoot(), "Views");
            path = Path.Combine(path, "Blogifier");
            path = Path.Combine(path, "Themes");

			var themeFolder = themeType == ThemeType.Admin ? "Admin" : "Blog";
			path = Path.Combine(path, themeFolder);

            if (Directory.Exists(path))
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    if(dir.ExtractTitle().ToLower() != "standard")
                    {
                        themes.Add(new SelectListItem { Value = dir.ExtractTitle(), Text = dir.ExtractTitle() });
                    }
                }
            }
            return themes;
        }

        public IList<string> GetAssets(string path)
        {
            var items = new List<string>();
            path = path.Replace("/", _separator);
            var dir = string.IsNullOrEmpty(path) ? Location : Path.Combine(Location, path);
            try
            {
                foreach (string f in Directory.GetFiles(dir))
                    items.Add(f);

                foreach (string d in Directory.GetDirectories(dir))
                    items.Add(d);
            }
            catch { }
            return items;
        }     

        public async Task <Asset> UploadFormFile(IFormFile file, string root, string path = "")
        {
            path = path.Replace("/", _separator);
            var fileName = file.FileName;
            var filePath = string.IsNullOrEmpty(path) ?
                Path.Combine(Location, fileName) :
                Path.Combine(Location, path + _separator + fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                return new Asset
                {
                    Title = fileName,
                    Path = filePath,
                    Url = GetUrl(filePath, root),
                    Length = file.Length
                };
            }
        }

        public async Task <Asset> UploadFromWeb(Uri requestUri, string root, string path = "")
        {
            path = path.Replace("/", _separator);
            var fileName = TitleFromUri(requestUri);
            var filePath = string.IsNullOrEmpty(path) ? 
                Path.Combine(Location, fileName) : 
                Path.Combine(Location, path + _separator + fileName);

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
                {
                    using (
                        Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(),
                        stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true))
                    {
                        await contentStream.CopyToAsync(stream);
                        return new Asset
                        {
                            Title = fileName,
                            Path = filePath,
                            Url = GetUrl(filePath, root),
                            Length = contentStream.Length
                        };
                    }
                }
            }
        }

        public void CreateFolder(string path)
        {
            var dir = GetFullPath(path);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public void DeleteFolder(string path)
        {
            var dir = GetFullPath(path);

            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
        }

        public void DeleteFile(string path)
        {
            File.Delete(GetFullPath(path));
        }

        string GetFullPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return Location;
            else
                return Path.Combine(Location, path.Replace("/", _separator));
        }

		string GetUrl(string path, string root)
        {
			var url = path.ReplaceIgnoreCase(Location, "").Replace(_separator, "/");
			return _uploadFolder + "/" + _blogSlug + url;
        }

        string GetRoot()
        {
            var root = Directory.GetCurrentDirectory();
            // unit tests workaround
            return root.Replace("Blogifier.Tests", "Blogifier.Web");
        }

        string TitleFromUri(Uri uri)
        {
            var title = uri.ToString().ToLower();
            title = title.Replace("%2f", "/");

            if (title.EndsWith(".axdx"))
            {
                title = title.Replace(".axdx", "");
            }
            if (title.Contains("image.axd?picture="))
            {
                title = title.Substring(title.IndexOf("image.axd?picture=") + 18);
            }
            if (title.Contains("encrypted-tbn") || title.Contains("base64,"))
            {
                Random rnd = new Random();
                title = string.Format("{0}.png", rnd.Next(1000, 9999));
            }

            if (title.Contains("/"))
            {
                title = title.Substring(title.LastIndexOf("/"));
            }           

            return title.Replace("/", "");
        }
    }
}
