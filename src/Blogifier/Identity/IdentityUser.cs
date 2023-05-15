using Minio.DataModel.ObjectLock;
using System.Security.Claims;

namespace Blogifier.Identity;

public class IdentityUser
{
  public string UserId { get; set; } = default!;
  public string UserName { get; set; } = default!;
  public string NickName { get; set; } = default!;
  public string? Email { get; set; }
  public string? Avatar { get; set; }
  public string? Gender { get; set; }

  public static IdentityUser? Analysis(ClaimsPrincipal principal)
  {
    if (principal.Identity == null || !principal.Identity.IsAuthenticated)
      return null;
    var user = new IdentityUser();
    foreach (var claim in principal.Claims)
    {
      switch (claim.Type)
      {
        case AppClaimTypes.UserId:
          user.UserId = claim.Value; break;
        case AppClaimTypes.UserName:
          user.UserName = claim.Value; break;
        case AppClaimTypes.Email:
          user.Email = claim.Value; break;
        case AppClaimTypes.NickName:
          user.NickName = claim.Value; break;
        case AppClaimTypes.Avatar:
          user.Avatar = claim.Value; break;
        case AppClaimTypes.Gender:
          user.Gender = claim.Value; break;
      }
    }
    return user;
  }
}
