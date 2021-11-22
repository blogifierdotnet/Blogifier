using System.Linq;
using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
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
                // return await FindByEmail(User.FindFirstValue(ClaimTypes.Name));
                return await new Task<Author>(() => CreateFromOIDC());
            }
            return new Author();
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
            await HttpContext.SignOutAsync();
            return await Task.FromResult(true);
        }

        [Authorize]
        [HttpPut("changepassword")]
        public async Task<ActionResult<bool>> ChangePassword(RegisterModel model)
        {
            var success = await _authorProvider.ChangePassword(model);
            return success ? Ok() : BadRequest();
        }

        [HttpGet("getcurrent")]
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
