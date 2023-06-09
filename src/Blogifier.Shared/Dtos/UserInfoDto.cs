namespace Blogifier.Shared;

public class UserInfoDto
{
  public int Id { get; set; }
  public string Email { get; set; } = default!;
  public string UserName { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
  public UserType Type { get; set; }
}
