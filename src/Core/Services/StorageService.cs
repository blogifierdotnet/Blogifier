using Core.Data;
using Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IStorageService
    {
        string Location { get; }
        
        void CreateFolder(string path);
        void DeleteFolder(string path);
        void DeleteAuthor(string name);

        Task<AssetItem> UploadFormFile(IFormFile file, string root, string path = "");
        Task<AssetItem> UploadBase64Image(string baseImg, string root, string path = "");
        Task<AssetItem> UploadFromWeb(Uri requestUri, string root, string path = "");
        void DeleteFile(string path);

        IList<string> GetAssets(string path);
        IList<string> GetThemes();
        bool SelectTheme(string theme);

        string GetHtmlTemplate(string template);

        string GetThemeData(string theme);
        Task SaveThemeData(ThemeDataModel model);

        Task<IEnumerable<AssetItem>> Find(Func<AssetItem, bool> predicate, Pager pager, string path = "", bool sanitize = false);

        Task Reset();
    }

    public class StorageService : IStorageService
    {
        string _blogSlug;
        string _separator = Path.DirectorySeparatorChar.ToString();
        string _uploadFolder = "data";
        IHttpContextAccessor _httpContext;

        private readonly ILogger _logger;

        public StorageService(IHttpContextAccessor httpContext, ILogger<StorageService> logger)
        {
            if(httpContext == null || httpContext.HttpContext == null)
            {
                _blogSlug = "";
            }
            else
            {
                _blogSlug = httpContext.HttpContext.User.Identity.Name;
            }
            
            _httpContext = httpContext;
            _logger = logger;

            if (!Directory.Exists(Location))
                CreateFolder("");
        }

        public string Location
        {
            get
            {
                var path = AppSettings.WebRootPath ?? Path.Combine(GetAppRoot(), "wwwroot");

                path = Path.Combine(path, _uploadFolder.Replace("/", Path.DirectorySeparatorChar.ToString()));

                if (!string.IsNullOrEmpty(_blogSlug))
                {
                    path = Path.Combine(path, _blogSlug);
                }
                return path;
            }
        }

        public IList<string> GetAssets(string path)
        {
            path = path.Replace("/", _separator);
            try
            {
                var dir = string.IsNullOrEmpty(path) ? Location : Path.Combine(Location, path);
                var info = new DirectoryInfo(dir);

                FileInfo[] files = info.GetFiles("*", SearchOption.AllDirectories)
                    .OrderByDescending(p => p.CreationTime).ToArray();

                if(files != null && files.Any())
                {
                    var assets = new List<string>();

                    foreach (FileInfo file in files)
                    {
                        assets.Add(file.FullName);
                    }
                    return assets;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public IList<string> GetThemes()
        {
            var items = new List<string>();
            var dir = Path.Combine(GetAppRoot(), $"wwwroot{_separator}themes");
            try
            {
                foreach (string d in Directory.GetDirectories(dir))
                {
                    if(!d.EndsWith("_active"))
                        items.Add(Path.GetFileName(d));
                }
            }
            catch { }
            return items;
        }

        public bool SelectTheme(string theme)
        {
            var dir = Path.Combine(GetAppRoot(), $"wwwroot{_separator}themes");
            string temp = $"{dir}{_separator}_temp";
            string active = $"{dir}{_separator}_active";
            string source = $"{dir}{_separator}{theme}";

            try
            {
                // backup
                if (Directory.Exists(active))
                    Directory.Move(active, temp);

                Directory.CreateDirectory(active);

                CopyFilesRecursively(new DirectoryInfo(source), new DirectoryInfo(active));

                Directory.Delete(temp, true);

                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    // restore and cleanup
                    if (Directory.Exists(temp))
                    {
                        if (Directory.Exists(active))
                            Directory.Delete(active, true);

                        Directory.Move(temp, active);
                    }
                }
                catch { }
                
                _logger.LogError($"Error replacing theme in the file system: {ex.Message}");
                return false;
            }
        }

        static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }

        public string GetThemeData(string theme)
        {
            string jsonFile = $"{AppSettings.WebRootPath}{_separator}themes{_separator}{theme}{_separator}assets{_separator}{Constants.ThemeDataFile}";
            if (File.Exists(jsonFile))
            {
                using (StreamReader r = new StreamReader(jsonFile))
                {
                    return r.ReadToEnd();
                }
            }
            return "";
        }

        public async Task SaveThemeData(ThemeDataModel model)
        {
            string jsonFile = $"{AppSettings.WebRootPath}{_separator}themes{_separator}{model.Theme}{_separator}assets{_separator}{Constants.ThemeDataFile}";
            if (File.Exists(jsonFile))
            {
                File.Delete(jsonFile);
                File.WriteAllText(jsonFile, model.Data);
            }
            await Task.CompletedTask;
        }

        public string GetHtmlTemplate(string template)
        {
            string content = "<p>Not found</p>";
            try
            {
                var path = AppSettings.WebRootPath ?? Path.Combine(GetAppRoot(), "wwwroot");
                path = Path.Combine(path, "templates");
                path = Path.Combine(path, $"{template}.html");

                if (File.Exists(path))
                {
                    content = File.ReadAllText(path);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return content;
        }

        public async Task<AssetItem> UploadFormFile(IFormFile file, string root, string path = "")
        {
            path = path.Replace("/", _separator);

            VerifyPath(path);

            var fileName = GetFileName(file.FileName);
            var filePath = string.IsNullOrEmpty(path) ?
                Path.Combine(Location, fileName) :
                Path.Combine(Location, path + _separator + fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                return new AssetItem
                {
                    Title = fileName,
                    Path = TrimFilePath(filePath),
                    Url = GetUrl(filePath, root)
                };
            }
        }

        public async Task<AssetItem> UploadBase64Image(string baseImg, string root, string path = "")
        {
            path = path.Replace("/", _separator);
            var fileName = "";

            VerifyPath(path);

            Random rnd = new Random();

            if (baseImg.StartsWith("data:image/png;base64,"))
            {
                fileName = string.Format("{0}.png", rnd.Next(1000, 9999));
                baseImg = baseImg.Replace("data:image/png;base64,", "");
            }
            if (baseImg.StartsWith("data:image/jpeg;base64,"))
            {
                fileName = string.Format("{0}.jpeg", rnd.Next(1000, 9999));
                baseImg = baseImg.Replace("data:image/jpeg;base64,", "");
            }
            if (baseImg.StartsWith("data:image/gif;base64,"))
            {
                fileName = string.Format("{0}.gif", rnd.Next(1000, 9999));
                baseImg = baseImg.Replace("data:image/gif;base64,", "");
            }

            var filePath = string.IsNullOrEmpty(path) ?
                Path.Combine(Location, fileName) :
                Path.Combine(Location, path + _separator + fileName);

            byte[] bytes = Convert.FromBase64String(baseImg);

            await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(baseImg));

            return new AssetItem
            {
                Title = fileName,
                Path = filePath,
                Url = GetUrl(filePath, root)
            };
        }

        public async Task<AssetItem> UploadFromWeb(Uri requestUri, string root, string path = "")
        {
            path = path.Replace("/", _separator);

            VerifyPath(path);

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
                        return new AssetItem
                        {
                            Title = fileName,
                            Path = filePath,
                            Url = GetUrl(filePath, root)
                        };
                    }
                }
            }
        }

        public async Task<IEnumerable<AssetItem>> Find(Func<AssetItem, bool> predicate, Pager pager, string path = "", bool sanitize = false)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var files = GetAssets(path);
            var items = MapFilesToAssets(files);

            if (predicate != null)
                items = items.Where(predicate).ToList();

            pager.Configure(items.Count);

            var page = items.Skip(skip).Take(pager.ItemsPerPage).ToList();

            if (sanitize)
            {
                foreach (var p in page)
                {
                    p.Path = p.Path.Replace(Location, "");
                }
            }

            return await Task.FromResult(page);
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

        public void DeleteAuthor(string name)
        {
            var dir = Path.GetFullPath(Path.Combine(Location, @"..\"));
            dir = Path.Combine(dir, name);

            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
        }

        public void DeleteFile(string path)
        {
            path = path.SanitizeFileName();
            path = path.Replace("/", _separator);
            path = path.Replace($"{_uploadFolder}{_separator}{_blogSlug}{_separator}", "");
            File.Delete(GetFullPath(path));
        }

        public async Task Reset()
        {
            try
            {
                var dirs = Directory.GetDirectories(Location);
                foreach (var dir in dirs)
                {
                    if (!dir.EndsWith("_init"))
                    {
                        Directory.Delete(dir, true);
                    }
                }
                var srcLoc = Path.Combine(Location, "_init");

                foreach (string dirPath in Directory.GetDirectories(srcLoc, "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(srcLoc, Location));

                foreach (string newPath in Directory.GetFiles(srcLoc, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(srcLoc, Location), true);

                await Task.CompletedTask;
            }
            catch { }
        }

        void VerifyPath(string path)
        {
            path = path.SanitizePath();

            if (!string.IsNullOrEmpty(path))
            {
                var dir = Path.Combine(Location, path);

                if (!Directory.Exists(dir))
                {
                    CreateFolder(dir);
                }
            }
        }

        string TrimFilePath(string path)
        {
            var p = path.Replace(AppSettings.WebRootPath, "");
            if (p.StartsWith("\\")) p = p.Substring(1);
            return p;
        }

        string GetFullPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return Location;
            else
                return Path.Combine(Location, path.Replace("/", _separator));
        }

        string GetFileName(string fileName)
        {
            // some browsers pass uploaded file name as short file name 
            // and others include the path; remove path part if needed
            if (fileName.Contains(_separator))
            {
                fileName = fileName.Substring(fileName.LastIndexOf(_separator));
                fileName = fileName.Replace(_separator, "");
            }
            // when drag-and-drop or copy image to TinyMce editor
            // it uses "mceclip0" as file name; randomize it for multiple uploads
            if (fileName.StartsWith("mceclip0"))
            {
                Random rnd = new Random();
                fileName = fileName.Replace("mceclip0", rnd.Next(100000, 999999).ToString());
            }
            return fileName.SanitizeFileName();
        }

        string GetUrl(string path, string root)
        {
            var url = path.ReplaceIgnoreCase(Location, "").Replace(_separator, "/");
            return string.Concat(_uploadFolder, "/", _blogSlug, url);
        }

        string GetAppRoot()
        {
            // normal application run
            if(!string.IsNullOrEmpty(AppSettings.ContentRootPath))
                return AppSettings.ContentRootPath;

            // unit tests of seed data load
            Assembly assembly;
            var assemblyName = "Core.Tests";
            try
            {
                assembly = Assembly.Load(new AssemblyName(assemblyName));
            }
            catch
            {
                assemblyName = "App";
                assembly = Assembly.Load(new AssemblyName(assemblyName));
            }
            
            var uri = new UriBuilder(assembly.CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var root = Path.GetDirectoryName(path);
            root = root.Substring(0, root.IndexOf(assemblyName));

            if (root.EndsWith($"tests{_separator}"))
            {
                root = root.Replace($"tests{_separator}", $"src{_separator}");
            }

            return Path.Combine(root, "App");
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
            if (title.Contains("file.axd?file="))
            {
                title = title.Substring(title.IndexOf("file.axd?file=") + 14);
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

            title = title.Replace(" ", "-");

            return title.Replace("/", "").SanitizeFileName();
        }

        List<AssetItem> MapFilesToAssets(IList<string> assets)
        {
            var items = new List<AssetItem>();

            if (assets != null && assets.Any())
            {
                foreach (var asset in assets)
                {
                    // Azure puts web sites under "wwwroot" folder
                    var path = asset.Replace($"wwwroot{_separator}wwwroot", "wwwroot", StringComparison.OrdinalIgnoreCase);

                    items.Add(new AssetItem
                    {
                        Path = asset,
                        Url = pathToUrl(path),
                        Title = pathToTitle(path),
                        Image = pathToImage(path)
                    });
                }
            }
            return items;
        }

        string pathToUrl(string path)
        {
            return path.Substring(path.IndexOf("wwwroot") + 8)
                .Replace(_separator, "/");
        }

        string pathToTitle(string path)
        {
            var title = path;

            if(title.LastIndexOf(_separator) > 0)
                title = title.Substring(title.LastIndexOf(_separator));       

            if(title.IndexOf('.') > 0)
                title = title.Substring(1, title.LastIndexOf('.') - 1);

            return title;
        }

        string pathToImage(string path)
        {
            if(path.IsImagePath())
                return pathToUrl(path);

            var ext = "blank.png";

            if (path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                ext = "xml.png";

            if (path.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                ext = "zip.png";

            if (path.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                ext = "txt.png";

            if (path.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                ext = "pdf.png";

            if (path.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                ext = "doc.png";

            if (path.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                ext = "xls.png";

            // video/audio formats fro HTML5 tags

            if (path.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) 
                || path.EndsWith(".webm", StringComparison.OrdinalIgnoreCase)
                || path.EndsWith(".ogv", StringComparison.OrdinalIgnoreCase))
                ext = "video.png";

            if (path.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase)
                || path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase)
                || path.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase))
                ext = "audio.png";

            return $"lib/img/doctypes/{ext}";
        }
    }
}