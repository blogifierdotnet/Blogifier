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
		protected readonly INewsletterProvider _subscriberProvider;

		public NewsletterController(INewsletterProvider subscriberProvider)
		{
			_subscriberProvider = subscriberProvider;
		}

		[HttpPut("subscribe")]
		public async Task<ActionResult<bool>> Subscribe([FromBody] Subscriber subscriber)
		{
			return await _subscriberProvider.AddSubscriber(subscriber);
		}

		[HttpGet("subscribers")]
		public async Task<List<Subscriber>> GetSubscribers()
		{
			return await _subscriberProvider.GetSubscribers();
		}

		[HttpGet("newsletters")]
		public async Task<List<Newsletter>> GetNewsletters()
		{
			return await _subscriberProvider.GetNewsletters();
		}

		[HttpGet("send/{postId:int}")]
		public async Task<bool> SendNewsletter(int postId)
		{
			return await _subscriberProvider.SendNewsletter(postId);
		}

		[HttpDelete("remove/{id:int}")]
		public async Task<ActionResult<bool>> RemoveCategory(int id)
		{
			return await _subscriberProvider.RemoveSubscriber(id);
		}


		[HttpGet("mailsettings")]
		public async Task<MailSetting> GetMailSettings()
		{
			return await _subscriberProvider.GetMailSettings();
		}

		[HttpPut("mailsettings")]
		public async Task<ActionResult<bool>> SaveMailSettings([FromBody] MailSetting mailSettings)
		{
			return await _subscriberProvider.SaveMailSettings(mailSettings);
		}
	}
}
