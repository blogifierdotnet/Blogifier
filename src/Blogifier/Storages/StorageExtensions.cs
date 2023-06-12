using AutoMapper;
using Blogifier.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net.Http;

namespace Blogifier.Storages;

public static class StorageExtensions
{
  public static IServiceCollection AddStorageStaticFiles(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IStorageProvider>(sp =>
    {
      var mapper = sp.GetRequiredService<IMapper>();
      var dbContext = sp.GetRequiredService<AppDbContext>();
      var section = configuration.GetSection("Blogifier:Minio");
      var enable = section.GetValue<bool>("Enable");
      if (enable)
      {
        var logger = sp.GetRequiredService<ILogger<StorageMinioProvider>>();
        var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
        return new StorageMinioProvider(logger, mapper, dbContext, httpClientFactory, section);
      }
      else
      {
        var logger = sp.GetRequiredService<ILogger<StorageLocalProvider>>();
        var hostEnvironment = sp.GetRequiredService<IHostEnvironment>();
        return new StorageLocalProvider(logger, mapper, dbContext, hostEnvironment);
      }
    });
    services.AddScoped<StorageManager>();
    return services;
  }

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
