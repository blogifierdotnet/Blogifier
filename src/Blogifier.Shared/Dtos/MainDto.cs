using Blogifier.Identity;
using System.Collections.Generic;

namespace Blogifier.Shared;

public class MainDto
{
  public string Title { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string? Logo { get; set; }
  public string Theme { get; set; } = default!;
  public string? HeaderScript { get; set; }
  public string? FooterScript { get; set; }
  public string Version { get; set; } = default!;
  public int ItemsPerPage { get; set; }
  public IEnumerable<CategoryItemDto> Categories { get; set; } = default!;
  public string? AbsoluteUrl { get; set; }
  public string? PathUrl { get; set; }
  public BlogifierClaims? Claims { get; set; }
}
