using Blogifier.Core.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IAnalyticsProvider
	{
		Task<AnalyticsModel> GetAnalytics();
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
			var model = new AnalyticsModel()
			{
				TotalPosts = _db.Posts.Count(),
				TotalViews = _db.Posts.Select(v => v.PostViews).Sum(),
				TotalSubscribers = _db.Subscribers.Count(),
				LatestPostViews = GetLatestPostViews()
			};

			return await Task.FromResult(model);
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
