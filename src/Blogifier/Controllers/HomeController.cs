using Blogifier.Core.Extensions;
using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Blogifier.Controllers
{
	public class HomeController : Controller
	{
		protected readonly IBlogProvider _blogProvider;
		protected readonly IPostProvider _postProvider;
		protected readonly IFeedProvider _feedProvider;
		protected readonly IAuthorProvider _authorProvider;
		protected readonly IThemeProvider _themeProvider;
		protected readonly IStorageProvider _storageProvider;

		public HomeController(IBlogProvider blogProvider, IPostProvider postProvider, IFeedProvider feedProvider, IAuthorProvider authorProvider, IThemeProvider themeProvider, IStorageProvider storageProvider)
		{
			_blogProvider = blogProvider;
			_postProvider = postProvider;
			_feedProvider = feedProvider;
			_authorProvider = authorProvider;
			_themeProvider = themeProvider;
			_storageProvider = storageProvider;
		}

		public async Task<IActionResult> Index(string term, int page = 1)
		{
			var model = new ListModel { PostListType = PostListType.Blog };
			try
			{
				model.Blog = await _blogProvider.GetBlogItem();
			}
			catch
			{
				return Redirect("~/admin");
			}

			model.Pager = new Pager(page, model.Blog.ItemsPerPage);

			if (string.IsNullOrEmpty(term))
			{
				if (model.Blog.IncludeFeatured)
					model.Posts = await _postProvider.GetList(model.Pager, 0, "", "FP");
				else
					model.Posts = await _postProvider.GetList(model.Pager, 0, "", "P");
			}
			else
			{
				model.PostListType = PostListType.Search;
				model.Blog.Title = term;
				model.Blog.Description = "";
				model.Posts = await _postProvider.Search(model.Pager, term, 0, "FP");
			}	

			if (model.Pager.ShowOlder) model.Pager.LinkToOlder = $"?page={model.Pager.Older}";
			if (model.Pager.ShowNewer) model.Pager.LinkToNewer = $"?page={model.Pager.Newer}";

			return View($"~/Views/Themes/{model.Blog.Theme}/List.cshtml", model);
		}

		[HttpPost]
		public IActionResult Search(string term)
		{
			return Redirect($"/home?term={term}");
		}

		[HttpGet("posts/{slug}")]
		public async Task<IActionResult> Single(string slug)
		{
			try
			{
				ViewBag.Slug = slug;
				PostModel model = await _postProvider.GetPostModel(slug);

				// If unpublished and unauthorised redirect to error / 404.
				if (model.Post.Published == DateTime.MinValue && !User.Identity.IsAuthenticated)
				{
					return Redirect("~/error");
				}

				model.Blog = await _blogProvider.GetBlogItem();
				model.Post.Description = model.Post.Description.MdToHtml();
				model.Post.Content = model.Post.Content.MdToHtml();

				return View($"~/Views/Themes/{model.Blog.Theme}/Post.cshtml", model);
			}
			catch
			{
				return Redirect("~/error");
			}
		}

		[ResponseCache(Duration = 1200)]
		[HttpGet("feed/{type}")]
		public async Task<IActionResult> Rss(string type)
		{
			string host = Request.Scheme + "://" + Request.Host;
			var blog = await _blogProvider.GetBlog();

			var posts = await _feedProvider.GetEntries(type, host);
			var items = new List<SyndicationItem>();

			var feed = new SyndicationFeed(
				 blog.Title,
				 blog.Description,
				 new Uri(host),
				 host,
				 posts.FirstOrDefault().Published
			);

			if (posts != null && posts.Count() > 0)
			{
				foreach (var post in posts)
				{
					var item = new SyndicationItem(
						 post.Title,
						 post.Description.MdToHtml(),
						 new Uri(post.Id),
						 post.Id,
						 post.Published
					);
					item.PublishDate = post.Published;
					items.Add(item);
				}
			}
			feed.Items = items;

			var settings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				NewLineHandling = NewLineHandling.Entitize,
				NewLineOnAttributes = true,
				Indent = true
			};

			using (var stream = new MemoryStream())
			{
				using (var xmlWriter = XmlWriter.Create(stream, settings))
				{
					var rssFormatter = new Rss20FeedFormatter(feed, false);
					rssFormatter.WriteTo(xmlWriter);
					xmlWriter.Flush();
				}
				return File(stream.ToArray(), "application/xml; charset=utf-8");
			}
		}
	}
}
