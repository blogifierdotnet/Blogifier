using Minio;
using System;

namespace Blogifier.Core.Minio;

public class MinioOptions
{
  public string Endpoint { get; set; } = default!;
  public int Port { get; set; } = 9000;
  public string AccessKey { get; set; } = default!;
  public string SecretKey { get; set; } = default!;
  public string Region { get; set; } = default!;
  public string SessionToken { get; set; } = default!;
  public string BucketName { get; set; } = default!;
  internal Action<MinioClient>? Configure { get; private set; }

  public void ConfigureClient(Action<MinioClient> configure)
  {
    Configure = configure;
  }
}
