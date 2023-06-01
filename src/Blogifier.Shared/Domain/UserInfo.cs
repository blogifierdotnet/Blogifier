using Blogifier.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Identity;

public class UserInfo : IdentityUser<string>
{
  public UserInfo() : base()
  {

  }

  public UserInfo(string userName) : base()
  {
    Id = Guid.NewGuid().ToString();
    UserName = userName;
  }

  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [StringLength(256)]
  public string NickName { get; set; } = default!;
  [StringLength(1024)]
  public string? Avatar { get; set; }
  [StringLength(2048)]
  public string? Bio { get; set; }
  [StringLength(32)]
  public string? Gender { get; set; }
  public UserType Type { get; set; }
  public UserState State { get; set; }
}
