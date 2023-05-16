using Blogifier.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
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
      var identity = await client.GetFromJsonAsync<BlogifierClaims>("/api/user/identity");
      var principal = BlogifierClaims.Generate(identity);
      _state = new AuthenticationState(principal);
    }
    return _state;
  }
}
