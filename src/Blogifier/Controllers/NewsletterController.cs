using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NewsletterController : ControllerBase
	{
		protected readonly INewsletterProvider _newsletterProvider;

		public NewsletterController(INewsletterProvider newsletterProvider)
		{
			_newsletterProvider = newsletterProvider;
		}

		[HttpPut("subscribe")]
		public async Task<ActionResult<bool>> Subscribe([FromBody] Subscriber subscriber)
		{
			return await _newsletterProvider.AddSubscriber(subscriber);
		}

		[HttpGet("subscribers")]
		public async Task<List<Subscriber>> GetSubscribers()
		{
			return await _newsletterProvider.GetSubscribers();
		}

		[HttpDelete("unsubscribe/{id:int}")]
		public async Task<ActionResult<bool>> RemoveSubscriber(int id)
		{
			return await _newsletterProvider.RemoveSubscriber(id);
		}


		[HttpGet("newsletters")]
		public async Task<List<Newsletter>> GetNewsletters()
		{
			return await _newsletterProvider.GetNewsletters();
		}

		[HttpGet("send/{postId:int}")]
		public async Task<bool> SendNewsletter(int postId)
		{
			return await _newsletterProvider.SendNewsletter(postId);
		}

		[HttpDelete("remove/{id:int}")]
		public async Task<ActionResult<bool>> RemoveNewsletter(int id)
		{
			return await _newsletterProvider.RemoveNewsletter(id);
		}


		[HttpGet("mailsettings")]
		public async Task<MailSetting> GetMailSettings()
		{
			return await _newsletterProvider.GetMailSettings();
		}

		[HttpPut("mailsettings")]
		public async Task<ActionResult<bool>> SaveMailSettings([FromBody] MailSetting mailSettings)
		{
			return await _newsletterProvider.SaveMailSettings(mailSettings);
		}
	}
}
