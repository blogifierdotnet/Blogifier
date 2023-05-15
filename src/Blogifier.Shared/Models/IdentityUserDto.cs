namespace Blogifier.Models;

public class IdentityUserDto
{
  public string UserId { get; set; } = default!;
  public string UserName { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Email { get; set; }
  public string? Avatar { get; set; }
  public string? Gender { get; set; }
}
