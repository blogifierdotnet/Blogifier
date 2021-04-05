namespace Blogifier.Shared
{
	public class AnalyticsModel
	{
		public int TotalPosts { get; set; }
        public int TotalPages { get; set; }
		public int TotalViews { get; set; }
		public int TotalSubscribers { get; set; }

        public AnalyticsListType DisplayType { get; set; }
        public AnalyticsPeriod DisplayPeriod { get; set; }

		public BarChartModel LatestPostViews { get; set; }
	}
}
