using Blogifier.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogifier.Admin
{
	public class BlogAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly HttpClient _httpClient;

		public BlogAuthenticationStateProvider(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			Author author = await _httpClient.GetFromJsonAsync<Author>("api/author/getcurrent");

			if (author != null && author.Email != null)
			{
				var claim = new Claim(ClaimTypes.Name, author.Email);
				var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

				return new AuthenticationState(claimsPrincipal);
			}
			else
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}
	}
}
