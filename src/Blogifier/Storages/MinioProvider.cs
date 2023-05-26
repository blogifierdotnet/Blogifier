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

public class MinioProvider : IDisposable
{
  private readonly ILogger _logger;
  private readonly string _bucketName = default!;
  private readonly MinioClient _minioClient = default!;

  public MinioProvider(ILogger<MinioProvider> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
  {
    _logger = logger;
    var section = configuration.GetSection("Blogifier:Minio");

    if (section == null)
    {
      _logger.LogWarning("Minio 配置信息不存在未初始化 MinioProvider.");
      return;
    }
    _bucketName = section.GetValue<string>("BucketName")!;
    _minioClient = new MinioClient()
     .WithEndpoint(section.GetValue<string>("Endpoint")!, section.GetValue<int>("Port"))
     .WithRegion(section.GetValue<string>("Region")!)
     .WithCredentials(section.GetValue<string>("AccessKey")!, section.GetValue<string>("SecretKey")!)
     .WithHttpClient(httpClientFactory.CreateClient())
     .Build();
  }

  public async Task<ObjectStat> GetObjectAsync(string objectName, Func<Stream, CancellationToken, Task> callback)
  {
    var args = new GetObjectArgs().WithBucket(_bucketName).WithObject(objectName).WithCallbackStream(callback);
    return await _minioClient.GetObjectAsync(args).ConfigureAwait(false);
  }

  private bool _disposedValue;

  ~MinioProvider() => Dispose(false);

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
