using Blogifier.Identity;
using Blogifier.Shared;
using System.Collections.Generic;

namespace Blogifier.Models;

public class MainModel : BaseModel
{
  public MainModel(string absoluteUrl, BlogifierClaims? claims, IEnumerable<CategoryItemDto> categories)
  {
    AbsoluteUrl = absoluteUrl;
    Claims = claims;
    Categories = categories;
  }
  public string AbsoluteUrl { get; set; }
  public BlogifierClaims? Claims { get; set; }
  public IEnumerable<CategoryItemDto> Categories { get; set; }
}
