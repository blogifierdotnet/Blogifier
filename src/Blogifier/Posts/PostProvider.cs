using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Helper;
using Blogifier.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using ReverseMarkdown.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogifier.Posts;

public class PostProvider : AppProvider<Post, int>
{
  private readonly IMapper _mapper;

  public PostProvider(IMapper mapper, AppDbContext dbContext) : base(dbContext)
  {
    _mapper = mapper;
  }

  public async Task<PostDto> FirstAsync(int id)
  {
    var query = _dbContext.Posts
      .Where(p => p.Id == id);
    return await _mapper.ProjectTo<PostDto>(query).FirstAsync();
  }

  public async Task<PostDto> GetAsync(int id)
  {
    var query = _dbContext.Posts
      .AsNoTracking()
      .Where(p => p.Id == id);
    return await _mapper.ProjectTo<PostDto>(query).FirstAsync();
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
    var postQuery = _dbContext.Posts
      .Include(m => m.User)
      .Where(p => p.Slug == slug);
    var post = await _mapper.ProjectTo<PostToHtmlDto>(postQuery).FirstAsync();
    post.Views++;
    await _dbContext.SaveChangesAsync();

    var olderQuery = _dbContext.Posts
      .AsNoTracking()
      .Where(m => m.State >= PostState.Release && m.PublishedAt > post.PublishedAt)
      .OrderBy(p => p.PublishedAt);

    var older = await _mapper.ProjectTo<PostItemDto>(olderQuery).FirstOrDefaultAsync();

    var newerQuery = _dbContext.Posts
      .AsNoTracking()
      .Where(m => m.State >= PostState.Release && m.PublishedAt < post.PublishedAt)
      .OrderByDescending(p => p.PublishedAt);

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

  public async Task<PostPagerDto> GetPostsAsync(int page, int pageSize)
  {
    var skip = (page - 1) * pageSize;
    var query = _dbContext.Posts
      .AsNoTracking()
      .Include(pc => pc.User)
      .Where(m => m.PostType == PostType.Post && m.State >= PostState.Release);

    var total = await query.CountAsync();
    query = query.OrderByDescending(m => m.CreatedAt).Skip(skip).Take(pageSize);
    var items = await _mapper.ProjectTo<PostItemDto>(query).ToListAsync();
    return new PostPagerDto(items, total, page, pageSize);
  }

  public async Task<PostEditorDto> GetEditorAsync(string slug)
  {
    var query = _dbContext.Posts
      .AsNoTracking()
      .Include(m => m.PostCategories)!
      .ThenInclude(m => m.Category)
      //.Include(m => m.StorageReferences!.Where(s => s.Type == StorageReferenceType.Post))
      .AsSingleQuery()
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
    var query = _dbContext.Posts
      .AsNoTracking()
      .Where(p => p.PostType == postType);

    query = filter switch
    {
      PublishedStatus.Featured |
      PublishedStatus.Published => query.Where(p => p.State >= PostState.Release).OrderByDescending(p => p.PublishedAt),
      PublishedStatus.Drafts => query.Where(p => p.State == PostState.Draft).OrderByDescending(p => p.Id),
      _ => query.OrderByDescending(p => p.PublishedAt).ThenByDescending(p => p.CreatedAt),
    };

    return await _mapper.ProjectTo<PostItemDto>(query).ToListAsync();
  }

  public async Task<PostPagerDto> GetSearchAsync(string term, int page, int pageSize)
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
        //if (post.Content.ToLower().Contains(termItem))
        //{
        //  rank += Regex.Matches(post.Content.ToLower(), termItem).Count;
        //}
      }

