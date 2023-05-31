using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Helper;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogifier.Posts;

public class PostProvider
{
  private readonly IMapper _mapper;
  private readonly AppDbContext _dbContext;

  public PostProvider(
    IMapper mapper,
    AppDbContext dbContext)
  {
    _mapper = mapper;
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<PostDto>> GetAsync()
  {
    var query = _dbContext.Posts
      .AsNoTracking()
      .Include(pc => pc.User)
      .OrderByDescending(m => m.CreatedAt);
    return await _mapper.ProjectTo<PostDto>(query).ToListAsync();
  }

  public async Task<PostSlugDto> GetAsync(string slug)
  {
    var postQuery = _dbContext.Posts.Include(m => m.User).Where(p => p.Slug == slug);
    var post = await _mapper.ProjectTo<PostToHtmlDto>(postQuery).FirstAsync();
    post.Views++;
    await _dbContext.SaveChangesAsync();

    var olderQuery = _dbContext.Posts
      .AsNoTracking()
      .Where(m => m.State >= PostState.Release && m.PublishedAt < post.PublishedAt)
      .OrderByDescending(p => p.PublishedAt);

    var older = await _mapper.ProjectTo<PostItemDto>(olderQuery).FirstOrDefaultAsync();

    var newerQuery = _dbContext.Posts
      .AsNoTracking()
      .Where(m => m.State >= PostState.Release && m.PublishedAt > post.PublishedAt)
      .OrderBy(p => p.PublishedAt);

    var newer = await _mapper.ProjectTo<PostItemDto>(newerQuery).FirstOrDefaultAsync();

    var relatedQuery = _dbContext.Posts
       .AsNoTracking()
       .Include(m => m.User)
       .Where(m => m.State == PostState.Featured && m.Id != post.Id);

    if (older != null) relatedQuery = relatedQuery.Where(m => m.Id != older.Id);
    if (newer != null) relatedQuery = relatedQuery.Where(m => m.Id != newer.Id);
    relatedQuery = relatedQuery.OrderByDescending(p => p.PublishedAt).Take(3);
    var related = await _mapper.ProjectTo<PostToHtmlDto>(relatedQuery).ToListAsync();

    return new PostSlugDto { Post = post, Older = older, Newer = newer, Related = related };
  }

  public async Task<IEnumerable<PostItemDto>> GetAsync(int page, int items)
  {
    var skip = (page - 1) * items;

    var query = _dbContext.Posts
       .AsNoTracking()
       .Include(pc => pc.User)
       .OrderByDescending(m => m.CreatedAt)
       .Skip(skip)
       .Take(items);

    return await _mapper.ProjectTo<PostItemDto>(query).ToListAsync();
  }

  public async Task<PostEditorDto> GetEditorAsync(string slug)
  {
    var query = _dbContext.Posts
      .AsNoTracking()
      .Include(m => m.PostCategories)!
      .ThenInclude(m => m.Category)
      .Where(p => p.Slug == slug);
    return await _mapper.ProjectTo<PostEditorDto>(query).FirstAsync();
  }

  public async Task<List<PostEditorDto>> MatchTitleAsync(IEnumerable<string> titles)
  {
    var query = _dbContext.Posts
      .AsNoTracking()
      .Include(m => m.PostCategories)!
      .ThenInclude(m => m.Category)
      .Where(p => titles.Contains(p.Title));
    return await _mapper.ProjectTo<PostEditorDto>(query).ToListAsync();
  }

  public async Task<IEnumerable<PostItemDto>> GetAsync(PublishedStatus filter, PostType postType)
  {
    var query = _dbContext.Posts.AsNoTracking()
      .Where(p => p.PostType == postType);

    query = filter switch
    {
      PublishedStatus.Featured |
      PublishedStatus.Published => query.Where(p => p.State >= PostState.Release).OrderByDescending(p => p.PublishedAt),
      PublishedStatus.Drafts => query.Where(p => p.State == PostState.Draft).OrderByDescending(p => p.Id),
      _ => query.OrderByDescending(p => p.Id),
    };

    return await _mapper.ProjectTo<PostItemDto>(query).ToListAsync();
  }

  public async Task<IEnumerable<PostItemDto>> SearchAsync(string term, int page, int items)
  {
    var query = _dbContext.Posts
     .AsNoTracking()
     .Include(pc => pc.User)
     .Include(pc => pc.PostCategories)
     .Where(m => m.Title.Contains(term) || m.Description.Contains(term) || m.Content.Contains(term));


    var posts = await _mapper.ProjectTo<PostItemDto>(query).ToListAsync();

    var postsSearch = new List<PostSearch>();
    var termList = term.ToLower().Split(' ').ToList();

    foreach (var post in posts)
    {
      var rank = 0;
      var hits = 0;

      foreach (var termItem in termList)
      {
        if (termItem.Length < 4 && rank > 0) continue;

        if (post.Categories != null)
        {
          foreach (var category in post.Categories)
          {
            if (category.Content.ToLower() == termItem) rank += 10;
          }
        }
        if (post.Title.ToLower().Contains(termItem))
        {
          hits = Regex.Matches(post.Title.ToLower(), termItem).Count;
          rank += hits * 10;
        }
        if (post.Description.ToLower().Contains(termItem))
        {
          hits = Regex.Matches(post.Description.ToLower(), termItem).Count;
          rank += hits * 3;
        }
        if (post.Content.ToLower().Contains(termItem))
        {
          rank += Regex.Matches(post.Content.ToLower(), termItem).Count;
        }
      }

      if (rank > 0)
      {
        postsSearch.Add(new PostSearch(post, rank));
      }
    }

    var skip = page * items - items;

    var results = postsSearch
      .OrderByDescending(r => r.Rank)
      .Skip(skip)
      .Take(items)
      .Select(m => m.Post)
      .ToList();

    return results;
  }

  public async Task<IEnumerable<PostItemDto>> CategoryAsync(string category, int page, int items)
  {
    var skip = (page - 1) * items;
    var query = _dbContext.PostCategories
       .AsNoTracking()
       .Include(pc => pc.Post)
       .ThenInclude(m => m.User)
       .Where(m => m.Category.Content.Contains(category))
       .Select(m => m.Post)
       .Skip(skip)
       .Take(items);
    var posts = await _mapper.ProjectTo<PostItemDto>(query).ToListAsync();
    return posts;
  }

  public async Task<List<Post>> SearchPosts(string term)
  {
    if (term == "*")
      return await _dbContext.Posts.ToListAsync();

    return await _dbContext.Posts
        .AsNoTracking()
        .Where(p => p.Title.ToLower().Contains(term.ToLower()))
        .ToListAsync();
  }

  public async Task<Post> GetPostById(int id)
  {
    return await _dbContext.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
  }

  public async Task<bool> Publish(int id, bool publish)
  {
    var existing = await _dbContext.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
    if (existing == null)
      return false;

    existing.PublishedAt = publish ? DateTime.UtcNow : DateTime.MinValue;

    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> Featured(int id, bool featured)
  {
    var existing = await _dbContext.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
    if (existing == null)
      return false;

    existing.IsFeatured = featured;

    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> Remove(int id)
  {
    var existing = await _dbContext.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
    if (existing == null)
      return false;

    _dbContext.Posts.Remove(existing);
    await _dbContext.SaveChangesAsync();
    return true;
  }

  public async Task<PostEditorDto> AddAsync(PostEditorDto postInput, string userId)
  {
    var post = await AddInternalAsync(postInput, userId);
    await _dbContext.SaveChangesAsync();
    return _mapper.Map<PostEditorDto>(post);
  }

  private async Task<Post> AddInternalAsync(PostEditorDto postInput, string userId)
  {
    var slug = await GetSlugFromTitle(postInput.Title);
    var postCategories = await CheckPostCategories(postInput.Categories);
    var contentFiltr = StringHelper.RemoveImgTags(StringHelper.RemoveScriptTags(postInput.Content));
    var descriptionFiltr = StringHelper.RemoveImgTags(StringHelper.RemoveScriptTags(postInput.Description));
    var publishedAt = postInput.PublishedAt;
    if (postInput.State >= PostState.Release)
    {
      if (!publishedAt.HasValue)
      {
        publishedAt = DateTime.UtcNow;
      }
    }
    else
    {
      publishedAt = null;
    }
    var post = new Post
    {
      UserId = userId,
      Title = postInput.Title,
      Slug = slug,
      Content = contentFiltr,
      Description = descriptionFiltr,
      Cover = postInput.Cover,
      PostType = postInput.PostType,
      State = postInput.State,
      PublishedAt = publishedAt,
      PostCategories = postCategories,
    };
    _dbContext.Posts.Add(post);
    return post;
  }

  public async Task<IEnumerable<PostEditorDto>> AddAsync(IEnumerable<PostEditorDto> posts, string userId)
  {
    var postsInput = new List<Post>();
    foreach (var post in posts)
    {
      var postInput = await AddInternalAsync(post, userId);
      postsInput.Add(postInput);
    }
    await _dbContext.SaveChangesAsync();
    return _mapper.Map<IEnumerable<PostEditorDto>>(postsInput);
  }

  public async Task<PostEditorDto> UpdateAsync(PostEditorDto postInput, string userId)
  {
    var post = await _dbContext.Posts
      .Include(m => m.PostCategories)!
      .ThenInclude(m => m.Category)
      .FirstAsync(m => m.Id == postInput.Id);

    if (post.UserId != userId) throw new BlogNotIitializeException();
    var postCategories = await CheckPostCategories(postInput.Categories, post.PostCategories);

    post.Slug = postInput.Slug!;
    post.Title = postInput.Title;
    var contentFiltr = StringHelper.RemoveImgTags(StringHelper.RemoveScriptTags(postInput.Content));
    var descriptionFiltr = StringHelper.RemoveImgTags(StringHelper.RemoveScriptTags(postInput.Description));
    post.Description = contentFiltr;
    post.Content = descriptionFiltr;
    post.Cover = postInput.Cover;
    post.PostCategories = postCategories;

    _dbContext.Update(post);
    await _dbContext.SaveChangesAsync();
    return _mapper.Map<PostEditorDto>(post);
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

  private async Task<List<PostCategory>?> CheckPostCategories(List<CategoryDto>? input, List<PostCategory>? original = null)
  {
    if (input == null || !input.Any()) return null;

    // 去重
    var categories = input.GroupBy(d => new { d.Content }).Select(m => m.Key.Content).ToList();

    if (original == null)
    {
      original = new List<PostCategory>();
    }
    else
    {
      original = original.Where(p =>
      {
        var item = categories.FirstOrDefault(m => p.Category.Content.Equals(m, StringComparison.Ordinal));
        if (item != null)
        {
          categories.Remove(item);
          return true;
        }
        return false;
      }).ToList();
    }

    if (categories.Any())
    {
      var categoriesDb = await _dbContext.Categories
        .Where(m => categories.Contains(m.Content))
        .ToListAsync();

      foreach (var item in categories)
      {
        var categoryDb = categoriesDb.FirstOrDefault(m => item.Equals(m.Content, StringComparison.Ordinal));
        original.Add(new PostCategory { Category = categoryDb ?? new Category { Content = item } });
      }
    }
    return original;
  }

}
