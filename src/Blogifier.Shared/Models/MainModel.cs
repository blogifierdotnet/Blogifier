namespace Blogifier.Models;

public class MainModel
{
  public MainModel(string absoluteUrl)
  {
    AbsoluteUrl = absoluteUrl;
  }
  public string AbsoluteUrl { get; set; }
  public string SiteFeed { get; set; } = default!;
  public string Title { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string? Logo { get; set; }
  public string Theme { get; set; } = default!;
  public string? HeaderScript { get; set; }
  public string? FooterScript { get; set; }
}
