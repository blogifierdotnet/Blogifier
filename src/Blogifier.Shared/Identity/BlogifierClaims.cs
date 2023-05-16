using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Blogifier.Identity;

public class BlogifierClaims
{
  public string UserId { get; set; } = default!;
  public string UserName { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Email { get; set; }
  public string? Avatar { get; set; }
  public string? Gender { get; set; }
  public string? Authority { get; set; }

  public static BlogifierClaims? Analysis(ClaimsPrincipal principal)
  {
    if (principal.Identity == null || !principal.Identity.IsAuthenticated)
      return null;

    var user = new BlogifierClaims();
    var autBuilder = new StringBuilder();
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
        default:
          {
            if (claim.Type.StartsWith(BlogifierClaimTypes.Authority))
              autBuilder.AppendFormat("{0}={1},", claim.Type, claim.Value);
            break;
          }
      }
    }
    if (autBuilder.Length > 0)
    {
      autBuilder.Remove(autBuilder.Length - 1, 1);
      user.Authority = autBuilder.ToString();
    };
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
        new Claim(BlogifierClaimTypes.NickName, identity.NickName)
      };
      if (identity.Authority != null)
      {
        var authorityes = identity.Authority.Split(',');
        foreach (var authority in authorityes)
        {
          var array = authority.Split("=");
          var key = array[0];
          var value = array[1];
          claims.Add(new Claim(key, value));
        }
      }
      if (!string.IsNullOrEmpty(identity.Email)) claims.Add(new Claim(BlogifierClaimTypes.Email, identity.Email));
      return new ClaimsPrincipal(new ClaimsIdentity(claims, "identity"));
    }
    return new ClaimsPrincipal(new ClaimsIdentity());
  }
}
