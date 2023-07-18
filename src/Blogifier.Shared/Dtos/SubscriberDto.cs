using System;

namespace Blogifier.Shared;

public class SubscriberDto
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public string Email { get; set; } = default!;
  public string? Ip { get; set; }
  public string? Country { get; set; }
  public string? Region { get; set; }
}
