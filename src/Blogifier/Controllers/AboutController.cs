using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AboutController : ControllerBase
	{
		private readonly IAboutProvider _aboutProvider;

		public AboutController(IAboutProvider aboutProvider)
		{
			_aboutProvider = aboutProvider;
		}

		[HttpGet]
		public async Task<AboutModel> GetAbout()
		{
			return await _aboutProvider.GetAboutModel();
		}
	}
}
