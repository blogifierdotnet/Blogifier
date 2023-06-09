using Blogifier.Identity;
using System;
using System.Linq;
using System.Security.Claims;

namespace Blogifier.Shared;

public static class PrincipalExtensions
{
  public static string FirstValue(this ClaimsPrincipal principal, string claimType)
  {
    var claim = principal.Claims.First(m => claimType.Equals(m.Type, StringComparison.OrdinalIgnoreCase));
    return claim.Value;
  }

  public static string? FirstOrDefault(this ClaimsPrincipal principal, string claimType)
  {
    var claim = principal.Claims.FirstOrDefault(m => claimType.Equals(m.Type, StringComparison.OrdinalIgnoreCase));
    return claim?.Value;
  }

  public static int FirstUserId(this ClaimsPrincipal principal)
  {
    var userIdString = FirstValue(principal, BlogifierClaimTypes.UserId);
    return int.Parse(userIdString);
  }
}
