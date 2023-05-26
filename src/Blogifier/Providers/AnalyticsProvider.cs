using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Providers;

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
                group post by new { Time = new { post.PublishedAt.Year, post.PublishedAt.Month, post.PublishedAt.Day } } into g
                select new BlogSumDto
                {
                  Time = g.Key.Time.Year + "-" + g.Key.Time.Month + "-" + g.Key.Time.Day,
                  Posts = g.Count(m => m.PostType == PostType.Post),
                  Pages = g.Count(m => m.PostType == PostType.Page),
                  Views = g.Sum(m => m.Views),
                };
    return await query.ToListAsync();
  }

  public async Task<AnalyticsDto> GetAnalytics()
  {
    var blog = await _dbContext.Blogs.FirstOrDefaultAsync();
    var model = new AnalyticsDto
    {
      TotalPosts = _dbContext.Posts.Where(p => p.PostType == PostType.Post).Count(),
      TotalPages = _dbContext.Posts.Where(p => p.PostType == PostType.Page).Count(),
      TotalViews = _dbContext.Posts.Select(v => v.Views).Sum(),
      TotalSubscribers = _dbContext.Subscribers.Count(),
      LatestPostViews = GetLatestPostViews(),
      DisplayType = blog.AnalyticsListType > 0 ? (AnalyticsListType)blog.AnalyticsListType : AnalyticsListType.Graph,
      DisplayPeriod = blog.AnalyticsPeriod > 0 ? (AnalyticsPeriod)blog.AnalyticsPeriod : AnalyticsPeriod.Days7
    };
    return await Task.FromResult(model);
  }

  public async Task<bool> SaveDisplayType(int type)
  {
    var blog = await _dbContext.Blogs.FirstOrDefaultAsync();
    blog.AnalyticsListType = type;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> SaveDisplayPeriod(int period)
  {
    var blog = await _dbContext.Blogs.OrderBy(b => b.Id).FirstAsync();
    blog.AnalyticsPeriod = period;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  private BarChartModel GetLatestPostViews()
  {
    var blog = _dbContext.Blogs.OrderBy(b => b.Id).First();
    var period = blog.AnalyticsPeriod == 0 ? 3 : blog.AnalyticsPeriod;

    var posts = _dbContext.Posts.AsNoTracking().Where(p => p.PublishedAt > DateTime.MinValue).OrderByDescending(p => p.PublishedAt).Take(GetDays(period));
    if (posts == null || posts.Count() < 3)
      return null;

    posts = posts.OrderBy(p => p.PublishedAt);

    return new BarChartModel()
    {
      Labels = posts.Select(p => p.Title).ToList(),
      Data = posts.Select(p => p.Views).ToList()
    };
  }

  private static int GetDays(int id)
  {
    return (AnalyticsPeriod)id switch
    {
      AnalyticsPeriod.Today => 1,
      AnalyticsPeriod.Yesterday => 2,
      AnalyticsPeriod.Days7 => 7,
      AnalyticsPeriod.Days30 => 30,
      AnalyticsPeriod.Days90 => 90,
      _ => throw new ApplicationException("Unknown analytics period"),
    };
  }
}
