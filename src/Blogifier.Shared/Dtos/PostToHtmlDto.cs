using System;
using System.Collections.Generic;

namespace Blogifier.Shared;

public class PostToHtmlDto
{
  public int Id { get; set; }
  public UserDto User { get; set; } = default!;
  public string Title { get; set; } = default!;
  public string? Slug { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string DescriptionHtml { get; set; } = default!;
  public string Content { get; set; } = default!;
  public string ContentHtml { get; set; } = default!;
  public string? Cover { get; set; }
  public int Views { get; set; }
  public DateTime? PublishedAt { get; set; }
  public PostType PostType { get; set; }
  public PostState State { get; set; }
  public List<CategoryDto>? Categories { get; set; }
}
