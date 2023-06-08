using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Blogifier.Providers;

public class StorageProvider
{
  private readonly ILogger _logger;
  private readonly AppDbContext _appDbContext;
  private readonly MinioProvider _minioProvider;
  private readonly string _publicStorageRoot;
  private readonly string _slash = Path.DirectorySeparatorChar.ToString();
  private readonly IConfiguration _configuration;

  public StorageProvider(ILogger<StorageProvider> logger, AppDbContext appDbContext, MinioProvider minioProvider, IConfiguration configuration)
  {
    _logger = logger;
    _appDbContext = appDbContext;
    _minioProvider = minioProvider;
    _configuration = configuration;
    _publicStorageRoot = Path.Combine(ContentRoot, "App_Data", "public");
  }

  /// <summary>
  /// 根据存储路径获取文件
  /// </summary>
  public async Task<Storage?> GetAsync(string storagePath, Func<Stream, CancellationToken, Task> callback)
  {
    _logger.LogInformation("Storage Url:{storagePath}", storagePath);
    var storage = await _appDbContext.Storages.FirstOrDefaultAsync(m => m.Path == storagePath);
    if (storage == null) return null;
    var objectStat = await _minioProvider.GetObjectAsync(storagePath, callback);
    if (objectStat == null) return null;
    storage.ContentType = objectStat.ContentType;
    storage.Length = objectStat.Size;
    return storage;
  }

  public bool FileExistsAsync(string path)
  {
    var absolutePath = Path.Combine(ContentRoot, path);
    _logger.LogInformation("File exists: {absolutePath}", absolutePath);
    return File.Exists(absolutePath);
  }

  public async Task<IList<string>> GetThemesAsync()
  {
    var themes = new List<string>();
    var themesDirectory = Path.Combine(ContentRoot, $"Views{_slash}Themes");
    foreach (string dir in Directory.GetDirectories(themesDirectory))
    {
      themes.Add(Path.GetFileName(dir));
    }
    return await Task.FromResult(themes);
  }

  public async Task<ThemeSettings?> GetThemeSettingsAsync(string theme)
  {
    var settings = new ThemeSettings();
    var fileName = Path.Combine(ContentRoot, $"wwwroot{_slash}themes{_slash}{theme.ToLower()}{_slash}settings.json");
    if (File.Exists(fileName))
    {
      try
      {
        string jsonString = File.ReadAllText(fileName);
        settings = JsonSerializer.Deserialize<ThemeSettings>(jsonString);
      }
      catch (Exception ex)
      {
        _logger.LogError("Error reading theme settings: {Message}", ex.Message);
        return null;
      }
    }

    return await Task.FromResult(settings);
  }

