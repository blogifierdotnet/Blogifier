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
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Blogifier.Storages;

public class StorageMinioProvider : AppProvider<Storage, int>, IStorageProvider, IDisposable
{
  private readonly ILogger _logger;
  private readonly IMapper _mapper;
  private readonly string _bucketName;
  private readonly MinioClient _minioClient;

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

  // 判断 minio 中文件是否存在
  public Task<bool> ExistsAsync(string slug)
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

  public async Task<StorageDto?> GetCheckStoragAsync(string path)
  {
    var query = _dbContext.Storages.AsNoTracking().Where(m => m.Path == path);
    var storage = await _mapper.ProjectTo<StorageDto>(query).FirstOrDefaultAsync();
    throw new NotImplementedException();
  }

  // 想 minio中添加文件
  public async Task<StorageDto> AddAsync(DateTime uploadAt, int userid, string path, string fileName, Stream stream, string contentType)
  {
    var args = new PutObjectArgs()
      .WithBucket(_bucketName)
      .WithObject(path)
      .WithStreamData(stream)
      .WithContentType(contentType);
    var result = await _minioClient.PutObjectAsync(args).ConfigureAwait(false);
    var storage = new Storage
    {
      UploadAt = uploadAt,
      UserId = userid,
      Name = fileName,
      Path = path,
      Length = result.Size,
      ContentType = contentType,
      Slug = result.ObjectName,
      Type = StorageType.Minio
    };
    await AddAsync(storage);
    return _mapper.Map<StorageDto>(storage);
  }

  public Task<StorageDto> AddAsync(DateTime uploadAt, int userid, string path, string fileName, byte[] bytes, string contentType)
  {
    using var stream = new MemoryStream(bytes);
    return AddAsync(uploadAt, userid, path, fileName, stream, contentType);
  }

  private async Task<ObjectStat> GetObjectAsync(string objectName, Func<Stream, CancellationToken, Task> callback)
  {
    var args = new GetObjectArgs().WithBucket(_bucketName).WithObject(objectName).WithCallbackStream(callback);
    return await _minioClient.GetObjectAsync(args).ConfigureAwait(false);
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
