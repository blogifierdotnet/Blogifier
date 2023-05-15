using Blogifier.Identity;
using Blogifier.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogifier.Admin;

public class BlogAuthStateProvider : AuthenticationStateProvider
{
  private readonly HttpClient _httpClient;

  public BlogAuthStateProvider(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    var identity = await _httpClient.GetFromJsonAsync<IdentityUserDto>("api/user/identity");
    var claims = new List<Claim>();
    if (identity != null)
    {
      claims.Add(new Claim(AppClaimTypes.UserId, identity.Id.ToString()));
      claims.Add(new Claim(AppClaimTypes.SecurityStamp, identity.SecurityStamp));
      claims.Add(new Claim(AppClaimTypes.UserName, identity.UserName));
      if (!string.IsNullOrEmpty(identity.Email)) claims.Add(new Claim(AppClaimTypes.Email, identity.Email));
      claims.Add(new Claim(AppClaimTypes.NickName, identity.NickName));
    }
    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "serverAuth")));
  }
}
