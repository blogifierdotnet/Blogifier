using System;

namespace Blogifier.Shared;

public class NewsletterDto
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public int PostId { get; set; }
  public PostItemDto? Post { get; set; }
  public bool Success { get; set; }
}
