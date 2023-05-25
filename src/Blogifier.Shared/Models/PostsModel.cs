using Blogifier.Shared;
using System.Collections.Generic;
namespace Blogifier.Models;

public class PostsModel : PagerModel
{
  public PostsModel(IEnumerable<PostItemDto> items, int currentPagem, MainDto main) : base(currentPagem, main)
  {
    Items = items;
  }
  public IEnumerable<PostItemDto> Items { get; set; } = default!;
}
