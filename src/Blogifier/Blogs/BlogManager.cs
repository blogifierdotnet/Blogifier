using Blogifier.Data;
using Blogifier.Options;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blogifier.Blogs;

public class BlogManager
{
  private readonly ILogger _logger;
  private readonly AppDbContext _dbContext;
  private readonly OptionStore _optionStore;

  private BlogData? _blogData;

  public BlogManager(
    ILogger<BlogManager> logger,
    AppDbContext dbContext,
    OptionStore optionStore)
  {
    _logger = logger;
    _dbContext = dbContext;
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
      Title = BlogifierConstant.DefaultTitle,
      Description = BlogifierConstant.DefaultDescription,
      Theme = BlogifierConstant.DefaultTheme,
      ItemsPerPage = BlogifierConstant.DefaultItemsPerPage,
    };
    value = JsonSerializer.Serialize(_blogData);
    _logger.LogCritical("blog initialize {value}", value);
    await _optionStore.SetByCacheValue(BlogData.CacheKey, value);
    return _blogData;
  }

  public async Task<IEnumerable<Post>> GetPostsAsync(int page, int items)
  {
    var skip = (page - 1) * items;
    return await _dbContext.Posts.Skip(skip).Take(items).ToListAsync();
  }
}
