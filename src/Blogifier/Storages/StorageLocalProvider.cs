using AutoMapper;
using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blogifier.Storages;

public class StorageLocalProvider : AppProvider<Storage, int>, IStorageProvider
{
  private readonly ILogger _logger;
  private readonly IMapper _mapper;
  private readonly string _pathLocalRoot;

  public StorageLocalProvider(
    ILogger<StorageLocalProvider> logger,
    IMapper mapper,
    AppDbContext dbContext,
    IHostEnvironment hostEnvironment) : base(dbContext)
  {
    _logger = logger;
    _mapper = mapper;
    _pathLocalRoot = Path.Combine(hostEnvironment.ContentRootPath, BlogifierConstant.StorageLocalRoot);
  }

  public Task<bool> ExistsAsync(string slug)
  {
    throw new NotImplementedException();
  }

  public Task<StorageDto?> GetAsync(string slug, Func<Stream, CancellationToken, Task> callback)
  {
    throw new NotImplementedException();
  }

  public async Task<StorageDto?> GetCheckStoragAsync(string path)
  {
    var query = _dbContext.Storages.AsNoTracking().Where(m => m.Path == path);
    var storage = await _mapper.ProjectTo<StorageDto>(query).FirstOrDefaultAsync();
    var existsing = Exists(path);
    if (storage == null)
    {
      if (existsing)
      {
        Delete(path);
      }
    }
    else
    {
      if (!existsing)
      {
        await DeleteAsync(storage.Id);
        return null;
      }
    }
    return storage;
  }

  public async Task<StorageDto> AddAsync(DateTime uploadAt, int userid, string path, string fileName, Stream stream, string contentType)
  {
    var storage = new Storage
    {
      UploadAt = uploadAt,
      UserId = userid,
      Name = fileName,
      Path = path,
      Length = stream.Length,
      ContentType = contentType,
      Slug = await WriteAsync(path, stream),
      Type = StorageType.Local
    };
    await AddAsync(storage);
    return _mapper.Map<StorageDto>(storage);
  }

  public async Task<StorageDto> AddAsync(DateTime uploadAt, int userid, string path, string fileName, byte[] bytes, string contentType)
  {
    var storage = new Storage
    {
      UploadAt = uploadAt,
      UserId = userid,
      Name = fileName,
      Path = path,
      Length = bytes.Length,
      ContentType = contentType,
      Slug = await WriteAsync(path, bytes),
      Type = StorageType.Local
    };
    await AddAsync(storage);
    return _mapper.Map<StorageDto>(storage);
  }



  private void Delete(string path)
  {
    var storagePath = Path.Combine(_pathLocalRoot, path);
    _logger.LogInformation("file delete: {storagePath}", storagePath);
    File.Delete(storagePath);
  }

  private bool Exists(string path)
  {
    var storagePath = Path.Combine(_pathLocalRoot, path);
    _logger.LogInformation("file exists: {storagePath}", storagePath);
    return File.Exists(storagePath);
  }

  private async Task<string> WriteAsync(string path, Stream stream)
  {
    var storagePath = Path.Combine(_pathLocalRoot, path);
    var directoryPath = Path.GetDirectoryName(storagePath)!;
    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
    using var fileStream = new FileStream(storagePath, FileMode.CreateNew);
    await stream.CopyToAsync(fileStream);
    var virtualPath = $"{BlogifierConstant.StorageLocalPhysicalRoot}/{path}";
    _logger.LogInformation("file Write: {storagePath} => {virtualPath}", storagePath, virtualPath);
    return virtualPath;
  }


  private async Task<string> WriteAsync(string path, byte[] bytes)
  {
    var storagePath = Path.Combine(_pathLocalRoot, path);
    var directoryPath = Path.GetDirectoryName(storagePath)!;
    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
    await File.WriteAllBytesAsync(storagePath, bytes);
    var virtualPath = $"{BlogifierConstant.StorageLocalPhysicalRoot}/{path}";
    _logger.LogInformation("file Write: {storagePath} => {virtualPath}", storagePath, virtualPath);
    return virtualPath;
  }


}
