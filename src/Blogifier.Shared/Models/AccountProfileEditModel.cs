namespace Blogifier.Shared;

public class AccountProfileEditModel: AccountProfileModel
{
  public UserDto User { get; set; } = default!;
}
