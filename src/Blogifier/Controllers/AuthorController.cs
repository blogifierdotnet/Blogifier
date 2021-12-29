using System.Linq;
using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using System.Threading.Tasks;
using System;

namespace Blogifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorProvider _authorProvider;

        public AuthorController(IAuthorProvider authorProvider)
        {
            _authorProvider = authorProvider;
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<List<Author>> All()
        {
            return await _authorProvider.GetAuthors();
        }

        [Authorize]
        [HttpGet("email/{email}")]
        public async Task<ActionResult<Author>> FindByEmail(string email)
        {
            return await _authorProvider.FindByEmail(email);
        }

        [HttpGet("getcurrent")]
        public async Task<ActionResult<Author>> GetCurrentAuthor()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var claim in User.Claims)
                {
                    Console.WriteLine("{0} ===> {1}", claim.Type, claim.Value);
                }
                // var tempAvatar = User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Picture).Value;
                var tempEmail = User.FindFirstValue(JwtClaimTypes.Email);
                var tempName = User.FindFirstValue(JwtClaimTypes.Name);
                var tempAvatar = User.FindFirstValue(JwtClaimTypes.Picture);
                var result = await FindByEmail(tempEmail);
                var tempAuthor = result.Value;
                // Sync Author to local DB if Admin role status not match local DB
                if (User.HasClaim("role", "AutoBloger"))
                {
                    Console.WriteLine("Yes, A Bolger Here");
                    if (tempAuthor is null)
                    { await _authorProvider.CreateFromOIDC(User); }
                    tempAuthor.DisplayName = tempName;
                    tempAuthor.Avatar = "https://auth.prime-minister.pub/images/user_avatars/" + tempAvatar + ".png";
                    return tempAuthor;
                }
                else
                {
                    if (await _authorProvider.ExistByOIDC(tempEmail))
                    { await _authorProvider.RemoveByOIDC(tempEmail); }
                    return new Author() { DisplayName = tempName };
                }
            }
            return new Author() { DisplayName = "Visitor" };
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> RemoveAuthor(int id)
        {
            return await _authorProvider.Remove(id);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<bool>> Add(Author author)
        {
            var success = await _authorProvider.Add(author);
            return success ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<bool>> Update(Author author)
        {
            var success = await _authorProvider.Update(author);
            return success ? Ok() : BadRequest();
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(RegisterModel model)
        {
            var success = await _authorProvider.Register(model);
            return success ? Ok() : BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (await _authorProvider.Verify(model) == false)
                return BadRequest();

            var claim = new Claim(ClaimTypes.Name, model.Email);
            var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);
            return Ok();
        }

        [HttpGet("logout")]
        public async Task<ActionResult<bool>> LogOutUser()
        {
            await HttpContext.SignOutAsync("cookie");
            // return await Task.FromResult(true);
            // await Task.FromResult(SignOut("cookie", "oidc"));
            return await Task.FromResult(true);
        }

        [Authorize]
        [HttpPut("changepassword")]
        public async Task<ActionResult<bool>> ChangePassword(RegisterModel model)
        {
            var success = await _authorProvider.ChangePassword(model);
            return success ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpGet]
        protected Author CreateFromOIDC()
        {
            var tempAuthor = new Author();
            tempAuthor.Avatar =
                "https://auth.prime-minister.pub/images/user_avatars/" +
                User.Claims.FirstOrDefault(claim => claim.Type == "avatar").Value + ".png";

            tempAuthor.DisplayName = User.Claims.FirstOrDefault(claim => claim.Type == "name").Value;
            tempAuthor.Email = User.Claims.FirstOrDefault(claim => claim.Type == "email").Value;
            Console.WriteLine(tempAuthor.Avatar);
            Console.WriteLine(tempAuthor.DisplayName);
            Console.WriteLine(tempAuthor.Email);
            return tempAuthor;
        }
    }
}
