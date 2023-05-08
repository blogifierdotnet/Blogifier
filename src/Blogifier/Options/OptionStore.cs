using Blogifier.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Options;

public class OptionStore
{
  private readonly ILogger _logger;
  private readonly IDistributedCache _distributedCache;
  private readonly AppDbContext _dbContext;

  public OptionStore(
    ILogger<OptionStore> logger,
    IDistributedCache distributedCache,
    AppDbContext dbContext)
  {
    _logger = logger;
    _distributedCache = distributedCache;
    _dbContext = dbContext;
  }

  public async Task<string?> GetByCacheValue(string key, DistributedCacheEntryOptions? options = null)
  {
    _logger.LogDebug("get option {key}", key);
    string? value;
    var cache = await _distributedCache.GetAsync(key);
    if (cache != null)
    {
      value = Encoding.UTF8.GetString(cache);
    }
    else
    {
      value = await _dbContext.Options.AsNoTracking().Where(m => m.Key == key).Select(m => m.Value).FirstOrDefaultAsync();
      if (value != null)
      {
        var bytes = Encoding.UTF8.GetBytes(value);
        var cacheOptions = options ?? new() { SlidingExpiration = TimeSpan.FromMinutes(15) };
        await _distributedCache.SetAsync(key, bytes, cacheOptions);
      }
    }
    _logger.LogDebug("return option {key}:{value}", key, value);
    return value;
  }

  public async Task SetByCacheValue(string key, string value, DistributedCacheEntryOptions? options = null)
  {
    var option = await _dbContext.Options.Where(m => m.Key == key).FirstOrDefaultAsync();
    if (option == null) _dbContext.Options.Add(new OptionInfo { Key = key, Value = value });
    else option.Value = value;
    await _dbContext.SaveChangesAsync();
    var bytes = Encoding.UTF8.GetBytes(value);
    var cacheOptions = options ?? new() { SlidingExpiration = TimeSpan.FromMinutes(15) };
    await _distributedCache.SetAsync(key, bytes, cacheOptions);
  }
}
