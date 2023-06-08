using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Blogs;

public class AnalyticsProvider
{
  private readonly AppDbContext _dbContext;

  public AnalyticsProvider(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<BlogSumDto>> GetPostSummaryAsync()
  {
    var currTime = DateTime.UtcNow;
    var query = from post in _dbContext.Posts.AsNoTracking()
                where post.State >= PostState.Release && post.PublishedAt >= currTime.AddDays(-7)
                group post by new { Time = new { post.PublishedAt!.Value.Year, post.PublishedAt!.Value.Month, post.PublishedAt!.Value.Day } } into g
                select new BlogSumDto
                {
                  Time = g.Key.Time.Year + "-" + g.Key.Time.Month + "-" + g.Key.Time.Day,
                  Posts = g.Count(m => m.PostType == PostType.Post),
                  Pages = g.Count(m => m.PostType == PostType.Page),
                  Views = g.Sum(m => m.Views),
                };
    return await query.ToListAsync();
  }

  //public async Task SaveDisplayType(int type)
  //{
  //  var blog = await _dbContext.Blogs.FirstAsync();
  //  blog.AnalyticsListType = type;
  //  await _dbContext.SaveChangesAsync();
  //}

  //public async Task SaveDisplayPeriod(int period)
  //{
  //  var blog = await _dbContext.Blogs.OrderBy(b => b.Id).FirstAsync();
  //  blog.AnalyticsPeriod = period;
  //  await _dbContext.SaveChangesAsync();
  //}

}
