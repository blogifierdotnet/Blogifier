namespace Blogifier.Shared;

public class CategoryEitorDto
{
  public int Id { get; set; }
  public string Content { get; set; } = default!;
  public string? Description { get; set; }
}
