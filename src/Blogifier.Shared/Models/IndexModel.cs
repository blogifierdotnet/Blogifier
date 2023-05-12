using Blogifier.Shared;
using System.Collections.Generic;

namespace Blogifier.Models;

public class IndexModel : PagerModel<PostItemModel>
{
  public IndexModel(string absoluteUrl, IEnumerable<PostItemModel> itmes, int currentPage, int itemsPerPage = 10)
    : base(absoluteUrl, itmes, currentPage, itemsPerPage)
  {
  }
}
