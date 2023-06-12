using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Helper;
using Blogifier.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blogifier.Storages;

public class StorageProvider
{
  private readonly ILogger _logger;
  private readonly IHttpClientFactory _httpClientFactory;
  private readonly BlogifierConfigure _blogifierConfigure;
  private readonly AppDbContext _dbContext;
  private readonly StorageLocalProvider _storageProvider;
  private readonly MinioProvider _minioProvider;

  public StorageProvider(
    ILogger<StorageProvider> logger,
    IHttpClientFactory httpClientFactory,
    IOptions<BlogifierConfigure> blogifierConfigure,
    AppDbContext dbContext,
    StorageLocalProvider storageProvider,
    MinioProvider minioProvider)
  {
    _logger = logger;
    _httpClientFactory = httpClientFactory;
    _blogifierConfigure = blogifierConfigure.Value;
    _dbContext = dbContext;
    _minioProvider = minioProvider;
    _storageProvider = storageProvider;
  }

  public async Task<Storage> UploadAsync(DateTime createdAt, int userid, Uri baseAddress, string url, string? fileName = null)
  {
    using var client = _httpClientFactory.CreateClient();
    client.BaseAddress = baseAddress;
    using var response = await client.GetAsync(url);
    if (!response.IsSuccessStatusCode) throw new HttpRequestException("url not content");
    using var stream = await response.Content.ReadAsStreamAsync();

    var folder = $"{userid}/{createdAt.Year}{createdAt.Month}";
    string? path = null;
    string? virtualPath;
    if (fileName != null)
    {
      path = $"{folder}/{fileName}";
      virtualPath = await GetVirtualPathAsync(path);
      if (virtualPath != null)
      {
        return new Storage
        {
          CreatedAt = createdAt,
          UserId = userid,
          Slug = virtualPath,
          Name = fileName,
          Path = path,
          Length = stream.Length,
          Type = StorageType.Local,
        };
      }
    }

    if (fileName == null)
    {
      fileName = response.Content.Headers.ContentDisposition?.FileNameStar;
      if (!string.IsNullOrEmpty(fileName))
      {
        path = $"{folder}/{fileName}";
      }
      else
      {
        fileName = GetFileNameByUrl(url);
        path = $"{folder}/{fileName}";
      }
      virtualPath = await GetVirtualPathAsync(path);
      if (virtualPath != null)
      {
        return new Storage
        {
          CreatedAt = createdAt,
          UserId = userid,
          Slug = virtualPath,
          Name = fileName,
          Path = path,
          Length = stream.Length,
          Type = StorageType.Local,
        };
      }
    }

    virtualPath = await _storageProvider.WriteAsync(path!, stream);
    return new Storage
    {
      CreatedAt = createdAt,
      UserId = userid,
      Slug = virtualPath,
      Name = fileName,
      Path = path!,
      Length = stream.Length,
      Type = StorageType.Local,
    }; ;
  }
  public async Task<string> UploadAsync(string path, Stream stream)
  {
    var virtualPath = await GetVirtualPathAsync(path);
    if (virtualPath != null) return virtualPath;
    virtualPath = await _storageProvider.WriteAsync(path, stream);
    return virtualPath;
  }
  public async Task<string?> UploadAsync(DateTime createdAt, int userid, IFormFile file)
  {
    var fileName = GetFileName(file.FileName);

    if (InvalidFileName(fileName))
    {
      _logger.LogError("Invalid file name: {fileName}", fileName);
      return null;
    }

    var folder = $"{userid}/{createdAt.Year}{createdAt.Month}";
    var path = Path.Combine(folder, fileName);
    var stream = file.OpenReadStream();
    var virtualPath = await UploadAsync(path, stream);
    return virtualPath;
  }
  public async Task<string> UploadsFoHtmlAsync(DateTime createdAt, int userid, Uri baseAddress, string content)
  {
    var uploadeImageContent = await UploadImagesFoHtml(createdAt, userid, baseAddress, content);

    var uploadeFileContent = await UploadFilesFoHtml(createdAt, userid, baseAddress, uploadeImageContent);

    return uploadeFileContent;
  }
  public async Task<string> UploadImagesFoHtml(DateTime createdAt, int userid, Uri baseAddress, string content)
  {
    var matches = StringHelper.MatchesImgTags(content);
    if (matches.Any())
    {
      var contentBuilder = new StringBuilder(content);
      foreach (Match match in matches.Cast<Match>())
      {
        var tag = match.Value;
        var matchUrl = StringHelper.MatchImgSrc(tag);
        var urlString = matchUrl.Groups[1].Value;
        var storage = await UploadAsync(createdAt, userid, baseAddress, urlString);
        var uploadTag = $"![{storage.Name}]({storage.Slug})";
        contentBuilder.Replace(tag, uploadTag);
      }
      content = contentBuilder.ToString();
    }
    return content;
  }

