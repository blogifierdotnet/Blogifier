namespace Blogifier.Shared;

public class PostCategory
{
  public int PostId { get; set; }
  public Post Post { get; set; } = default!;
  public int CategoryId { get; set; }
  public Category Category { get; set; } = default!;
}
