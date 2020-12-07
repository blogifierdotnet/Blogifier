using System.Collections.Generic;

namespace Blogifier.Shared
{
	public class PageListModel
   {
      public IEnumerable<PostItem> Posts { get; set; }
      public Pager Pager { get; set; }
   }
}
