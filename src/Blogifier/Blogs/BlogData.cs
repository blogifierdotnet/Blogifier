namespace Blogifier.Blogs;

public class BlogData
{
  public string Title { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string? Logo { get; set; }
  public string Theme { get; set; } = default!;
  public string? HeaderScript { get; set; }
  public string? FooterScript { get; set; }
  public string Version { get; set; } = default!;
  public int ItemsPerPage { get; set; }
  public bool IncludeFeatured { get; set; }
}
