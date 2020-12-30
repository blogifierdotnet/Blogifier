using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SyndicationController : ControllerBase
	{
		private readonly IRssImportProvider _syndicationProvider;

		public SyndicationController(IRssImportProvider syndicationProvider)
		{
			_syndicationProvider = syndicationProvider;
		}

		[HttpGet("getitems")]
		public async Task<List<Post>> GetItems(string feedUrl)
		{
			SyndicationFeed feed = SyndicationFeed.Load(XmlReader.Create(feedUrl));

			List<Post> posts = new List<Post>();

			foreach (var item in feed.Items)
			{
				posts.Add(new Post
				{
					AuthorId = 1,
					Title = item.Title.Text,
					Slug = item.Title.Text,
					Description = item.Title.Text,
					Content = item.Summary.Text,
					Published = item.PublishDate.DateTime,
					DateCreated = item.PublishDate.DateTime,
					DateUpdated = item.LastUpdatedTime.DateTime
				});
			}

			return await Task.FromResult(posts);
		}
	}
}
