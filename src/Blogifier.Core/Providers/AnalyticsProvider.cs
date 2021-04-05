using Blogifier.Core.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
    public interface IAnalyticsProvider
	{
		Task<AnalyticsModel> GetAnalytics();
        Task<bool> SaveDisplayType(int type);
        Task<bool> SaveDisplayPeriod(int period);
    }

	public class AnalyticsProvider : IAnalyticsProvider
	{
		private readonly AppDbContext _db;

		public AnalyticsProvider(AppDbContext db)
		{
			_db = db;
		}

		public async Task<AnalyticsModel> GetAnalytics()
		{
            var blog = await _db.Blogs.FirstOrDefaultAsync();
			var model = new AnalyticsModel()
			{
				TotalPosts = _db.Posts.Where(p => p.PostType == PostType.Post).Count(),
                TotalPages = _db.Posts.Where(p => p.PostType == PostType.Page).Count(),
				TotalViews = _db.Posts.Select(v => v.PostViews).Sum(),
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
            var blog = await _db.Blogs.FirstOrDefaultAsync();
            blog.AnalyticsPeriod = period;
            return await _db.SaveChangesAsync() > 0;
        }

        private BarChartModel GetLatestPostViews()
		{
			var posts = _db.Posts.AsNoTracking().Where(p => p.Published > DateTime.MinValue).OrderByDescending(p => p.Published).Take(7);
			if (posts == null || posts.Count() < 3)
				return null;

			posts = posts.OrderBy(p => p.Published);

			return new BarChartModel()
			{
				Labels = posts.Select(p => p.Title).ToList(),
				Data = posts.Select(p => p.PostViews).ToList()
			};
		}
	}
}
