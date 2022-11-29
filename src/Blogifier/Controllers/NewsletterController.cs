using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NewsletterController : ControllerBase
	{
		protected readonly INewsletterProvider _newsletterProvider;
		private readonly IAuthorProvider _authorProvider;
		protected readonly IEmailProvider _emailProvider;
		public NewsletterController(
			IEmailProvider emailProvider,
			IAuthorProvider authorProvider,
			INewsletterProvider newsletterProvider
			)
		{
			_emailProvider = emailProvider;
			_authorProvider = authorProvider;
			_newsletterProvider = newsletterProvider;
		}

		[HttpPost("subscribe")]
		public async Task<ActionResult<bool>> Subscribe([FromBody] Subscriber subscriber)
		{
			return await _newsletterProvider.AddSubscriber(subscriber);
		}

		[Authorize]
		[HttpGet("subscribers")]
		public async Task<List<Subscriber>> GetSubscribers()
		{
			return await _newsletterProvider.GetSubscribers();
		}

		[Authorize]
		[HttpDelete("unsubscribe/{id:int}")]
		public async Task<ActionResult<bool>> RemoveSubscriber(int id)
		{
			return await _newsletterProvider.RemoveSubscriber(id);
		}

		[AllowAnonymous]
		[HttpPost("unsubscribe")]
		public async Task<ActionResult<bool>> RemoveSubscriber(UnsubscribeRequest model)
		{
			var userEmail = await _authorProvider.ValidateCurrentToken(model.Token);
			if(userEmail != null)
			{
				return await _newsletterProvider.RemoveSubscriber(userEmail);
			}
			else
			{
				return BadRequest();
			}
		}
		
		[Authorize]
		[HttpGet("newsletters")]
		public async Task<List<Newsletter>> GetNewsletters()
		{
			return await _newsletterProvider.GetNewsletters();
		}

		[Authorize]
		[HttpGet("send/{postId:int}")]
		public async Task<bool> SendNewsletter(int postId)
		{
			string webRoot = Url.Content("~/");
            var origin = $"{Request.Scheme}s://{Request.Host}{webRoot}";
			return await _newsletterProvider.SendNewsletter(postId, origin);
		}

		[Authorize]
		[HttpDelete("remove/{id:int}")]
		public async Task<ActionResult<bool>> RemoveNewsletter(int id)
		{
			return await _newsletterProvider.RemoveNewsletter(id);
		}

		[Authorize]
		[HttpGet("mailsettings")]
		public async Task<MailSetting> GetMailSettings()
		{
			var currentUser = await _authorProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
			if(!currentUser.IsAdmin)
			{
				return new MailSetting();
			}
			return await _newsletterProvider.GetMailSettings();
		}

		[Authorize]
		[HttpPut("mailsettings")]
		public async Task<ActionResult<bool>> SaveMailSettings([FromBody] MailSetting mailSettings)
		{
			var currentUser = await _authorProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
			if(!currentUser.IsAdmin)
			{
				return false;
			}
			
			var success = await _newsletterProvider.SaveMailSettings(mailSettings);
			if(success && mailSettings.Enabled)
			{
				string webRoot = Url.Content("~/");
            	var origin = $"{Request.Scheme}s://{Request.Host}{webRoot}";
				//do not turn this on if it doesn't work
				if(!await _emailProvider.SendVerificationEmail(currentUser,origin)){
					mailSettings.Enabled = false;
					success = await _newsletterProvider.SaveMailSettings(mailSettings);
				}
			}
			return success;
		}
	}
}
