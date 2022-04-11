using Blogifier.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Blogifier
{
    public class BlogifierServerAuthStateProvider : AuthenticationStateProvider

    {
        private readonly IHttpContextAccessor _contextAccessor;

        public BlogifierServerAuthStateProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = _contextAccessor.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                return await Task.FromResult(new AuthenticationState(user));
            }
            else
            {
                var identity = new ClaimsIdentity(new[]{
                                    new Claim(ClaimTypes.Name, "AnonymousVisitor"),
                                    }, "AnonymousAuth");

                var fakeUser = new ClaimsPrincipal(identity);
                return new AuthenticationState(fakeUser);
            }

        }
    }
}
