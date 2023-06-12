using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Blogifier.Storages;

public class StorageLocalProvider
{
  private readonly ILogger _logger;
  private readonly string _pathLocalRoot;

  public StorageLocalProvider(ILogger<StorageLocalProvider> logger, IHostEnvironment hostEnvironment)
  {
    _logger = logger;
    _pathLocalRoot = Path.Combine(hostEnvironment.ContentRootPath, BlogifierConstant.StorageLocalRoot);
  }

  public string? GetVirtualPath(string path)
  {
    var storagePath = Path.Combine(_pathLocalRoot, path);
    if (!File.Exists(storagePath)) return null;
    return $"{BlogifierConstant.StorageLocalPhysicalRoot}/{path}";
  }

  public async Task<string> WriteAsync(string path, Stream stream)
  {
    var storagePath = Path.Combine(_pathLocalRoot, path);
    var directoryPath = Path.GetDirectoryName(storagePath)!;
    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
    using var fileStream = new FileStream(storagePath, FileMode.CreateNew);
    await stream.CopyToAsync(fileStream);
    return $"{BlogifierConstant.StorageLocalPhysicalRoot}/{path}";
  }

  public bool Exists(string path)
  {
    var storagePath = Path.Combine(_pathLocalRoot, path);
    _logger.LogInformation("File exists: {storagePath}", storagePath);
    return File.Exists(storagePath);
  }

}
