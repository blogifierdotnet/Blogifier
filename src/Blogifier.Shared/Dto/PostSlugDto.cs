using System.Collections.Generic;

namespace Blogifier.Shared;

public class PostSlugDto
{
  public PostDto Post { get; set; } = default!;
  public PostDto? Older { get; set; }
  public PostDto? Newer { get; set; }
  public IEnumerable<PostDto> Related { get; set; } = default!;
}
