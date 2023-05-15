namespace Blogifier.Identity;

public static class AppClaimTypes
{
  public const string UserId = "sub";
  public const string SecurityStamp = "ses";
  public const string UserName = "name";
  public const string Email = "email";
  public const string NickName = "nck";
  public const string Avatar = "ava";
  public const string Gender = "gen";
  public const string Authority = "aut";
  public const string AuthorityYes = "y";
  public const string AuthorityAdmin = $"{Authority}:adm";
}
