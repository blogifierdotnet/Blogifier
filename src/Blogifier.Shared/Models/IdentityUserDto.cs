using System;

namespace Blogifier.Models;

public class IdentityUserDto
{
  public int Id { get; set; }
  public string SecurityStamp { get; set; } = default!;
  public string UserName { get; set; } = string.Empty;
  public string? Email { get; set; }
  public DateTime CreatedAt { get; set; }
  public string NickName { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
  public string? Gender { get; set; }
}
