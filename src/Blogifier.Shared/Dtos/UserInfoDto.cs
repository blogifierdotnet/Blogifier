namespace Blogifier.Shared;

public class UserInfoDto
{
  public string Id { get; set; } = default!;
  public string Email { get; set; } = default!;
  public string UserName { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
  public UserType Type { get; set; }
}
