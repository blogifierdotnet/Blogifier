using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Helper;
using Blogifier.Identity;
using Blogifier.Options;
using Blogifier.Shared;
using Microsoft.AspNetCore.Http;
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
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AppDbContext _dbContext;
  private readonly OptionStore _optionStore;
  private BlogData? _blogData;

  public BlogManager(ILogger<BlogManager> logger, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext, OptionStore optionStore)
  {
    _logger = logger;
    _httpContextAccessor = httpContextAccessor;
    _dbContext = dbContext;
    _optionStore = optionStore;
  }

  public async Task<MainData> GetAsync()
  {
    var blogData = await GetBlogDataAsync();
    var categoryItemes = await GetCategoryItemesAsync();
    var httpContext = _httpContextAccessor.HttpContext;
    if (httpContext != null)
    {
      var request = httpContext.Request;
      var absoluteUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}";
      var claims = BlogifierClaims.Analysis(httpContext.User);
      return new MainData(blogData, categoryItemes, absoluteUrl, claims);
    }
    return new MainData(blogData, categoryItemes);
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
      .Include(pc => pc.User)
      .OrderByDescending(m => m.CreatedAt)
      .Skip(skip)
      .Take(items)
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<IEnumerable<CategoryItem>> GetCategoryItemesAsync()
  {
    return await _dbContext.PostCategories
      .Include(pc => pc.Category)
      .GroupBy(m => new { m.Category.Id, m.Category.Content, m.Category.Description })
      .Select(m => new CategoryItem
      {
        Id = m.Key.Id,
        Category = m.Key.Content,
        Description = m.Key.Description,
        PostCount = m.Count()
      })
      .AsNoTracking()
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
    return await query.AsNoTracking().ToListAsync();
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

    return await query.AsNoTracking().ToListAsync();
  }

  public async Task<Post> AddPostAsync(Post post)
  {
    post.Slug = await GetSlugFromTitle(post.Title);

    var contentFiltr = StringHelper.RemoveImgTags(StringHelper.RemoveScriptTags(post.Content));
    var descriptionFiltr = StringHelper.RemoveImgTags(StringHelper.RemoveScriptTags(post.Description));
    post.Description = contentFiltr;
    post.Content = descriptionFiltr;

    if (post.State >= PostState.Release) post.PublishedAt = DateTime.UtcNow;

    _dbContext.Posts.Add(post);
    await _dbContext.SaveChangesAsync();
    return post;
  }

  public async Task<Post> UpdatePostAsync(Post postInput, int userId)
  {
    var post = await _dbContext.Posts.FirstAsync(m => m.Id == postInput.Id);
    if (post.UserId != userId) throw new BlogNotIitializeException();

    post.Slug = postInput.Slug;
    post.Title = postInput.Title;
    var contentFiltr = StringHelper.RemoveImgTags(StringHelper.RemoveScriptTags(postInput.Content));
    var descriptionFiltr = StringHelper.RemoveImgTags(StringHelper.RemoveScriptTags(postInput.Description));
    post.Description = contentFiltr;
    post.Content = descriptionFiltr;
    post.Cover = postInput.Cover;

    _dbContext.Update(post);
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

  public async Task<PostSlug> GetPostAsync(string slug)
  {
    var post = await _dbContext.Posts
      .Include(m => m.User)
      .Where(p => p.Slug == slug)
      .FirstAsync();

    post.Views++;
    await _dbContext.SaveChangesAsync();

    var older = await _dbContext.Posts
      .AsNoTracking()
      .Where(m => m.State >= PostState.Release && m.PublishedAt < post.PublishedAt)
      .OrderByDescending(p => p.PublishedAt)
      .FirstOrDefaultAsync();

    var newer = await _dbContext.Posts
      .AsNoTracking()
      .Where(m => m.State >= PostState.Release && m.PublishedAt > post.PublishedAt)
      .OrderBy(p => p.PublishedAt)
      .FirstOrDefaultAsync();

    var relatedQuery = _dbContext.Posts
       .AsNoTracking()
       .Include(m => m.User)
       .Where(m => m.State == PostState.Archive && m.Id != post.Id);

    if (older != null) relatedQuery = relatedQuery.Where(m => m.Id != older.Id);
    if (newer != null) relatedQuery = relatedQuery.Where(m => m.Id != newer.Id);
    var related = await relatedQuery.OrderByDescending(p => p.PublishedAt).Take(3).ToListAsync();
    return new PostSlug { Post = post, Older = older, Newer = newer, Related = related };
  }
}
