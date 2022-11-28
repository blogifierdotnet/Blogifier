using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorProvider _authorProvider;
		private readonly IEmailProvider _emailProvider;

		public AuthorController(
			IEmailProvider emailProvider,
			IAuthorProvider authorProvider
			)
		{
			_emailProvider = emailProvider;
			_authorProvider = authorProvider;
		}

		[Authorize]
		[HttpGet("all")]
		public async Task<List<Author>> All()
		{
			var currentUser = await _authorProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
			var authors = await _authorProvider.GetAuthors();
			if(!currentUser.IsAdmin)
			{
				authors = authors.Where(x => x.Email == currentUser.Email).ToList();
			}
			return authors;
		}

		[Authorize]
		[HttpGet("email/{email}")]
		public async Task<ActionResult<Author>> FindByEmail(string email)
		{
			var currentUser = await _authorProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
			if(!currentUser.IsAdmin)
			{
				return currentUser;
			}
			return await _authorProvider.FindByEmail(email);
		}

		[HttpGet("getcurrent")]
		public async Task<ActionResult<Author>> GetCurrentAuthor()
		{
			if (User.Identity.IsAuthenticated)
			{
				bool isEmailEnabled = await _authorProvider.IsEmailEnabled();
				var currentUser = await _authorProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
				if(isEmailEnabled && currentUser.Verified == null){
					return new Author();
				}
				return currentUser;
			}
			return new Author();
		}

		[Authorize]
		[HttpDelete("{id:int}")]
		public async Task<ActionResult<bool>> RemoveAuthor(int id)
		{
			var currentUser = await _authorProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
			if(!currentUser.IsAdmin)
			{
				return false;
			}
			return await _authorProvider.Remove(id);
		}

		[Authorize]
		[HttpPost("add")]
		public async Task<ActionResult<bool>> Add(Author author)
		{
			var currentUser = await _authorProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
			if(!currentUser.IsAdmin)
			{
				return false;
			}
			var success = await _authorProvider.Add(author);
			return success ? Ok() : BadRequest();
		}

		[Authorize]
		[HttpPut("update")]
		public async Task<ActionResult<bool>> Update(Author author)
		{
			var currentUser = await _authorProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
			if(!currentUser.IsAdmin)
			{
				return false;
			}
			var success = await _authorProvider.Update(author);
			return success ? Ok() : BadRequest();
		}

		[HttpPost("register")]
		public async Task<ActionResult<bool>> Register(RegisterModel model)
		{
			var success = await _authorProvider.Register(model);
			
			if(success)
			{
				// send email
				var author = await _authorProvider.FindByEmail(model.Email);
				if(author != null)
				{
					string webRoot = Url.Content("~/");
					var origin = $"{Request.Scheme}s://{Request.Host}{webRoot}";
					await _emailProvider.SendVerificationEmail(author, origin);
				}
			}
        	
			return success ? Ok() : BadRequest();
		}

		[AllowAnonymous]
		[HttpPost("verify-email")]
		public async Task<ActionResult<bool>> VerifyEmail(VerifyEmailRequest model)
		{
			var userEmail = await _authorProvider.ValidateCurrentToken(model.Token);
			if(userEmail != null)
			{
				var currentUser = await _authorProvider.FindByEmail(userEmail);
				currentUser.VerificationToken = null;
				currentUser.Verified = DateTime.UtcNow;
				var success = await _authorProvider.Update(currentUser);
				return success ? Ok() : BadRequest();
			}
			else
			{
				return BadRequest();
			}
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
