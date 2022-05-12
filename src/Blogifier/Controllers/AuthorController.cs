using System.Net.Http;
using System.Linq;
using Blogifier.Core.Providers;
using Blogifier.Shared;
using Blogifier.Shared.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;

namespace Blogifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IStorageProvider _storageProvider;

        public AuthorController(IAuthorProvider authorProvider, IStorageProvider storageProvider)
        {
            _authorProvider = authorProvider;
            _storageProvider = storageProvider;
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

        [HttpGet("partialinfobyguid/{guid}")]
        public async Task<ActionResult<CommentUserModel>> FindByGuid(string guid)
        {
            var author = await _authorProvider.FindByOpenId(guid);

            return new CommentUserModel()
            {
                Avatar = author.Avatar,
                Name = author.DisplayName,
                Email = author.Email,
                IsAdmin = author.IsAdmin
            };
        }

        [HttpGet("getcurrent")]
        public async Task<ActionResult<Author>> GetCurrentAuthor()
        {
            Console.WriteLine("--------Current Author was Called!-----------");


            if (User.Identity.IsAuthenticated)
            {
                var tempAuthor = CreateFromOIDC();
                tempAuthor.Avatar = tempAuthor.Avatar.VerifyAvatar();

                if (User.HasClaim("role", "AutoBloger"))
                {
                    tempAuthor.IsAdmin = true;
                }

                // Sync with local DB on Bio firstly
                var existingUser = await _authorProvider.FindByOpenId(User.FindFirstValue(JwtClaimTypes.Subject));
                existingUser.Avatar = existingUser.Avatar.VerifyAvatar();

                if (existingUser is null)
                {
                    Console.WriteLine("Has no local data, need to Sync!");
                    await SyncAuthorWithDB(tempAuthor);
                    var avatarResult = await _storageProvider.SyncAvatarFromWeb(new Uri("https://auth.prime-minister.pub/images/user_avatars/" + tempAuthor.Avatar + ".png"), "/");
                    return tempAuthor;
                }

                else
                {
                    // Sync with Avatar/Name/Email
                    var oldAvatarName = existingUser.Avatar;
                    if (existingUser.DisplayName != tempAuthor.DisplayName || existingUser.Email != tempAuthor.Email || existingUser.Avatar != tempAuthor.Avatar)
                    {
                        System.Console.WriteLine("----Update Profile----");
                        tempAuthor.Bio = existingUser.Bio;
                        await _authorProvider.Update(tempAuthor);
                    }

                    if (oldAvatarName != tempAuthor.Avatar)
                    {
                        System.Console.WriteLine("----Update Avatar----");
                        var avatarResult = await _storageProvider.SyncAvatarFromWeb(new Uri("https://auth.prime-minister.pub/images/user_avatars/" + tempAuthor.Avatar + ".png"), "/");
                        await _storageProvider.DeleteOldAvatar(oldAvatarName);
                    }
                }
                tempAuthor.Bio = existingUser.Bio;
                System.Console.WriteLine("Bio here----");
                System.Console.WriteLine(existingUser.Bio);
                // var addressedAvatar = "/data/Avatar/" + tempAuthor.Avatar + ".png";
                // tempAuthor.Avatar = addressedAvatar;
                return tempAuthor;
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
            var tempAutor = await _authorProvider.FindByOpenId(author.OpenGuid);
            if (tempAutor is null)
            {
                return await _authorProvider.CreateFromAuthor(author) ? Ok() : BadRequest();
            }
            System.Console.WriteLine("Bio here--------");
            System.Console.WriteLine(author.Bio);
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
            var tempGuid = User.FindFirstValue(JwtClaimTypes.Subject);
            var tempEmail = User.FindFirstValue(JwtClaimTypes.Email);
            var tempName = User.FindFirstValue(JwtClaimTypes.Name);
            var tempAvatar = User.FindFirstValue(JwtClaimTypes.Picture);

            tempAuthor.Avatar = tempAvatar;
            tempAuthor.DisplayName = tempName;
            tempAuthor.Email = tempEmail;
            tempAuthor.OpenGuid = tempGuid;
            tempAuthor.IsAdmin = false;

            return tempAuthor;
        }

        [Authorize]
        protected async Task<bool> SyncAuthorWithDB(Author author)
        {
            Author authorToSync = author;
            authorToSync.Bio = "Update Bio/更新简介";
            authorToSync.DateCreated = DateTime.UtcNow;
            return await _authorProvider.CreateFromAuthor(authorToSync);
        }


    }
}