      if (rank > 0)
      {
        postsSearch.Add(new PostSearch(post, rank));
      }
    }

    var total = postsSearch.Count;
    var skip = page * pageSize - pageSize;
    var items = postsSearch
      .OrderByDescending(r => r.Rank)
      .Skip(skip)
      .Take(pageSize)
      .Select(m => m.Post)
      .ToList();

    return new PostPagerDto(items, total, page, pageSize);
  }

  public async Task<PostPagerDto> GetByCategoryAsync(string category, int page, int pageSize)
  {
    var skip = (page - 1) * pageSize;
    var query = _dbContext.Categories
       .AsNoTracking()
       .Include(pc => pc.PostCategories)!
       .ThenInclude(m => m.Post)
       .ThenInclude(m => m.User)
       .Where(m => m.Content.Contains(category))
       .SelectMany(pc => pc.PostCategories!, (parent, child) => child.Post);
    var total = await query.CountAsync();
    query = query.Skip(skip).Take(pageSize);
    var items = await _mapper.ProjectTo<PostItemDto>(query).ToListAsync();
    return new PostPagerDto(items, total, page, pageSize);
  }

  public async Task<IEnumerable<PostItemDto>> GetSearchAsync(string term)
  {
    var query = _dbContext.Posts.AsNoTracking();
    if ("*".Equals(term, StringComparison.Ordinal))
      query = query.Where(p => p.Title.ToLower().Contains(term.ToLower()));
    return await _mapper.ProjectTo<PostItemDto>(query).ToListAsync();
  }

  public Task StateAsynct(int id, PostState state)
  {
    var query = _dbContext.Posts
      .Where(p => p.Id == id);
    return StateInternalAsynct(query, state);
  }

  public Task StateAsynct(IEnumerable<int> ids, PostState state)
  {
    var query = _dbContext.Posts
     .Where(p => ids.Contains(p.Id));
    return StateInternalAsynct(query, state);
  }

  public async Task StateInternalAsynct(IQueryable<Post> query, PostState state)
  {
    await query.ExecuteUpdateAsync(setters =>
        setters.SetProperty(b => b.State, state));
  }

  public async Task<string> AddAsync(PostEditorDto postInput, int userId)
  {
    var post = await AddInternalAsync(postInput, userId);
    await _dbContext.SaveChangesAsync();
    return post.Slug;
  }

  private async Task<Post> AddInternalAsync(PostEditorDto postInput, int userId)
  {
    var slug = await GetSlugFromTitle(postInput.Title);
    var postCategories = await CheckPostCategories(postInput.Categories);

    var contentScriptFiltr = StringHelper.HtmlScriptGeneratedRegex().Replace(postInput.Content, string.Empty);
    var descriptionScriptFiltr = StringHelper.HtmlScriptGeneratedRegex().Replace(postInput.Description, string.Empty);
    var contentFiltr = StringHelper.HtmlImgGeneratedRegex().Replace(contentScriptFiltr, string.Empty);
    var descriptionFiltr = StringHelper.HtmlImgGeneratedRegex().Replace(descriptionScriptFiltr, string.Empty);

    var publishedAt = GetPublishedAt(postInput.PublishedAt, postInput.State);
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

  private static DateTime? GetPublishedAt(DateTime? inputPublishedAt, PostState inputState)
  {
    if (inputState >= PostState.Release)
    {
      if (!inputPublishedAt.HasValue)
      {
        return DateTime.UtcNow;
      }
      return inputPublishedAt;
    }
    else
    {
      return null;
    }
  }

  public async Task<IEnumerable<PostEditorDto>> AddAsync(IEnumerable<PostEditorDto> posts, int userId)
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

  public async Task UpdateAsync(PostEditorDto postInput, int userId)
  {
    var post = await _dbContext.Posts
      .Include(m => m.PostCategories)!
      .ThenInclude(m => m.Category)
      .FirstAsync(m => m.Id == postInput.Id);

    if (post.UserId != userId) throw new BlogNotIitializeException();
    var postCategories = await CheckPostCategories(postInput.Categories, post.PostCategories);

    post.Slug = postInput.Slug!;
    post.Title = postInput.Title;

    var contentScriptFiltr = StringHelper.HtmlScriptGeneratedRegex().Replace(postInput.Content, string.Empty);
    var descriptionScriptFiltr = StringHelper.HtmlScriptGeneratedRegex().Replace(postInput.Description, string.Empty);
    var contentFiltr = StringHelper.HtmlImgGeneratedRegex().Replace(contentScriptFiltr, string.Empty);
    var descriptionFiltr = StringHelper.HtmlImgGeneratedRegex().Replace(descriptionScriptFiltr, string.Empty);

    post.Description = descriptionFiltr;
    post.Content = contentFiltr;
    post.Cover = postInput.Cover;
    post.PostCategories = postCategories;
    post.PublishedAt = GetPublishedAt(postInput.PublishedAt, postInput.State);
    post.State = postInput.State;
    _dbContext.Update(post);
    await _dbContext.SaveChangesAsync();
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
