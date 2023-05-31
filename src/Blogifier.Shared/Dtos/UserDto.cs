namespace Blogifier.Shared;

public class UserDto
{
  public string Id { get; set; } = default!;
  public string Email { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
}
