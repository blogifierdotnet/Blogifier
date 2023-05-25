using System.Collections.Generic;

namespace Blogifier.Shared;

public class PostModel
{
  public BlogItem Blog { get; set; }
  public PostItemDto Post { get; set; }
  public PostItemDto Older { get; set; }
  public PostItemDto Newer { get; set; }
  public IEnumerable<PostItemDto> Related { get; set; }
}
