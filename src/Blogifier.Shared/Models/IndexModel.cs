using Blogifier.Shared;
using System.Collections.Generic;
namespace Blogifier.Models;

public class IndexModel : PostsModel
{
  public IndexModel(IEnumerable<PostItemDto> items, int page, MainDto main) : base(items, page, main)
  {
  }
}
