using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Blogifier.Storages;

public static class StorageExtensions
{
  public static WebApplication UseStorageStaticFiles(this WebApplication app)
  {
    var fileProviderRoot = Path.Combine(app.Environment.ContentRootPath, "App_Data/public");
    if (!Directory.Exists(fileProviderRoot)) Directory.CreateDirectory(fileProviderRoot);
    app.UseStaticFiles(new StaticFileOptions
    {
      FileProvider = new PhysicalFileProvider(fileProviderRoot),
      RequestPath = "/data"
    });
    return app;
  }
}
