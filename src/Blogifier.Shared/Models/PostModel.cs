using System.Collections.Generic;

namespace Blogifier.Shared
{
  public class PostModel
  {
    public BlogItem Blog { get; set; }
    public PostItemModel Post { get; set; }
    public PostItemModel Older { get; set; }
    public PostItemModel Newer { get; set; }
    public IEnumerable<PostItemModel> Related { get; set; }
  }
}
