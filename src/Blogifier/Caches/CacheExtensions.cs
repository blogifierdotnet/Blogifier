using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.IO;

namespace Blogifier.Caches;

public static class CacheExtensions
{
  public static IServiceCollection AddCache(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
  {
    var redisConnectionString = configuration.GetSection("Blogifier:Redis").Value;
    if (string.IsNullOrEmpty(redisConnectionString))
    {
      services.AddDistributedMemoryCache();
      var dataProtectionPath = Path.Combine(environment.ContentRootPath, "App_Data", "DataProtection-Keys");
      var dataProtectionDirectory = new DirectoryInfo(dataProtectionPath);
      services.AddDataProtection().PersistKeysToFileSystem(dataProtectionDirectory);
    }
    else
    {
      services.AddStackExchangeRedisCache(options =>
      {
        options.Configuration = redisConnectionString;
        options.InstanceName = "blogifier:";
      });
      var redisConnectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
      services.AddDataProtection().PersistKeysToStackExchangeRedis(redisConnectionMultiplexer, "DataProtection-Keys");
    }
    return services;
  }
}
