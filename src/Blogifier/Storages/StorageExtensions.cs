using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Blogifier.Storages;

public static class StorageExtensions
{
  public static WebApplication UseStorageStaticFiles(this WebApplication app)
  {
    var fileProviderRoot = Path.Combine(app.Environment.ContentRootPath, BlogifierConstant.StorageLocalRoot);
    if (!Directory.Exists(fileProviderRoot)) Directory.CreateDirectory(fileProviderRoot);
    app.UseStaticFiles(new StaticFileOptions
    {
      FileProvider = new PhysicalFileProvider(fileProviderRoot),
      RequestPath = BlogifierConstant.StorageLocalPhysicalRoot,
    });
    return app;
  }
}
