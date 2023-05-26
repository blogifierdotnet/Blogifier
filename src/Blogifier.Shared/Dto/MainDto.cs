using Blogifier.Identity;
using System.Collections.Generic;

namespace Blogifier.Shared;

public class MainDto
{
  public string Title { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string? Logo { get; set; }
  public string? HeaderScript { get; set; }
  public string? FooterScript { get; set; }
  public string Version { get; set; } = default!;
  public IEnumerable<CategoryItemDto>? Categories { get; set; }
  public string? AbsoluteUrl { get; set; }
  public string? SiteFeed { get { return AbsoluteUrl; } }
  public BlogifierClaims? Claims { get; set; }
}
