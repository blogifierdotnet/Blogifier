using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Data;
using Blogifier.Providers;
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
  private readonly CategoryProvider _categoryProvider;

  public PostProvider(
    IMapper mapper,
    AppDbContext dbContext,
    CategoryProvider categoryProvider)
  {
    _mapper = mapper;
    _dbContext = dbContext;
    _categoryProvider = categoryProvider;
  }

  public async Task<IEnumerable<PostDto>> GetAsync()
  {
    var postQuery = _dbContext.Posts
      .AsNoTracking()
      .Include(pc => pc.User)
      .OrderByDescending(m => m.CreatedAt);
    return await _mapper.ProjectTo<PostDto>(postQuery).ToListAsync();
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
       .Where(m => m.State == PostState.Archive && m.Id != post.Id);

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

  public async Task<IEnumerable<PostItemDto>> GetList(Pager pager, int author = 0, string category = "", string include = "", bool sanitize = true)
  {
    var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

    var posts = new List<Post>();
    foreach (var p in GetPosts(include, author))
    {
      if (string.IsNullOrEmpty(category))
      {
        posts.Add(p);
      }
      else
      {
        if (p.PostCategories != null && p.PostCategories.Count > 0)
        {
          Category cat = _dbContext.Categories.Single(c => c.Content.ToLower() == category.ToLower());
          if (cat == null)
            continue;

          foreach (var pc in p.PostCategories)
          {
            if (pc.CategoryId == cat.Id)
            {
              posts.Add(p);
            }
          }
        }
      }
    }
    pager.Configure(posts.Count);

    var items = new List<PostItemDto>();
    foreach (var p in posts.Skip(skip).Take(pager.ItemsPerPage).ToList())
    {
      items.Add(await PostToItem(p, sanitize));
    }
    return await Task.FromResult(items);
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

  #region Private methods

  async Task<PostItemDto> PostToItem(Post p, bool sanitize = false)
  {
    var post = new PostItemDto
    {
      Id = p.Id,
      PostType = p.PostType,
      Slug = p.Slug,
      Title = p.Title,
      Description = p.Description,
      Content = p.Content,
      Categories = await _categoryProvider.GetPostCategories(p.Id),
      Cover = p.Cover,
      PostViews = p.Views,
      Rating = p.Rating,
      Published = p.PublishedAt,
      Featured = p.IsFeatured,
      Author = _dbContext.Authors.Single(a => a.Id == p.AuthorId),
      SocialFields = new List<SocialField>()
    };

    if (post.Author != null)
    {
      if (string.IsNullOrEmpty(post.Author.Avatar))
        string.Format(BlogifierConstant.AvatarDataImage, post.Author.DisplayName.Substring(0, 1).ToUpper());

      post.Author.Email = sanitize ? "donotreply@us.com" : post.Author.Email;
    }
    return await Task.FromResult(post);
  }

  List<Post> GetPosts(string include, int author)
  {
    var items = new List<Post>();
    var pubfeatured = new List<Post>();

    if (include.ToUpper().Contains(BlogifierConstant.PostDraft) || string.IsNullOrEmpty(include))
    {
      var drafts = author > 0 ?
           _dbContext.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt == DateTime.MinValue && p.AuthorId == author && p.PostType == PostType.Post).ToList() :
           _dbContext.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt == DateTime.MinValue && p.PostType == PostType.Post).ToList();
      items = items.Concat(drafts).ToList();
    }

    if (include.ToUpper().Contains(BlogifierConstant.PostFeatured) || string.IsNullOrEmpty(include))
    {
      var featured = author > 0 ?
           _dbContext.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt > DateTime.MinValue && p.IsFeatured && p.AuthorId == author && p.PostType == PostType.Post).OrderByDescending(p => p.PublishedAt).ToList() :
           _dbContext.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt > DateTime.MinValue && p.IsFeatured && p.PostType == PostType.Post).OrderByDescending(p => p.PublishedAt).ToList();
      pubfeatured = pubfeatured.Concat(featured).ToList();
    }

    if (include.ToUpper().Contains(BlogifierConstant.PostPublished) || string.IsNullOrEmpty(include))
    {
      var published = author > 0 ?
           _dbContext.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt > DateTime.MinValue && !p.IsFeatured && p.AuthorId == author && p.PostType == PostType.Post).OrderByDescending(p => p.PublishedAt).ToList() :
           _dbContext.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt > DateTime.MinValue && !p.IsFeatured && p.PostType == PostType.Post).OrderByDescending(p => p.PublishedAt).ToList();
      pubfeatured = pubfeatured.Concat(published).ToList();
    }

    pubfeatured = pubfeatured.OrderByDescending(p => p.PublishedAt).ToList();
    items = items.Concat(pubfeatured).ToList();

    return items;
  }

  #endregion
}
