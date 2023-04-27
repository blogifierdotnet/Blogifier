using Blogifier.Data;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blogifier.Options;

public class OptionManager
{
  private readonly ILogger _logger;
  private readonly IDistributedCache _distributedCache;
  private readonly AppDbContext _dbContext;

  public OptionManager(
    ILogger<OptionManager> logger,
    IDistributedCache distributedCache,
    AppDbContext dbContext)
  {
    _logger = logger;
    _distributedCache = distributedCache;
    _dbContext = dbContext;
  }

  public Task<string> GetThemeValueAsync() => GetValue(BlogifierConstant.ThemeKey);

  public async Task<string> GetValue(string key)
  {
    _logger.LogDebug("get option {key}", key);
    string? value;
    var cache = await _distributedCache.GetAsync(key);
    if (cache != null)
    {
      value = Encoding.Default.GetString(cache);
    }
    else
    {
      value = await _dbContext.Options
       .AsNoTracking()
       .Where(m => m.Key == key)
       .Select(m => m.Value)
       .FirstOrDefaultAsync();
      value ??= BlogifierConstant.DefaultOption[key];
      var bytes = Encoding.Default.GetBytes(value);
      await _distributedCache.SetAsync(key, bytes);
    }
    _logger.LogDebug("return option {key}:{value}", key, value);
    return value;
  }
}
