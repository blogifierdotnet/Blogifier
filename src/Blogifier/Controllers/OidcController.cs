using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System;

namespace Blogifier.Controllers
{
    public class OidcController : ControllerBase
    {
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ExternalSignIn(string path = "")
        {
            System.Console.WriteLine("New Controller here!");

            var domain = $"{Request.Scheme}://{Request.Host}";
            var absolutePath = string.Equals(path, "") ? domain : domain + path;
            var returnUri = new Uri(absolutePath);
            return await Task.FromResult(Challenge(BuildAuthenticationProperties(returnUri), "oidc"));
        }
        private AuthenticationProperties BuildAuthenticationProperties(Uri returnUri)
        {
            var authenticationProperties = new AuthenticationProperties();
            if (returnUri != null)
            {
                if (string.Equals(base.Request.Host.Host, returnUri.Host, StringComparison.OrdinalIgnoreCase))
                {
                    authenticationProperties.RedirectUri = returnUri.ToString();
                }
            }
            return authenticationProperties;
        }
    }
}