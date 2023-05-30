using Blogifier.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blogifier.Blogs;

public class BlogManager
{
  private readonly ILogger _logger;
  private readonly IDistributedCache _distributedCache;
  private readonly OptionProvider _optionStore;
  private BlogData? _blogData;

  public BlogManager(
    ILogger<BlogManager> logger,
    IDistributedCache distributedCache,
    OptionProvider optionStore)
  {
    _logger = logger;
    _distributedCache = distributedCache;
    _optionStore = optionStore;
  }

  public async Task<BlogData> GetAsync()
  {
    if (_blogData != null) return _blogData;
    var key = BlogData.CacheKey;
    _logger.LogDebug("get option {key}", key);
    var cache = await _distributedCache.GetAsync(key);
    if (cache != null)
    {
      var value = Encoding.UTF8.GetString(cache);
      _blogData = JsonSerializer.Deserialize<BlogData>(value);
      return _blogData!;
    }
    else
    { 
      var value = _optionStore.GetByValue(key);
    }
    throw new BlogNotIitializeException();
  }

  public async Task<bool> AnyAsync()
  {
    if (await _optionStore.AnyKey(BlogData.CacheKey))
      return true;
    await _optionStore.RemoveCacheValue(BlogData.CacheKey);
    return false;
  }

  public async Task SetAsync(BlogData blogData)
  {
    var value = JsonSerializer.Serialize(blogData);
    _logger.LogCritical("blog initialize {value}", value);
    await _optionStore.SetByCacheValue(BlogData.CacheKey, value);
  }


}
