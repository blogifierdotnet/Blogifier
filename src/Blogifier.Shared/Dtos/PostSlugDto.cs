using System.Collections.Generic;

namespace Blogifier.Shared;

public class PostSlugDto
{
  public PostToHtmlDto Post { get; set; } = default!;
  public PostItemDto? Older { get; set; }
  public PostItemDto? Newer { get; set; }
  public IEnumerable<PostToHtmlDto> Related { get; set; } = default!;
}
