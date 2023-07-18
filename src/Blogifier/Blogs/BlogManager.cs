using Blogifier.Caches;
using Blogifier.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blogifier.Blogs;

public class BlogManager
{
  private readonly ILogger _logger;
  private readonly IDistributedCache _distributedCache;
  private readonly OptionProvider _optionProvider;
  private BlogData? _blogData;

  public BlogManager(
    ILogger<BlogManager> logger,
    IDistributedCache distributedCache,
    OptionProvider optionProvider)
  {
    _logger = logger;
    _distributedCache = distributedCache;
    _optionProvider = optionProvider;
  }

  public async Task<BlogData> GetAsync()
  {
    if (_blogData != null) return _blogData;
    var key = CacheKeys.BlogData;
    _logger.LogDebug("get option {key}", key);
    var cache = await _distributedCache.GetAsync(key);
    if (cache != null)
    {
      var value = Encoding.UTF8.GetString(cache);
      return Deserialize(value);
    }
    else
    {
      var value = await _optionProvider.GetByValueAsync(key);
      if (value != null)
      {

        var bytes = Encoding.UTF8.GetBytes(value);
        await _distributedCache.SetAsync(key, bytes, new() { SlidingExpiration = TimeSpan.FromMinutes(15) });
        return Deserialize(value);
      }
    }
    throw new BlogNotIitializeException();

    BlogData Deserialize(string value)
    {
      _logger.LogDebug("return option {key}:{value}", key, value);
      _blogData = JsonSerializer.Deserialize<BlogData>(value);
      return _blogData!;
    }
  }

  public async Task<bool> AnyAsync()
  {
    var key = CacheKeys.BlogData;
    if (await _optionProvider.AnyKeyAsync(key))
      return true;
    await _distributedCache.RemoveAsync(key);
    return false;
  }

  public async Task SetAsync(BlogData blogData)
  {
    var key = CacheKeys.BlogData;
    var value = JsonSerializer.Serialize(blogData);
    _logger.LogCritical("blog set {value}", value);
    var bytes = Encoding.UTF8.GetBytes(value);
    await _distributedCache.SetAsync(key, bytes, new() { SlidingExpiration = TimeSpan.FromMinutes(15) });
    await _optionProvider.SetValue(key, value);
  }

}
