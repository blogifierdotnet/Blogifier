using Blogifier.Blogs;

namespace Blogifier.Models;

public class MainModel
{
  public MainModel(string absoluteUrl, BlogData blogData)
  {
    AbsoluteUrl = absoluteUrl;
    SiteFeed = $"{absoluteUrl}/feed/rss";
    Title = blogData.Title;
    Description = blogData.Description;
    Logo = blogData.Logo;
    Theme = blogData.Theme;
    HeaderScript = blogData.HeaderScript;
    FooterScript = blogData.FooterScript;
  }

  public string AbsoluteUrl { get; init; } = default!;
  public string SiteFeed { get; init; }
  public string Title { get; init; } = default!;
  public string Description { get; init; } = default!;
  public string? Logo { get; init; }
  public string Theme { get; init; } = default!;
  public string? HeaderScript { get; init; }
  public string? FooterScript { get; init; }
}
