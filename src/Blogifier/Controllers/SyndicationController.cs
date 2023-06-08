using Blogifier.Core.Data;
using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SyndicationController : ControllerBase
	{
		private readonly AppDbContext _dbContext;
		private readonly ISyndicationProvider _syndicationProvider;

		public SyndicationController(AppDbContext dbContext, ISyndicationProvider syndicationProvider)
		{
			_dbContext = dbContext;
			_syndicationProvider = syndicationProvider;
		}

		[Authorize]
		[HttpGet("getitems")]
		public async Task<List<Post>> GetItems(string feedUrl, string baseUrl)
		{
			Author author = await _dbContext.Authors
				.Where(a => a.Email == User.Identity.Name)
				.FirstOrDefaultAsync();

			string webRoot = Url.Content("~/");

			return await _syndicationProvider.GetPosts(feedUrl, author.Id, new System.Uri(baseUrl), webRoot);
		}

		[Authorize]
		[HttpPost("import")]
		public async Task<ActionResult<bool>> Import(Post post)
		{
			var success = await _syndicationProvider.ImportPost(post);
			return success ? Ok() : BadRequest();

			//Random rnd = new Random();
			//var ok = rnd.Next(1, 10) >= 2;

			//System.Threading.Thread.Sleep(1000);

			//if(ok)
			//	return await Task.FromResult(Ok());
			//else
			//	return await Task.FromResult(BadRequest());
		}
	}
}
