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

  public async Task<BlogData> GetBlogDataAsync()
  {
    if (_blogData != null) return _blogData;
    var value = await _optionStore.GetByCacheValue(BlogData.CacheKey);
    if (value != null)
    {
      _blogData = JsonSerializer.Deserialize<BlogData>(value);
      if (_blogData != null) return _blogData;
    }
    _blogData = new BlogData
    {
      Title = "Blog Title",
      Description = "Short Blog Description",
      Theme = "standard",
      ItemsPerPage = 10,
    };
    value = JsonSerializer.Serialize(_blogData);
    _logger.LogCritical("blog initialize {value}", value);
    await _optionStore.SetByCacheValue(BlogData.CacheKey, value);
    return _blogData;
  }
}
