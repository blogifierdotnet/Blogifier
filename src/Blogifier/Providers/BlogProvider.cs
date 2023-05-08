using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Providers;

public class BlogProvider
{
  private readonly AppDbContext _dbContext;
  private readonly StorageProvider _storageProvider;

  public BlogProvider(AppDbContext dbContext, StorageProvider storageProvider)
  {
    _dbContext = dbContext;
    _storageProvider = storageProvider;
  }

  public async Task<Blog?> FirstOrDefaultAsync()
  {
    return await _dbContext.Blogs.AsNoTracking().FirstOrDefaultAsync();
  }

  public async Task<BlogItem> GetBlogItem()
  {
    var blog = await _dbContext.Blogs.AsNoTracking().OrderBy(b => b.Id).FirstAsync();
    blog.Theme = blog.Theme.ToLower();
    return new BlogItem
    {
      Title = blog.Title,
      Description = blog.Description,
      Theme = blog.Theme,
      IncludeFeatured = blog.IncludeFeatured,
      ItemsPerPage = blog.ItemsPerPage,
      SocialFields = new List<SocialField>(),
      Cover = string.IsNullOrEmpty(blog.Cover) ? blog.Cover : Constants.DefaultCover,
      Logo = string.IsNullOrEmpty(blog.Logo) ? blog.Logo : Constants.DefaultLogo,
      HeaderScript = blog.HeaderScript,
      FooterScript = blog.FooterScript,
      values = await GetValues(blog.Theme)
    };
  }

  public async Task<Blog> GetBlog()
  {
    return await _dbContext.Blogs.OrderBy(b => b.Id).AsNoTracking().FirstAsync();
  }

  public async Task<ICollection<Category>> GetBlogCategories()
  {
    return await _dbContext.Categories.AsNoTracking().ToListAsync();
  }

  public async Task<bool> Update(Blog blog)
  {
    var existing = await _dbContext.Blogs.OrderBy(b => b.Id).FirstAsync();

    existing.Title = blog.Title;
    existing.Description = blog.Description;
    existing.ItemsPerPage = blog.ItemsPerPage;
    existing.IncludeFeatured = blog.IncludeFeatured;
    existing.Theme = blog.Theme;
    existing.Cover = blog.Cover;
    existing.Logo = blog.Logo;
    existing.HeaderScript = blog.HeaderScript;
    existing.FooterScript = blog.FooterScript;
    existing.AnalyticsListType = blog.AnalyticsListType;
    existing.AnalyticsPeriod = blog.AnalyticsPeriod;

    return await _dbContext.SaveChangesAsync() > 0;
  }

  private async Task<dynamic> GetValues(string theme)
  {
    var settings = await _storageProvider.GetThemeSettingsAsync(theme);
    var values = new Dictionary<string, string>();

    if (settings != null && settings.Sections != null)
    {
      foreach (var section in settings.Sections)
      {
        if (section.Fields != null)
        {
          foreach (var field in section.Fields)
          {
            values.Add(field.Id, field.Value);
          }
        }
      }
    }
    return values;
  }
}
