using System.Security.Claims;

namespace Blogifier.Identity;

public class IdentityManager
{
  public UserInfo? GetIdentityUser(ClaimsPrincipal user)
  {
    if (user.Identity == null || !user.Identity.IsAuthenticated)
      return null;
    var userInfo = new UserInfo();
    foreach (var claim in user.Claims)
    {
      switch (claim.Type)
      {
        case AppClaimTypes.UserId:
          userInfo.Id = int.Parse(claim.Value); break;
        case AppClaimTypes.SecurityStamp:
          userInfo.SecurityStamp = claim.Value; break;
        case AppClaimTypes.UserName:
          userInfo.UserName = claim.Value; break;
        case AppClaimTypes.Email:
          userInfo.Email = claim.Value; break;
        case AppClaimTypes.NickName:
          userInfo.NickName = claim.Value; break;
        case AppClaimTypes.Avatar:
          userInfo.Avatar = claim.Value; break;
        case AppClaimTypes.Gender:
          userInfo.Gender = claim.Value; break;
      }
    }
    return userInfo;
  }
}
