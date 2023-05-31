namespace Blogifier.Shared;

public class AccountProfileEditModel: AccountProfileModel
{
  public string Email { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
}
