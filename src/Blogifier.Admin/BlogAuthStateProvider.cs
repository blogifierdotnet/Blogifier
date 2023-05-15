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
  private readonly IHttpClientFactory _httpClientFactory;

  private AuthenticationState? _state;

  public BlogAuthStateProvider(IHttpClientFactory httpClientFactory)
  {
    _httpClientFactory = httpClientFactory;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    if (_state == null)
    {
      var client = _httpClientFactory.CreateClient();
      var identity = await client.GetFromJsonAsync<IdentityUserDto>("/api/user/identity");
      if (identity != null)
      {
        var claims = new List<Claim>
        {
          new Claim(AppClaimTypes.UserId, identity.Id.ToString()),
          new Claim(AppClaimTypes.SecurityStamp, identity.SecurityStamp),
          new Claim(AppClaimTypes.UserName, identity.UserName),
          new Claim(AppClaimTypes.NickName, identity.NickName)
        };
        if (!string.IsNullOrEmpty(identity.Email)) claims.Add(new Claim(AppClaimTypes.Email, identity.Email));
        _state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "identity")));
      }
      else
      {
        _state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
      }
    }
    return _state;
  }
}
