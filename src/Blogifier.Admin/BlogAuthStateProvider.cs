using Blogifier.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blogifier.Admin;

public class BlogAuthStateProvider : AuthenticationStateProvider
{
  private readonly ILogger _logger;
  protected readonly HttpClient _httpClient;
  protected AuthenticationState? _state;

  public BlogAuthStateProvider(ILogger<BlogAuthStateProvider> logger, HttpClient httpClient)
  {
    _logger = logger;
    _httpClient = httpClient;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    if (_state == null)
    {
      var response = await _httpClient.GetAsync("/api/token/userinfo");
      BlogifierClaims? claims = null;
      if (response.IsSuccessStatusCode)
      {
        var stream = await response.Content.ReadAsStreamAsync();
        if (stream.Length > 0)
        {
          claims = JsonSerializer.Deserialize<BlogifierClaims>(stream, BlogifierSharedConstant.DefaultJsonSerializerOptions)!;
          _logger.LogInformation("claims success userName:{UserName}", claims.UserName);
        }
      }
      else
      {
        _logger.LogError("claims http error StatusCode:{StatusCode}", response.StatusCode);
      }
      var principal = BlogifierClaims.Generate(claims);
      _state = new AuthenticationState(principal);
    }
    return _state;
  }
}
