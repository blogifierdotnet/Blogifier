using System;

namespace Blogifier.Identity;

public interface IIdentityUser
{
  public static class ClaimTypes
  {
    public const string UserId = "sub";
    public const string SecurityStamp = "ses";
    public const string UserName = "name";
    public const string Avatar = "ava";
    public const string Nickname = "nck";
    public const string Gender = "gen";
  }
  public DateTime CreatedAt { get; set; }
  public string Nickname { get; set; }
  public string? Avatar { get; set; }
  public string? Bio { get; set; }
  public string? Gender { get; set; }
}
