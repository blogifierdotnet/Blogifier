using Blogifier.Identity;
using Blogifier.Shared;
using System.Collections.Generic;

namespace Blogifier.Blogs;

public class MainData
{
  public MainData(BlogData blogData, IEnumerable<CategoryItem> categories)
  {
    Title = blogData.Title;
    Description = blogData.Description;
    Logo = blogData.Logo;
    Theme = blogData.Theme;
    HeaderScript = blogData.HeaderScript;
    FooterScript = blogData.FooterScript;
    Version = blogData.Version;
    ItemsPerPage = blogData.ItemsPerPage;
    Categories = categories;
  }

  public MainData(BlogData blogData, IEnumerable<CategoryItem> categories, string? absoluteUrl, BlogifierClaims? claims) : this(blogData, categories)
  {
    AbsoluteUrl = absoluteUrl;
    Claims = claims;
  }

  public string Title { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string? Logo { get; set; }
  public string Theme { get; set; } = default!;
  public string? HeaderScript { get; set; }
  public string? FooterScript { get; set; }
  public string Version { get; set; } = default!;
  public int ItemsPerPage { get; set; }
  public IEnumerable<CategoryItem> Categories { get; set; } = default!;
  public string? AbsoluteUrl { get; set; }
  public BlogifierClaims? Claims { get; set; }
}
