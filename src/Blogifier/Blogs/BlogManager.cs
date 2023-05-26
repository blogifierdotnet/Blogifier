using Blogifier.Options;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blogifier.Blogs;

public class BlogManager
{
  private readonly ILogger _logger;
  private readonly OptionStore _optionStore;
  private BlogData? _blogData;

  public BlogManager(
    ILogger<BlogManager> logger,
    OptionStore optionStore)
  {
    _logger = logger;
    _optionStore = optionStore;
  }

  public async Task<BlogData> GetAsync()
  {
    if (_blogData != null) return _blogData;
    var value = await _optionStore.GetByCacheValue(BlogData.CacheKey);
    if (value != null)
    {
      _blogData = JsonSerializer.Deserialize<BlogData>(value);
      return _blogData!;
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
