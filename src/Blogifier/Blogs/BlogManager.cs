using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Helper;
using Blogifier.Options;
using Blogifier.Shared;
using Blogifier.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
    if (await _optionStore.AnyKey(BlogData.CacheKey))
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

  public async Task<IEnumerable<BlogSumInfo>> GetBlogSumInfoAsync()
  {
    var currTime = DateTime.UtcNow;
    var query = from post in _dbContext.Posts
                where post.State >= PostState.Release && post.PublishedAt >= currTime.AddDays(-7)
                group post by new { Time = new { post.PublishedAt.Year, post.PublishedAt.Month, post.PublishedAt.Day } } into g
                select new BlogSumInfo
                {
                  Time = g.Key.Time.Year + "-" + g.Key.Time.Month + "-" + g.Key.Time.Day,
                  Posts = g.Count(m => m.PostType == PostType.Post),
                  Pages = g.Count(m => m.PostType == PostType.Page),
                  Views = g.Sum(m => m.Views),
                };
    return await query.ToListAsync();
  }

  public async Task<IEnumerable<Post>> GetPostsAsync(PublishedStatus filter, PostType postType)
  {
    var query = _dbContext.Posts.AsNoTracking()
      .Where(p => p.PostType == postType);

    query = filter switch
    {
      PublishedStatus.Published => query.Where(p => p.State >= PostState.Release).OrderByDescending(p => p.PublishedAt),
      PublishedStatus.Drafts => query.Where(p => p.State == PostState.Draft).OrderByDescending(p => p.Id),
      PublishedStatus.Featured => query.Where(p => p.IsFeatured).OrderByDescending(p => p.Id),
      _ => query.OrderByDescending(p => p.Id),
    };

    return await query.ToListAsync();
  }

  public async Task<Post> AddPostAsync(Post post)
  {
    post.Slug = await GetSlugFromTitle(post.Title);

    var contentRemoveScriptTags = StringHelper.RemoveScriptTags(post.Content);
    var contentRemoveImgTags = StringHelper.RemoveImgTags(contentRemoveScriptTags);
    var descriptionRemoveScriptTags = StringHelper.RemoveScriptTags(post.Description);
    var descriptionRemoveImgTags = StringHelper.RemoveImgTags(descriptionRemoveScriptTags);
    post.Description = descriptionRemoveImgTags;
    post.Content = contentRemoveImgTags;

    if (post.State >= PostState.Release) post.PublishedAt = DateTime.UtcNow;

    _dbContext.Posts.Add(post);
    await _dbContext.SaveChangesAsync();
    return post;
  }

  private async Task<string> GetSlugFromTitle(string title)
  {
    var slug = title.ToSlug();
    var i = 1;
    var slugOriginal = slug;
    while (true)
    {
      if (!await _dbContext.Posts.Where(p => p.Slug == slug).AnyAsync()) return slug;
      i++;
      if (i >= 100) throw new BlogNotIitializeException();
      slug = $"{slugOriginal}{i}";
    }
  }
}
