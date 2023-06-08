using System.Collections.Generic;

namespace Blogifier.Shared
{
	public class PostModel
   {
      public BlogItem Blog { get; set; }
      public PostItem Post { get; set; }
      public PostItem Older { get; set; }
      public PostItem Newer { get; set; }
      public IEnumerable<PostItem> Related { get; set; }
   }
}
