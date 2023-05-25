using Blogifier.Shared;
using System.Collections.Generic;
namespace Blogifier.Models;

public class PostsModel : PagerModel
{
  public PostsModel(IEnumerable<PostItemDto> items, int currentPage) : base(currentPage)
  {
    Items = items;
  }
  public IEnumerable<PostItemDto> Items { get; set; } = default!;
}
