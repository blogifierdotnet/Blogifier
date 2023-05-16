using Blogifier.Identity;
using System.Collections.Generic;

namespace Blogifier.Models;

public class MainModel
{
  public MainModel(string absoluteUrl, BlogifierClaims? claims, IEnumerable<CategoryItemDto> categories)
  {
    AbsoluteUrl = absoluteUrl;
    Claims = claims;
    Categories = categories;
  }
  public string AbsoluteUrl { get; set; }
  public string SiteFeed { get; set; } = default!;
  public string Title { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string? Logo { get; set; }
  public string Theme { get; set; } = default!;
  public string? HeaderScript { get; set; }
  public string? FooterScript { get; set; }
  public string Version { get; set; } = default!;
  public BlogifierClaims? Claims { get; set; }
  public IEnumerable<CategoryItemDto> Categories { get; set; }
}
