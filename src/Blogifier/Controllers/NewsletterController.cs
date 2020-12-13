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
		protected readonly ISubscriberProvider _subscriberProvider;

		public NewsletterController(ISubscriberProvider subscriberProvider)
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

		[HttpDelete("remove/{id:int}")]
		public async Task<ActionResult<bool>> RemoveCategory(int id)
		{
			return await _subscriberProvider.RemoveSubscriber(id);
		}
	}
}
