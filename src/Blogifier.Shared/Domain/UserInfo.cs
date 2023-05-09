using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Identity;

public class UserInfo : IdentityUser<int>
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
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [StringLength(256)]
  public string Nickname { get; set; } = default!;
  [StringLength(1024)]
  public string? Avatar { get; set; }
  [StringLength(2048)]
  public string? Bio { get; set; }
  [StringLength(32)]
  public string? Gender { get; set; }
}
