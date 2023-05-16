using Blogifier.Identity;
using Blogifier.Shared;
using System.Collections.Generic;
namespace Blogifier.Models;

public class IndexModel : PostsModel
{
  public IndexModel(
    IEnumerable<PostItemDto> items,
    int currentPage,
    int itemsPerPage,
    string absoluteUrl,
    BlogifierClaims? claims,
    IEnumerable<CategoryItemDto> categories)
    : base(items, currentPage, itemsPerPage, absoluteUrl, claims, categories)
  {
  }
}
