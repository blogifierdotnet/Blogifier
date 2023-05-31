using System.Collections.Generic;

namespace Blogifier.Shared;

public class SearchModel : PostsModel
{
  public SearchModel(IEnumerable<PostItemDto> items, int page, MainDto main) : base(items, page, main)
  {
  }
}
