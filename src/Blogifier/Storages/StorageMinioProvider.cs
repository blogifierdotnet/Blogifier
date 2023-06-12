using AutoMapper;
using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Blogifier.Storages;

public class StorageMinioProvider : AppProvider<Storage, int>, IStorageProvider, IDisposable
{
  private readonly ILogger _logger;
  private readonly IMapper _mapper;
  private readonly string _bucketName = default!;
  private readonly MinioClient _minioClient = default!;

  public StorageMinioProvider(
    ILogger<StorageMinioProvider> logger,
    IMapper mapper,
    AppDbContext dbContext,
    IHttpClientFactory httpClientFactory,
    IConfigurationSection section) : base(dbContext)
  {
    _logger = logger;
    _mapper = mapper;
    _bucketName = section.GetValue<string>("BucketName")!;
    _minioClient = new MinioClient()
     .WithEndpoint(section.GetValue<string>("Endpoint")!, section.GetValue<int>("Port"))
     .WithRegion(section.GetValue<string>("Region")!)
     .WithCredentials(section.GetValue<string>("AccessKey")!, section.GetValue<string>("SecretKey")!)
     .WithHttpClient(httpClientFactory.CreateClient())
     .Build();
  }

  public Task<bool> ExistsAsync(string slug)
  {
    throw new NotImplementedException();
  }

  public Task<StorageDto> AddAsync(DateTime uploadAt, int userid, string path, string fileName, Stream stream, string contentType)
  {
    throw new NotImplementedException();
  }

  public async Task<StorageDto?> GetAsync(string slug, Func<Stream, CancellationToken, Task> callback)
  {
    _logger.LogInformation("Storage slug:{slug}", slug);
    var storage = await _dbContext.Storages.FirstOrDefaultAsync(m => m.Slug == slug);
    if (storage == null) return null;
    var objectStat = await GetObjectAsync(slug, callback);
    if (objectStat == null) return null;
    storage.ContentType = objectStat.ContentType;
    storage.Length = objectStat.Size;
    return _mapper.Map<StorageDto>(storage);
  }

  public async Task<ObjectStat> GetObjectAsync(string objectName, Func<Stream, CancellationToken, Task> callback)
  {
    var args = new GetObjectArgs().WithBucket(_bucketName).WithObject(objectName).WithCallbackStream(callback);
    return await _minioClient.GetObjectAsync(args).ConfigureAwait(false);
  }

  public Task<StorageDto?> GetCheckStoragAsync(string path)
  {
    throw new NotImplementedException();
  }

  private bool _disposedValue;

  ~StorageMinioProvider() => Dispose(false);

  // Public implementation of Dispose pattern callable by consumers.
  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  // Protected implementation of Dispose pattern.
  protected virtual void Dispose(bool disposing)
  {
    if (!_disposedValue)
    {
      if (disposing)
      {
        _minioClient.Dispose();
      }
      _disposedValue = true;
    }
  }


}
