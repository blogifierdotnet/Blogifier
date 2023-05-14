using System;

namespace Blogifier.Models;

public class IdentityUserDto
{
  public DateTime CreatedAt { get; set; }
  public string Nickname { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
  public string? Gender { get; set; }
}
