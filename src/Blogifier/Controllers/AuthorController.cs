using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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
				return await FindByEmail(User.FindFirstValue(ClaimTypes.Name));
			return new Author();
		}

		[Authorize]
		[HttpDelete("{id:int}")]
		public async Task<ActionResult> RemoveAuthor(int id)
		{
			await _authorProvider.RemoveAsync(id);

            return Ok();
		}

		[Authorize]
		[HttpPost("add")]
		public async Task<ActionResult> Add(Author author)
		{
			await _authorProvider.AddAsync(author);

            return Ok();
		}

		[Authorize]
		[HttpPut("update")]
		public async Task<ActionResult> Update(Author author)
		{
            await _authorProvider.UpdateAsync(author);

            return Ok();
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
	}
}
