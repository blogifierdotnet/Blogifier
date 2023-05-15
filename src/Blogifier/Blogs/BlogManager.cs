using Blogifier.Data;
using Blogifier.Options;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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

  public BlogManager(ILogger<BlogManager> logger, AppDbContext dbContext, OptionStore optionStore)
  {
    _logger = logger;
    _dbContext = dbContext;
    _optionStore = optionStore;
  }

  public async Task<bool> AnyBlogDataAsync()
  {
    if(await _optionStore.AnyKey(BlogData.CacheKey))
      return true;

    await _optionStore.RemoveCacheValue(BlogData.CacheKey);
    return false;
  }

  public async Task SetBlogDataAsync(BlogData blogData)
  {
    var value = JsonSerializer.Serialize(blogData);
    _logger.LogCritical("blog initialize {value}", value);
    await _optionStore.SetByCacheValue(BlogData.CacheKey, value);
  }

  public async Task<BlogData> GetBlogDataAsync()
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

  public async Task<IEnumerable<Post>> GetPostsAsync(int page, int items)
  {
    var skip = (page - 1) * items;
    return await _dbContext.Posts
      .OrderByDescending(m => m.CreatedAt)
      .Skip(skip)
      .Take(items)
      .ToListAsync();
  }

  public async Task<IEnumerable<CategoryItem>> GetCategoryItemesAsync()
  {
    return await _dbContext.PostCategories.Include(pc => pc.Category)
          .GroupBy(m => new { m.Category.Id, m.Category.Content, m.Category.Description })
          .Select(m => new CategoryItem
          {
            Id = m.Key.Id,
            Category = m.Key.Content,
            Description = m.Key.Description,
            PostCount = m.Count()
          })
          .ToListAsync();
  }
}
