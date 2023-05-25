using Blogifier.Identity;
using System.Collections.Generic;

namespace Blogifier.Models;

public class MainModel
{
  public string Title { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string? Logo { get; set; }
  public string Theme { get; set; } = default!;
  public string? HeaderScript { get; set; }
  public string? FooterScript { get; set; }
  public string Version { get; set; } = default!;
  public int ItemsPerPage { get; set; }
  public IEnumerable<CategoryItemDto>? Categories { get; set; }
  public string? AbsoluteUrl { get; set; }
  public string? SiteFeed { get { return AbsoluteUrl; } }
  public BlogifierClaims? Claims { get; set; }
}
