using System;
using System.Collections.Generic;

namespace Blogifier.Shared;

public class ImportPostDto
{
  public DateTime UpdatedAt { get; set; }
  public string Title { get; set; } = default!;
  public string Slug { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string Content { get; set; } = default!;
  public string? Cover { get; set; }
  public int PostViews { get; set; }
  public double Rating { get; set; }
  public bool Selected { get; set; }
  public DateTime Published { get; set; }
  public List<ImportPostCategory>? PostCategories { get; set; }
}
