using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blogifier.Storages;

public class StorageManager
{
  private readonly IHttpClientFactory _httpClientFactory;
  private readonly StorageProvider _storageProvider;

  public StorageManager(
    IHttpClientFactory httpClientFactory,
    StorageProvider storageProvider)
  {
    _httpClientFactory = httpClientFactory;
    _storageProvider = storageProvider;
  }

  public async Task<string> Upload(DateTime createdAt, int userid, string url, string? fileName = null)
  {
    var path = $"{userid}/{createdAt.Year}{createdAt.Month}";
    string virtualPath;
    if (fileName != null)
    {
      path = $"/{fileName}";
      virtualPath = await GetVirtualPathAsync(path);
      if (virtualPath != null) return virtualPath;
    }

    var client = _httpClientFactory.CreateClient();
    var response = await client.GetAsync(url);
    if (!response.IsSuccessStatusCode) throw new HttpRequestException("url not content");
    var stream = await response.Content.ReadAsStreamAsync();


    if (fileName == null)
    {
      fileName = response.Content.Headers.ContentDisposition?.FileNameStar;
      if (!string.IsNullOrEmpty(fileName))
      {
        path = $"/{fileName}";
        virtualPath = await GetVirtualPathAsync(path);
        if (virtualPath != null) return virtualPath;
      }
      else
      {
        fileName = Guid.NewGuid().ToString();
        path = $"/{fileName}";
      }
    }

    virtualPath = _storageProvider.Write(path, stream);
    return virtualPath;
  }

  public Task<string> GetVirtualPathAsync(string path)
  {
    var virtualPath = _storageProvider.GetVirtualPath(path);
    return Task.FromResult(virtualPath);
  }
}
