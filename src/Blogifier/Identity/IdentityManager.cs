using System.Security.Claims;

namespace Blogifier.Identity;

public class IdentityManager
{
  public IIdentityUser? GetIdentityUser(ClaimsPrincipal user)
  {
    if (user.Identity == null || !user.Identity.IsAuthenticated)
      return null;
    var userInfo = new UserInfo();
    foreach (var claim in user.Claims)
    {
      switch (claim.Type)
      {
        case IIdentityUser.ClaimTypes.UserId:
          userInfo.Id = int.Parse(claim.Value); break;
        case IIdentityUser.ClaimTypes.SecurityStamp:
          userInfo.SecurityStamp = claim.Value; break;
        case IIdentityUser.ClaimTypes.UserName:
          userInfo.UserName = claim.Value; break;
        case IIdentityUser.ClaimTypes.NickName:
          userInfo.NickName = claim.Value; break;
        case IIdentityUser.ClaimTypes.Avatar:
          userInfo.Avatar = claim.Value; break;
        case IIdentityUser.ClaimTypes.Gender:
          userInfo.Gender = claim.Value; break;
      }
    }
    return userInfo;
  }
}
