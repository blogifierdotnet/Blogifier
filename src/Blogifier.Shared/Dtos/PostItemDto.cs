using System;
using System.Collections.Generic;

namespace Blogifier.Shared;

public class PostItemDto
{
  public int Id { get; set; }
  public UserDto User { get; set; } = default!;
  public string Title { get; set; } = default!;
  public string Slug { get; set; } = default!;
  public string Description { get; set; } = default!;
  public List<CategoryDto>? Categories { get; set; }
  public string? Cover { get; set; }
  public PostState State { get; set; }
  public DateTime? PublishedAt { get; set; }
}
