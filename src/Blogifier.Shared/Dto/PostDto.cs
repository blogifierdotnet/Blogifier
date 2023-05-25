using System.Collections.Generic;
using System;

namespace Blogifier.Shared;

public class PostDto
{
  public int Id { get; set; }
  public string Title { get; set; } = default!;
  public string? Slug { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string Content { get; set; } = default!;
  public string? Cover { get; set; }
  public bool IsFeatured { get; set; }
  public DateTime? PublishedAt { get; set; }
  public PostType PostType { get; set; }
  public PostState State { get; set; }
  public List<Category>? Categories { get; set; }
  public bool Selected { get; set; }
}
