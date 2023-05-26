using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Shared;
using Blogifier.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogifier.Providers;

public class PostProvider
{
  private readonly AppDbContext _db;
  private readonly CategoryProvider _categoryProvider;

  public PostProvider(AppDbContext db, CategoryProvider categoryProvider)
  {
    _db = db;
    _categoryProvider = categoryProvider;
  }


  public async Task<List<Post>> SearchPosts(string term)
  {
    if (term == "*")
      return await _db.Posts.ToListAsync();

    return await _db.Posts
        .AsNoTracking()
        .Where(p => p.Title.ToLower().Contains(term.ToLower()))
        .ToListAsync();
  }


  public async Task<Post> GetPostById(int id)
  {
    return await _db.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
  }







  public async Task<bool> Publish(int id, bool publish)
  {
    var existing = await _db.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
    if (existing == null)
      return false;

    existing.PublishedAt = publish ? DateTime.UtcNow : DateTime.MinValue;

    return await _db.SaveChangesAsync() > 0;
  }

  public async Task<bool> Featured(int id, bool featured)
  {
    var existing = await _db.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
    if (existing == null)
      return false;

    existing.IsFeatured = featured;

    return await _db.SaveChangesAsync() > 0;
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
          Category cat = _db.Categories.Single(c => c.Content.ToLower() == category.ToLower());
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
    var existing = await _db.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
    if (existing == null)
      return false;

    _db.Posts.Remove(existing);
    await _db.SaveChangesAsync();
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
      Author = _db.Authors.Single(a => a.Id == p.AuthorId),
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
           _db.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt == DateTime.MinValue && p.AuthorId == author && p.PostType == PostType.Post).ToList() :
           _db.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt == DateTime.MinValue && p.PostType == PostType.Post).ToList();
      items = items.Concat(drafts).ToList();
    }

    if (include.ToUpper().Contains(BlogifierConstant.PostFeatured) || string.IsNullOrEmpty(include))
    {
      var featured = author > 0 ?
           _db.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt > DateTime.MinValue && p.IsFeatured && p.AuthorId == author && p.PostType == PostType.Post).OrderByDescending(p => p.PublishedAt).ToList() :
           _db.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt > DateTime.MinValue && p.IsFeatured && p.PostType == PostType.Post).OrderByDescending(p => p.PublishedAt).ToList();
      pubfeatured = pubfeatured.Concat(featured).ToList();
    }

    if (include.ToUpper().Contains(BlogifierConstant.PostPublished) || string.IsNullOrEmpty(include))
    {
      var published = author > 0 ?
           _db.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt > DateTime.MinValue && !p.IsFeatured && p.AuthorId == author && p.PostType == PostType.Post).OrderByDescending(p => p.PublishedAt).ToList() :
           _db.Posts.Include(p => p.PostCategories).Where(p => p.PublishedAt > DateTime.MinValue && !p.IsFeatured && p.PostType == PostType.Post).OrderByDescending(p => p.PublishedAt).ToList();
      pubfeatured = pubfeatured.Concat(published).ToList();
    }

    pubfeatured = pubfeatured.OrderByDescending(p => p.PublishedAt).ToList();
    items = items.Concat(pubfeatured).ToList();

    return items;
  }

  #endregion
}
