using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System;

namespace Blogifier.Controllers
{
    public class OidcController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ExternalSignIn(string path = "")
        {
            System.Console.WriteLine("New Controller here!");
            var domain = $"{Request.Scheme}://{Request.Host}";
            var absolutePath = String.IsNullOrEmpty(path) ? domain : domain + path;
            // System.Console.WriteLine(path);
            // System.Console.WriteLine(absolutePath);
            // var returnUri = new Uri(WebUtility.UrlEncode(absolutePath), UriKind.Absolute);
            // var returnUri = new Uri(WebUtility.UrlEncode(absolutePath), UriKind.Absolute);
            var returnUri = new Uri(new Uri(domain), new Uri(path, UriKind.Relative));
            return await Task.FromResult(Challenge(BuildAuthenticationProperties(returnUri), "oidc"));
        }
        private AuthenticationProperties BuildAuthenticationProperties(Uri returnUri)
        {
            var authenticationProperties = new AuthenticationProperties();
            if (returnUri != null)
            {
                if (string.Equals(base.Request.Host.Host, returnUri.Host, StringComparison.OrdinalIgnoreCase))
                {
                    authenticationProperties.RedirectUri = returnUri.AbsolutePath;
                }
            }
            return authenticationProperties;
        }
    }
}