using Blogifier.Shared;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Blogifier.Identity;

public class BlogifierClaims
{
  public string UserId { get; set; } = default!;
  public string UserName { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Email { get; set; }
  public string? Avatar { get; set; }
  public UserType Type { get; set; }
  public static BlogifierClaims? Analysis(ClaimsPrincipal principal)
  {
    if (principal.Identity == null || !principal.Identity.IsAuthenticated)
      return null;

    var user = new BlogifierClaims();
    foreach (var claim in principal.Claims)
    {
      switch (claim.Type)
      {
        case BlogifierClaimTypes.UserId:
          user.UserId = claim.Value; break;
        case BlogifierClaimTypes.UserName:
          user.UserName = claim.Value; break;
        case BlogifierClaimTypes.Email:
          user.Email = claim.Value; break;
        case BlogifierClaimTypes.NickName:
          user.NickName = claim.Value; break;
        case BlogifierClaimTypes.Avatar:
          user.Avatar = claim.Value; break;
        case BlogifierClaimTypes.Type:
          user.Type = (UserType)Enum.Parse(typeof(UserType), claim.Value); break;
        default:
          {
            break;
          }
      }
    }
    return user;
  }

  public static ClaimsPrincipal Generate(BlogifierClaims? identity)
  {
    if (identity != null)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, identity.UserName),
        new Claim(BlogifierClaimTypes.UserId, identity.UserId),
        new Claim(BlogifierClaimTypes.UserName, identity.UserName),
        new Claim(BlogifierClaimTypes.NickName, identity.NickName),
        new Claim(BlogifierClaimTypes.Type, ((int)identity.Type).ToString()),
      };
      if (!string.IsNullOrEmpty(identity.Email)) claims.Add(new Claim(BlogifierClaimTypes.Email, identity.Email));
      return new ClaimsPrincipal(new ClaimsIdentity(claims, "identity"));
    }
    return new ClaimsPrincipal(new ClaimsIdentity());
  }
}
