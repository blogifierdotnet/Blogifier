using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Providers;

public class AnalyticsProvider
{
  private readonly AppDbContext _db;

  public AnalyticsProvider(AppDbContext db)
  {
    _db = db;
  }

  public async Task<AnalyticsModel> GetAnalytics()
  {
    var blog = await _db.Blogs.FirstOrDefaultAsync();
    var model = new AnalyticsModel
    {
      TotalPosts = _db.Posts.Where(p => p.PostType == PostType.Post).Count(),
      TotalPages = _db.Posts.Where(p => p.PostType == PostType.Page).Count(),
      TotalViews = _db.Posts.Select(v => v.Views).Sum(),
      TotalSubscribers = _db.Subscribers.Count(),
      LatestPostViews = GetLatestPostViews(),
      DisplayType = blog.AnalyticsListType > 0 ? (AnalyticsListType)blog.AnalyticsListType : AnalyticsListType.Graph,
      DisplayPeriod = blog.AnalyticsPeriod > 0 ? (AnalyticsPeriod)blog.AnalyticsPeriod : AnalyticsPeriod.Days7
    };
    return await Task.FromResult(model);
  }

  public async Task<bool> SaveDisplayType(int type)
  {
    var blog = await _db.Blogs.FirstOrDefaultAsync();
    blog.AnalyticsListType = type;
    return await _db.SaveChangesAsync() > 0;
  }

  public async Task<bool> SaveDisplayPeriod(int period)
  {
    var blog = await _db.Blogs.OrderBy(b => b.Id).FirstAsync();
    blog.AnalyticsPeriod = period;
    return await _db.SaveChangesAsync() > 0;
  }

  private BarChartModel GetLatestPostViews()
  {
    var blog = _db.Blogs.OrderBy(b => b.Id).First();
    var period = blog.AnalyticsPeriod == 0 ? 3 : blog.AnalyticsPeriod;

    var posts = _db.Posts.AsNoTracking().Where(p => p.PublishedAt > DateTime.MinValue).OrderByDescending(p => p.PublishedAt).Take(GetDays(period));
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
