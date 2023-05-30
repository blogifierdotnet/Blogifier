namespace Blogifier.Shared;

public class CategoryItemDto
{
  public int Id { get; set; }
  public string Category { get; set; } = default!;
  public string? Description { get; set; }
  public int PostCount { get; set; }
}
