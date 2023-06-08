namespace Blogifier.Models;

using Blogifier.Blogs;
using Blogifier.Shared;
using System.Collections.Generic;
using System.Linq;

public class PostsModel : PagerModel
{
  public IEnumerable<PostItem> Posts { get; set; } = default!;
}
