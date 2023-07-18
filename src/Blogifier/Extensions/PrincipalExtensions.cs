using System;
using System.Security.Claims;

namespace Blogifier.Extensions;

public static class PrincipalExtensions
{
  public static string FirstValue(this ClaimsPrincipal principal, string claimType)
  {
    var value = FirstOrDefault(principal, claimType);
    if (value == null) throw new NullReferenceException(nameof(value));
    return value;
  }

  public static string? FirstOrDefault(this ClaimsPrincipal principal, string claimType)
    => principal.FindFirstValue(claimType);
}
