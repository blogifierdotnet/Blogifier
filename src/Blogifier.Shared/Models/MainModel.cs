namespace Blogifier.Models;

public class MainModel
{
  public string AbsoluteUrl { get; init; } = default!;
  public string SiteFeed { get; init; } = default!;
  public string Title { get; init; } = default!;
  public string Description { get; init; } = default!;
  public string? Logo { get; init; }
  public string Theme { get; init; } = default!;
  public string? HeaderScript { get; init; }
  public string? FooterScript { get; init; }
}
