namespace Blogifier.Shared;

public class PostBriefDto
{
  public int Id { get; set; }
  public string Title { get; set; } = default!;
  public string Slug { get; set; } = default!;
}
