using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Posts;

public class CategoryProvider : AppProvider<Category, int>
{
  public CategoryProvider(AppDbContext dbContext) : base(dbContext)
  {
  }

  public async Task<List<CategoryItemDto>> GetItemsAsync()
  {
    return await _dbContext.Categories
      .Include(pc => pc.PostCategories)
      .GroupBy(m => new { m.Id, m.Content, m.Description })
      .Select(m => new CategoryItemDto
      {
        Id = m.Key.Id,
        Category = m.Key.Content,
        Description = m.Key.Description,
        PostCount = m.Sum(p => p.PostCategories!.Count())
      })
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<List<CategoryItemDto>> GetItemsExistPostAsync()
  {
    return await _dbContext.PostCategories
      .Include(pc => pc.Category)
      .GroupBy(m => new { m.Category.Id, m.Category.Content, m.Category.Description })
      .Select(m => new CategoryItemDto
      {
        Id = m.Key.Id,
        Category = m.Key.Content,
        Description = m.Key.Description,
        PostCount = m.Count()
      })
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<List<CategoryItemDto>> SearchCategories(string term)
  {
    var cats = await GetItemsAsync();

    if (term == "*")
      return cats;

    return cats.Where(c => c.Category.ToLower().Contains(term.ToLower())).ToList();
  }

  public async Task<Category> GetCategory(int categoryId)
  {
    return await _dbContext.Categories.AsNoTracking()
        .Where(c => c.Id == categoryId)
        .FirstOrDefaultAsync();
  }

  public async Task<ICollection<Category>> GetPostCategories(int postId)
  {
    return await _dbContext.PostCategories.AsNoTracking()
        .Where(pc => pc.PostId == postId)
        .Select(pc => pc.Category)
        .ToListAsync();
  }

  public async Task<bool> SaveCategory(Category category)
  {
    //Category existing = await _db.Categories.AsNoTracking()
    //    .Where(c => c.Content.ToLower() == category.Content.ToLower()).FirstOrDefaultAsync();

    //if (existing != null)
    //    return false; // already exists category with the same title

    Category dbCategory = await _dbContext.Categories.Where(c => c.Id == category.Id).FirstOrDefaultAsync();
    if (dbCategory == null)
      return false;

    dbCategory.Content = category.Content;
    dbCategory.Description = category.Description;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<Category> SaveCategory(string tag)
  {
    Category category = await _dbContext.Categories
        .AsNoTracking()
        .Where(c => c.Content == tag)
        .FirstOrDefaultAsync();

    if (category != null)
      return category;

    category = new Category()
    {
      Content = tag,
      CreatedAt = DateTime.UtcNow
    };
    _dbContext.Categories.Add(category);
    await _dbContext.SaveChangesAsync();

    return category;
  }

  public async Task<bool> AddPostCategory(int postId, string tag)
  {
    Category category = await SaveCategory(tag);

    if (category == null)
      return false;

    Post post = await _dbContext.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();
    if (post == null)
      return false;

    post.PostCategories ??= new List<PostCategory>();

    PostCategory postCategory = await _dbContext.PostCategories
        .AsNoTracking()
        .Where(pc => pc.CategoryId == category.Id)
        .Where(pc => pc.PostId == postId)
        .FirstOrDefaultAsync();

    if (postCategory == null)
    {
      _dbContext.PostCategories.Add(new PostCategory
      {
        CategoryId = category.Id,
        PostId = postId
      });
      return await _dbContext.SaveChangesAsync() > 0;
    }

    return false;
  }

  public async Task<bool> SavePostCategories(int postId, List<Category> categories)
  {
    List<PostCategory> existingPostCategories = await _dbContext.PostCategories.AsNoTracking()
        .Where(pc => pc.PostId == postId).ToListAsync();

    _dbContext.PostCategories.RemoveRange(existingPostCategories);

    await _dbContext.SaveChangesAsync();

    foreach (var cat in categories)
    {
      await AddPostCategory(postId, cat.Content);
    }

    return await _dbContext.SaveChangesAsync() > 0;
  }
}
