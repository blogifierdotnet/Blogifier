using Blogifier.Core.Data;
using Blogifier.Core.Extensions;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Blogifier.Core.Providers
{
	public interface ISyndicationProvider
	{
		Task<List<Post>> GetPosts(string feedUrl, int userId, Uri baseUrl, string webRoot = "/");
	}

	public class SyndicationProvider : ISyndicationProvider
	{
		private readonly AppDbContext _dbContext;
		private readonly IStorageProvider _storageProvider;
		private readonly string _defaultCover = "img/cover.png";
		private int _userId;
		private string _webRoot;
		private Uri _baseUrl;

		public SyndicationProvider(AppDbContext dbContext, IStorageProvider storageProvider)
		{
			_dbContext = dbContext;
			_storageProvider = storageProvider;
		}

		public async Task<List<Post>> GetPosts(string feedUrl, int userId, Uri baseUrl, string webRoot = "/")
		{
			_userId = userId;
			_webRoot = webRoot;
			_baseUrl = baseUrl;

			SyndicationFeed feed = SyndicationFeed.Load(XmlReader.Create(feedUrl));

			List<Post> posts = new List<Post>();

			foreach (var item in feed.Items)
			{
				posts.Add(await GetPost(item));
			}

			return posts;
		}

		#region Private members

		async Task<Post> GetPost(SyndicationItem syndicationItem)
		{
			Blog blog = await _dbContext.Blogs.FirstOrDefaultAsync();

			Post post = new Post()
			{
				AuthorId = _userId,
				Blog = blog,
				Title = syndicationItem.Title.Text,
				Slug = await GetSlug(syndicationItem.Title.Text),
				Description = syndicationItem.Title.Text,
				Content = syndicationItem.Summary.Text,
				Cover = $"{_webRoot}{_defaultCover}",
				Published = syndicationItem.PublishDate.DateTime,
				DateCreated = syndicationItem.PublishDate.DateTime,
				DateUpdated = syndicationItem.LastUpdatedTime.DateTime
			};

			if (syndicationItem.ElementExtensions != null)
			{
				foreach (SyndicationElementExtension ext in syndicationItem.ElementExtensions)
				{
					if (ext.GetObject<XElement>().Name.LocalName == "summary")
						post.Description = ext.GetObject<XElement>().Value;

					if (ext.GetObject<XElement>().Name.LocalName == "cover")
					{
						post.Cover = ext.GetObject<XElement>().Value;
						var path = string.Format("{0}/{1}/{2}", post.AuthorId, post.Published.Year, post.Published.Month);

						var mdTag = await _storageProvider.UploadFromWeb(new Uri(post.Cover), _webRoot, path);
						if (mdTag.Length > 0 && mdTag.IndexOf("(") > 2)
							post.Cover = mdTag.Substring(mdTag.IndexOf("(") + 2).Replace(")", "");
					}
				}
			}

			if (syndicationItem.Categories != null)
			{
				if (post.Categories == null)
					post.Categories = new List<Category>();

				foreach (var category in syndicationItem.Categories)
				{
					post.Categories.Add(new Category()
					{
						Content = category.Name,
						DateCreated = DateTime.UtcNow,
						DateUpdated = DateTime.UtcNow
					});
				}
			}

			return post;
		}

		async Task<string> GetSlug(string title)
		{
			string slug = title.ToSlug();
			Post post = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Slug == slug);

			if (post == null)
				return slug;

			for (int i = 2; i < 100; i++)
			{
				post = await _dbContext.Posts.AsNoTracking()
					.SingleAsync(p => p.Slug == $"{slug}{i}");

				if (post == null)
					return await Task.FromResult(slug + i.ToString());
			}
			return slug;
		}

		string ValidateUrl(string link)
		{
			var url = link;

			var baseUrl = _baseUrl.ToString();
			if (baseUrl.EndsWith("/"))
				baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);

			if (url.StartsWith("~"))
				url = url.Replace("~", baseUrl);

			if (url.StartsWith("/"))
				url = $"{baseUrl}{url}";

			if (!(url.StartsWith("http:") || url.StartsWith("https:")))
				url = $"{baseUrl}/{url}";

			return url;
		}

		#endregion
	}
}
