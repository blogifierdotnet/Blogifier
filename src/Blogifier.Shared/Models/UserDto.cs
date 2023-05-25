using System;

namespace Blogifier.Shared;

public class UserDto
{
  public DateTime CreatedAt { get; set; }
  public string NickName { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
  public string? Gender { get; set; }
}