  public async Task<string> UploadFilesFoHtml(DateTime createdAt, int userid, Uri baseAddress, string content)
  {
    var matches = StringHelper.MatchesFile(content);
    if (matches.Any())
    {
      var contentBuilder = new StringBuilder(content);
      foreach (Match match in matches.Cast<Match>())
      {
        var tag = match.Value;
        var urlString = XElement.Parse(tag).Attribute("href")!.Value;
        if (InvalidFileName(urlString))
        {
          var storage = await UploadAsync(createdAt, userid, baseAddress, urlString);
          var uploadTag = $"![{storage.Name}]({storage.Slug})";
          contentBuilder.Replace(tag, uploadTag);
        }
      }
      content = contentBuilder.ToString();
    }
    return content;
  }

  public Task<string?> GetVirtualPathAsync(string path)
  {
    var virtualPath = _storageProvider.GetVirtualPath(path);
    return Task.FromResult(virtualPath);
  }

  /// <summary>
  /// 根据存储路径获取文件
  /// </summary>
  public async Task<Storage?> GetAsync(string storagePath, Func<Stream, CancellationToken, Task> callback)
  {
    _logger.LogInformation("Storage Url:{storagePath}", storagePath);
    var storage = await _dbContext.Storages.FirstOrDefaultAsync(m => m.Path == storagePath);
    if (storage == null) return null;
    var objectStat = await _minioProvider.GetObjectAsync(storagePath, callback);
    if (objectStat == null) return null;
    storage.ContentType = objectStat.ContentType;
    storage.Length = objectStat.Size;
    return storage;
  }

  public Task<bool> ExistsAsync(string path)
  {
    var existsing = _storageProvider.Exists(path);
    return Task.FromResult(existsing);
  }

  private bool InvalidFileName(string fileName)
  {
    var fileExtensions = BlogifierConstant.FileExtensions;
    var configFileExtensions = _blogifierConfigure.FileExtensions;
    if (!string.IsNullOrEmpty(configFileExtensions))
      fileExtensions = new List<string>(configFileExtensions.Split(','));
    return fileExtensions.Any(fileName.EndsWith);
  }

