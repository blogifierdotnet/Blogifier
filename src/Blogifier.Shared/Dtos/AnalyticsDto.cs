using System.Collections.Generic;

namespace Blogifier.Shared;

public class AnalyticsDto
{
  public IEnumerable<BlogSumDto> Blogs { get; set; } = default!;
  public int TotalPosts { get; set; }
  public int TotalPages { get; set; }
  public int TotalViews { get; set; }
  public int TotalSubscribers { get; set; }
  public AnalyticsListType DisplayType { get; set; }
  public AnalyticsPeriod DisplayPeriod { get; set; }
  public BarChartModel LatestPostViews { get; set; } = default!;
}
