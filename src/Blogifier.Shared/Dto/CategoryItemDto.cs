namespace Blogifier.Shared;

public class CategoryItemDto
{
  public string Category { get; set; } = default!;
  public string? Description { get; set; }
  public int PostCount { get; set; }
}
