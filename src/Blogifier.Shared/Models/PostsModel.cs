using Blogifier.Identity;
using Blogifier.Shared;
using System.Collections.Generic;
namespace Blogifier.Models;

public class PostsModel : PagerModel
{
  public PostsModel(
    IEnumerable<PostItemDto> items,
    int currentPage,
    int itemsPerPage ,
    string absoluteUrl,
    BlogifierClaims? claims,
    IEnumerable<CategoryItemDto> categories)
    : base( currentPage, itemsPerPage, absoluteUrl, claims, categories)
  {
    Items = items;
  }
  public IEnumerable<PostItemDto> Items { get; set; } = default!;
}