  private static string GetFileName(string fileName)
  {
    // some browsers pass uploaded file name as short file name
    // and others include the path; remove path part if needed
    const string slash = "/";
    if (fileName.Contains(slash))
    {
      fileName = fileName.Substring(fileName.LastIndexOf(slash));
      fileName = fileName.Replace(slash, "");
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

  private static string GetFileNameByUrl(string uri)
  {
    var title = uri.ToLower();
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
    if (title.Contains('/'))
    {
      title = title[title.LastIndexOf("/")..];
    }
    title = title.Replace(" ", "-");
    return title.Replace("/", "").SanitizeFileName();
  }


  #region no use code

  //public async Task<string> UploadBase64Image(string baseImg, string root, string path = "")
  //{
  //  path = path.Replace("/", _slash);
  //  var fileName = "";

  //  VerifyPath(_pathLocalRoot, path);
  //  string imgSrc = GetImgSrcValue(baseImg);

  //  var rnd = new Random();
  //  if (imgSrc.StartsWith("data:image/png;base64,"))
  //  {
  //    fileName = string.Format("{0}.png", rnd.Next(1000, 9999));
  //    imgSrc = imgSrc.Replace("data:image/png;base64,", "");
  //  }
  //  if (imgSrc.StartsWith("data:image/jpeg;base64,"))
  //  {
  //    fileName = string.Format("{0}.jpeg", rnd.Next(1000, 9999));
  //    imgSrc = imgSrc.Replace("data:image/jpeg;base64,", "");
  //  }
  //  if (imgSrc.StartsWith("data:image/gif;base64,"))
  //  {
  //    fileName = string.Format("{0}.gif", rnd.Next(1000, 9999));
  //    imgSrc = imgSrc.Replace("data:image/gif;base64,", "");
  //  }

  //  var filePath = string.IsNullOrEmpty(path) ?
  //       Path.Combine(_pathLocalRoot, fileName) :
  //       Path.Combine(_pathLocalRoot, path + _slash + fileName);

  //  await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(imgSrc));

  //  return $"![{fileName}]({root}{PathToUrl(filePath)})";
  //}

  //static void VerifyPath(string basePath, string path)
  //{
  //  path = path.SanitizePath();

  //  if (!string.IsNullOrEmpty(path))
  //  {
  //    var dir = Path.Combine(basePath, path);

  //    if (!Directory.Exists(dir))
  //    {
  //      Directory.CreateDirectory(dir);
  //    }
  //  }
  //}
  //string PathToUrl(string path)
  //{
  //  string url = path.ReplaceIgnoreCase(_pathLocalRoot, "").Replace(_slash, "/");
  //  return $"data/{url}";
  //}
  //static string GetImgSrcValue(string imgTag)
  //{
  //  if (!(imgTag.Contains("data:image") && imgTag.Contains("src=")))
  //    return imgTag;

  //  int start = imgTag.IndexOf("src=");
  //  int srcStart = imgTag.IndexOf("\"", start) + 1;

  //  if (srcStart < 2)
  //    return imgTag;

  //  int srcEnd = imgTag.IndexOf("\"", srcStart);

  //  if (srcEnd < 1 || srcEnd <= srcStart)
  //    return imgTag;

  //  return imgTag.Substring(srcStart, srcEnd - srcStart);
  //}
  #endregion

  #region theme no use code

  //public async Task<IList<string>> GetThemesAsync()
  //{
  //  var themes = new List<string>();
  //  var themesDirectory = Path.Combine(ContentRoot, $"Views{_slash}Themes");
  //  foreach (string dir in Directory.GetDirectories(themesDirectory))
  //  {
  //    themes.Add(Path.GetFileName(dir));
  //  }
  //  return await Task.FromResult(themes);
  //}

  //public async Task<ThemeSettings?> GetThemeSettingsAsync(string theme)
  //{
  //  var settings = new ThemeSettings();
  //  var fileName = Path.Combine(ContentRoot, $"wwwroot{_slash}themes{_slash}{theme.ToLower()}{_slash}settings.json");
  //  if (File.Exists(fileName))
  //  {
  //    try
  //    {
  //      string jsonString = File.ReadAllText(fileName);
  //      settings = JsonSerializer.Deserialize<ThemeSettings>(jsonString);
  //    }
  //    catch (Exception ex)
  //    {
  //      _logger.LogError("Error reading theme settings: {Message}", ex.Message);
  //      return null;
  //    }
  //  }

  //  return await Task.FromResult(settings);
  //}

  //public async Task<bool> SaveThemeSettingsAsync(string theme, ThemeSettings settings)
  //{
  //  var fileName = Path.Combine(ContentRoot, $"wwwroot{_slash}themes{_slash}{theme.ToLower()}{_slash}settings.json");
  //  try
  //  {
  //    if (File.Exists(fileName))
  //      File.Delete(fileName);

  //    var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };

  //    string jsonString = JsonSerializer.Serialize(settings, options);

  //    using FileStream createStream = File.Create(fileName);
  //    await JsonSerializer.SerializeAsync(createStream, settings, options);
  //  }
  //  catch (Exception ex)
  //  {
  //    _logger.LogError("Error writing theme settings: {Message}", ex.Message);
  //    return false;
  //  }
  //  return true;
  //}
  #endregion
}
