using System.Collections.Generic;

namespace Blogifier.Shared;

public class CategoryModel : PostsModel
{
  public string Category { get; set; }

  public CategoryModel(string category, IEnumerable<PostItemDto> items, int page, MainDto main) : base(items, page, main)
  {
    Category = category;
  }
}
