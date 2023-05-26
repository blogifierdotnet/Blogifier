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

  public BlogProvider(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Blog> GetBlog()
  {
    return await _dbContext.Blogs.OrderBy(b => b.Id).AsNoTracking().FirstAsync();
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
}