  public async Task<bool> SaveThemeSettingsAsync(string theme, ThemeSettings settings)
  {
    var fileName = Path.Combine(ContentRoot, $"wwwroot{_slash}themes{_slash}{theme.ToLower()}{_slash}settings.json");
    try
    {
      if (File.Exists(fileName))
        File.Delete(fileName);

      var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };

      string jsonString = JsonSerializer.Serialize(settings, options);

      using FileStream createStream = File.Create(fileName);
      await JsonSerializer.SerializeAsync(createStream, settings, options);
    }
    catch (Exception ex)
    {
      _logger.LogError("Error writing theme settings: {Message}", ex.Message);
      return false;
    }
    return true;
  }

  public async Task<bool> UploadFormFileAsync(IFormFile file, string path = "")
  {
    path = path.Replace("/", _slash);
    VerifyPath(_publicStorageRoot, path);

    var fileName = GetFileName(file.FileName);

    if (InvalidFileName(fileName))
    {
      _logger.LogError("Invalid file name: {fileName}", fileName);
      return false;
    }

    var filePath = string.IsNullOrEmpty(path) ?
         Path.Combine(_publicStorageRoot, fileName) :
         Path.Combine(_publicStorageRoot, path + _slash + fileName);

    _logger.LogInformation("Storage root: {_publicStorageRoot}", _publicStorageRoot);
    _logger.LogInformation("Uploading file: {filePath}", filePath);
    try
    {
      using var fileStream = new FileStream(filePath, FileMode.Create);
      await file.CopyToAsync(fileStream);
      _logger.LogInformation("Uploaded file: {filePath}", filePath);
    }
    catch (Exception ex)
    {
      _logger.LogInformation("Error uploading file: {Message}", ex.Message);
    }

    return true;
  }

  public async Task<string> UploadFromWeb(Uri requestUri, string root, string path = "")
  {
    path = path.Replace("/", _slash);
    VerifyPath(_publicStorageRoot, path);

    var fileName = TitleFromUri(requestUri);
    var filePath = string.IsNullOrEmpty(path) ?
         Path.Combine(_publicStorageRoot, fileName) :
         Path.Combine(_publicStorageRoot, path + _slash + fileName);

    var client = new HttpClient();
    var response = await client.GetAsync(requestUri);
    using var fs = new FileStream(filePath, FileMode.CreateNew);
    await response.Content.CopyToAsync(fs);
    return $"![{fileName}]({root}{PathToUrl(filePath)})";
  }

  public async Task<string> UploadBase64Image(string baseImg, string root, string path = "")
  {
    path = path.Replace("/", _slash);
    var fileName = "";

    VerifyPath(_publicStorageRoot, path);
    string imgSrc = GetImgSrcValue(baseImg);

    var rnd = new Random();
    if (imgSrc.StartsWith("data:image/png;base64,"))
    {
      fileName = string.Format("{0}.png", rnd.Next(1000, 9999));
      imgSrc = imgSrc.Replace("data:image/png;base64,", "");
    }
    if (imgSrc.StartsWith("data:image/jpeg;base64,"))
    {
      fileName = string.Format("{0}.jpeg", rnd.Next(1000, 9999));
      imgSrc = imgSrc.Replace("data:image/jpeg;base64,", "");
    }
    if (imgSrc.StartsWith("data:image/gif;base64,"))
    {
      fileName = string.Format("{0}.gif", rnd.Next(1000, 9999));
      imgSrc = imgSrc.Replace("data:image/gif;base64,", "");
    }

    var filePath = string.IsNullOrEmpty(path) ?
         Path.Combine(_publicStorageRoot, fileName) :
         Path.Combine(_publicStorageRoot, path + _slash + fileName);

    await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(imgSrc));

    return $"![{fileName}]({root}{PathToUrl(filePath)})";
  }

  #region Private members

  private string ContentRoot
  {
    get
    {
      string path = Directory.GetCurrentDirectory();
      string testsDirectory = $"tests{_slash}Blogifier.Tests";
      string appDirectory = $"src{_slash}Blogifier";

      _logger.LogInformation("Current directory path: {path}", path);

      // development unit test run
      if (path.LastIndexOf(testsDirectory) > 0)
      {
        path = path[..path.LastIndexOf(testsDirectory)];
        var rootPath = $"{path}src{_slash}Blogifier";
        _logger.LogInformation("Unit test path: {rootPath}", rootPath);
        return rootPath;
      }

      // this needed to make sure we have correct data directory
      // when running in debug mode in Visual Studio
      // so instead of debug (src/Blogifier/bin/Debug..)
      // will be used src/Blogifier/wwwroot/data
      // !! this can mess up installs that have "src/Blogifier" in the path !!
      if (path.LastIndexOf(appDirectory) > 0)
      {
        path = path[..path.LastIndexOf(appDirectory)];
        var rootPath = $"{path}src{_slash}Blogifier";
        _logger.LogInformation("Development debug path: {rootPath}", rootPath);
        return rootPath;
      }
      _logger.LogInformation($"Final path: {path}");
      return path;
    }
  }
  string GetFileName(string fileName)
  {
    // some browsers pass uploaded file name as short file name
    // and others include the path; remove path part if needed
    if (fileName.Contains(_slash))
    {
      fileName = fileName.Substring(fileName.LastIndexOf(_slash));
      fileName = fileName.Replace(_slash, "");
    }
    // when drag-and-drop or copy image to TinyMce editor
    // it uses "mceclip0" as file name; randomize it for multiple uploads
    if (fileName.StartsWith("mceclip0"))
    {
      var rnd = new Random();
      fileName = fileName.Replace("mceclip0", rnd.Next(100000, 999999).ToString());
    }
    return fileName.SanitizePath();
  }
  static void VerifyPath(string basePath, string path)
  {
    path = path.SanitizePath();

    if (!string.IsNullOrEmpty(path))
    {
      var dir = Path.Combine(basePath, path);

      if (!Directory.Exists(dir))
      {
        Directory.CreateDirectory(dir);
      }
    }
  }
  static string TitleFromUri(Uri uri)
  {
    var title = uri.ToString().ToLower();
    title = title.Replace("%2f", "/");

    if (title.EndsWith(".axdx"))
    {
      title = title.Replace(".axdx", "");
    }
    if (title.Contains("image.axd?picture="))
    {
      title = title[(title.IndexOf("image.axd?picture=") + 18)..];
    }
    if (title.Contains("file.axd?file="))
    {
      title = title[(title.IndexOf("file.axd?file=") + 14)..];
    }
    if (title.Contains("encrypted-tbn") || title.Contains("base64,"))
    {
      var rnd = new Random();
      title = string.Format("{0}.png", rnd.Next(1000, 9999));
    }

    if (title.Contains("/"))
    {
      title = title[title.LastIndexOf("/")..];
    }

    title = title.Replace(" ", "-");

    return title.Replace("/", "").SanitizeFileName();
  }
  string PathToUrl(string path)
  {
    string url = path.ReplaceIgnoreCase(_publicStorageRoot, "").Replace(_slash, "/");
    return $"data/{url}";
  }
  static string GetImgSrcValue(string imgTag)
  {
    if (!(imgTag.Contains("data:image") && imgTag.Contains("src=")))
      return imgTag;

    int start = imgTag.IndexOf("src=");
    int srcStart = imgTag.IndexOf("\"", start) + 1;

    if (srcStart < 2)
      return imgTag;

    int srcEnd = imgTag.IndexOf("\"", srcStart);

    if (srcEnd < 1 || srcEnd <= srcStart)
      return imgTag;

    return imgTag.Substring(srcStart, srcEnd - srcStart);
  }
  bool InvalidFileName(string fileName)
  {
    List<string> fileExtensions = new List<string>() { "png", "gif", "jpeg", "jpg", "zip", "7z", "pdf", "doc", "docx", "xls", "xlsx", "mp3", "mp4", "avi" };
    var configFileExtensions = _configuration.GetSection("Blogifier").GetValue<string>("FileExtensions");

    if (!string.IsNullOrEmpty(configFileExtensions))
    {
      fileExtensions = new List<string>(configFileExtensions.Split(','));
    }

    foreach (string ext in fileExtensions)
    {
      if (fileName.EndsWith(ext))
      {
        return false;
      }
    }

    return true;
  }

  #endregion
}
