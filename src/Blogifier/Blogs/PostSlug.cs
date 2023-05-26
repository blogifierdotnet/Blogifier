using Blogifier.Shared;
using System.Collections.Generic;

namespace Blogifier.Blogs;

public class PostSlug
{
  public Post Post { get; set; } = default!;
  public Post? Older { get; set; }
  public Post? Newer { get; set; }
  public IEnumerable<Post> Related { get; set; } = default!;
}
