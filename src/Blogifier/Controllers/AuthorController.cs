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

                var tempAuthor = CreateFromOIDC();
                if (User.HasClaim("role", "AutoBloger"))
                {
                    // Sync with local DB on Bio firstly
                    var tempBio = await _authorProvider.FindByEmail(User.FindFirstValue(JwtClaimTypes.Email));
                    if (tempBio is null)
                    {
                        Console.WriteLine("Has no local data, need to Sync!");
                        await SyncWithDB(tempAuthor);
                    }
                    tempAuthor.IsAdmin = true;
                    var reBio = await _authorProvider.FindByEmail(User.FindFirstValue(JwtClaimTypes.Email));
                    tempAuthor.Id = (tempBio is null) ? 1 : reBio.Id;
                    tempAuthor.Bio = (tempBio is null) ? "Update Bio/更新简介" : reBio.Bio;
                    Console.WriteLine("Bloger ID is " + tempAuthor.Id);
                    return tempAuthor;
                }
                else
                {
                    var tempAuthorToRemove = await _authorProvider.FindByEmail(User.FindFirstValue(JwtClaimTypes.Email));
                    if (tempAuthorToRemove is not null)
                    {
                        Console.WriteLine("Has no Author Cliam, need to Delete!");
                        await _authorProvider.Remove(tempAuthorToRemove.Id);
                    }
                    return tempAuthor;
                }
            }
            return new Author() { DisplayName = "Visitor", IsAdmin = false };
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
            Console.WriteLine("Update API Called!!!");
            var tempAutor = await _authorProvider.FindByEmail(author.Email);
            if (tempAutor is null)
            {
                return await _authorProvider.CreateFromAuthor(author) ? Ok() : BadRequest();
            }
            var success = await _authorProvider.Update(author);
            Console.WriteLine("Update Results: " + success);
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

        // [HttpGet("logout")]
        // public async Task<ActionResult<bool>> LogOutUser()
        // {
        //     await HttpContext.SignOutAsync("oidc");
        //     // await HttpContext.SignOutAsync("oidc");
        //     // return await Task.FromResult(true);
        //     // await Task.FromResult(SignOut("cookie", "oidc"));
        //     return await Task.FromResult(true);
        // }

        // [HttpGet("logout")]
        // public async Task<IActionResult> LogOutUser()
        // {
        //     await HttpContext.SignOutAsync("cookie");
        //     await HttpContext.SignOutAsync("oidc");
        //     return Redirect("~/");
        // }

        [HttpGet("logout")]
        public async Task LogOutUser()
        {
            await HttpContext.SignOutAsync("cookie");
            await HttpContext.SignOutAsync("oidc");
        }

        [Authorize]
        [HttpPut("changepassword")]
        public async Task<ActionResult<bool>> ChangePassword(RegisterModel model)
        {
            var success = await _authorProvider.ChangePassword(model);
            return success ? Ok() : BadRequest();
        }

        [Authorize]
        protected Author CreateFromOIDC()
        {
            var tempAuthor = new Author();
            var tempEmail = User.FindFirstValue(JwtClaimTypes.Email);
            var tempName = User.FindFirstValue(JwtClaimTypes.Name);
            var tempAvatar = User.FindFirstValue(JwtClaimTypes.Picture);

            tempAuthor.Avatar = "https://auth.prime-minister.pub/images/user_avatars/" + tempAvatar + ".png";
            tempAuthor.DisplayName = tempName;
            tempAuthor.Email = tempEmail;
            tempAuthor.IsAdmin = false;

            return tempAuthor;
        }

        [Authorize]
        protected async Task<bool> SyncWithDB(Author author)
        {
            Author authorToSync = author;
            authorToSync.IsAdmin = true;
            authorToSync.Bio = "Update Bio/更新简介";
            authorToSync.DateCreated = DateTime.UtcNow;
            return await _authorProvider.CreateFromAuthor(authorToSync);
        }
    }
}
