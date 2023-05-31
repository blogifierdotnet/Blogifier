namespace Blogifier.Shared;

public class BlogEitorDto
{
  public string Title { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string? HeaderScript { get; set; }
  public string? FooterScript { get; set; }
  public int ItemsPerPage { get; set; }
  public bool IncludeFeatured { get; set; }
}
