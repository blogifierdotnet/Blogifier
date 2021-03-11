using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Blogifier.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Blogifier.Core.FilterAttributes;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace Blogifier.Controllers
{
	public class BlogController : Controller
	{
		protected IDataService DataService;
		protected IStorageService StorageService;
		protected SignInManager<AppUser> SignInManager;
		protected IFeedService FeedService;
		private readonly IOptionsMonitor<AppItem> _appSettingsMonitor;

		public BlogController(
			IDataService data,
			IStorageService storage,
			SignInManager<AppUser> signInMgr,
			IFeedService feedService,
			IOptionsMonitor<AppItem> appSettingsMonitor)
		{
			DataService = data;
			StorageService = storage;
			SignInManager = signInMgr;
			FeedService = feedService;
			_appSettingsMonitor = appSettingsMonitor;
		}

		public async Task<IActionResult> Index(string term, int page = 1)
		{			   
			var blog = await DataService.CustomFields.GetBlogSettings();
			var model = new ListModel { 
				Blog = blog, 
				PostListType = PostListType.Blog 
			};
			var pgr = new Pager(page, blog.ItemsPerPage);

			if (string.IsNullOrEmpty(term))
			{
				if(blog.IncludeFeatured)
					model.Posts = await DataService.BlogPosts.GetList(pgr, 0, "", "FP");
				else
					model.Posts = await DataService.BlogPosts.GetList(pgr, 0, "", "P");
			}
			else
			{
				model.Posts = await DataService.BlogPosts.Search(pgr, term, 0, "FP");
			}

			if (pgr.ShowOlder) pgr.LinkToOlder = $"blog?page={pgr.Older}";
			if (pgr.ShowNewer) pgr.LinkToNewer = $"blog?page={pgr.Newer}";

			model.Pager = pgr;

			if (!string.IsNullOrEmpty(term))
			{
				model.Blog.Title = term;
				model.Blog.Description = "";
				model.PostListType = PostListType.Search;
			}

			return View($"~/Views/Themes/{blog.Theme}/List.cshtml", model);
		}

		[HttpGet("categories/{name}")]
		public async Task<IActionResult> Categories(string name, int page = 1)
		{
			var blog = await DataService.CustomFields.GetBlogSettings();
			var model = new ListModel
			{
				Blog = blog,
				PostListType = PostListType.Category
			};
			var pgr = new Pager(page, blog.ItemsPerPage);

			model.Posts = await DataService.BlogPosts.GetList(p => p.Categories.Contains(name), pgr);

			if (pgr.ShowOlder) pgr.LinkToOlder = $"?page={pgr.Older}";
			if (pgr.ShowNewer) pgr.LinkToNewer = $"?page={pgr.Newer}";

			model.Pager = pgr;

			return View($"~/Views/Themes/{blog.Theme}/List.cshtml", model);
		}

		[HttpPost]
		public IActionResult Search(string term)
		{
			return Redirect($"/blog?term={term}");
		}

		[HttpGet("posts/{slug}")]
		public async Task<IActionResult> Single(string slug)
		{
			try
			{
				ViewBag.Slug = slug;
				var model = await DataService.BlogPosts.GetModel(slug);

				// If unpublished and unauthorised redirect to error / 404.
				if (model.Post.Published == DateTime.MinValue && !User.Identity.IsAuthenticated)
				{
					return Redirect("~/error");
				}

				model.Blog = await DataService.CustomFields.GetBlogSettings();
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
			var sitemapBaseUri = Request.ExtractAbsoluteUri();

			var blog = await DataService.CustomFields.GetBlogSettings();
			

			var host = $"{sitemapBaseUri}{Url.Content("~/")}";

			switch (type)
			{
				case "zen":
					var zenPosts = await GetPosts(DateTime.UtcNow.AddYears(-2));
					return ZenFeed(blog, host, zenPosts.ToArray());
				case "turbo":
					var turboPosts = await GetPosts(DateTime.UtcNow.AddYears(-2));
					return TurboFeed(blog, host, turboPosts.ToArray());
				default:
					var rssPosts = await GetPosts(DateTime.UtcNow.AddMonths(-2));
					return await RssFeed(blog, host, rssPosts.ToArray());
			}

			Task<IEnumerable<PostItem>> GetPosts(DateTime since)
			{
				return DataService.BlogPosts.GetList(p => p.Published > since, new Pager(1, 100));
			}
		}

		private IActionResult ZenFeed(BlogItem blog, string host, PostItem[] posts)
		{
			XNamespace contentNamespace = "http://purl.org/rss/1.0/modules/content/"; 
			XNamespace dcNamespace = "http://purl.org/dc/elements/1.1/"; 
			XNamespace mediaNamespace = "http://search.yahoo.com/mrss/"; 
			XNamespace atomNamespace = "http://www.w3.org/2005/Atom"; 
			XNamespace georssNamespace = "http://www.georss.org/georss";

			var feedUri = new Uri(host);

			var itemElements = posts
				.Select(post =>
				{
					var render = post.Content.MdToZen(feedUri);
					
					return new XElement(
						"item",
						new XElement("guid", feedUri + $"posts/{post.Slug}", new XAttribute("isPermaLink", false)),
						new XElement("title", post.Title),
						new XElement("link", feedUri + $"posts/{post.Slug}"),
						new XElement("pubDate", post.Published),
						new XElement("author", post.Author.DisplayName),
						new XElement("title", post.Title),
						post.Categories?.Split(",").Select(x => new XElement("category", x)),
						new XElement("description", post.Description),
						new XElement(contentNamespace + "encoded", render.content),
						new XElement(
							"enclosure",
							new XAttribute("url", feedUri + post.Cover)),
						render.images.Select(x => new XElement(
							"enclosure",
							new XAttribute("url", feedUri + x.Url)))
					);
				});
			
			var rssDocument = new XDocument(
				new XDeclaration("1.0", "UTF-8", "yes"),
				new XElement(
					"rss",
					new XAttribute("version", "2.0"),
					new XAttribute(XNamespace.Xmlns + "content", contentNamespace),
					new XAttribute(XNamespace.Xmlns + "dc", dcNamespace),
					new XAttribute(XNamespace.Xmlns + "media", mediaNamespace),
					new XAttribute(XNamespace.Xmlns + "atom", atomNamespace),
					new XAttribute(XNamespace.Xmlns + "georss", georssNamespace),
					new XElement(
						"channel",
						new XElement("title", blog.Title),
						new XElement("link", feedUri),
						new XElement("description", blog.Description),
						new XElement("language", "ru"),
						itemElements)));

			return new ContentResult
			{
				Content = rssDocument.ToString(),
				ContentType = "text/xml",
				StatusCode = 200
			};
		}

		private IActionResult TurboFeed(BlogItem blog, string host, PostItem[] posts)
		{
			XNamespace yandexNamespace = "http://news.yandex.ru";
			XNamespace mediaNamespace = "http://search.yahoo.com/mrss/";
			XNamespace turboNamespace = "http://turbo.yandex.ru";

			var feedUri = new Uri(host);

			var itemElements = posts
				.Select(post =>
				{
					var header = new XElement(
						"header",
						new XElement("h1", post.Title),
						new XElement(
							"figure",
							new XElement("img", new XAttribute("src", feedUri + post.Cover)))).ToString();
					var contentBody = post.Content.MdToHtml();

					return new XElement(
						"item",
						new XAttribute("turbo", true),
						new XElement("link", feedUri + $"posts/{post.Slug}"),
						new XElement(turboNamespace + "content", new XCData(header + Environment.NewLine + contentBody)));
				});
			
			var rssDocument = new XDocument(
				new XDeclaration("1.0", "UTF-8", "yes"),
				new XElement(
					"rss",
					new XAttribute("version", "2.0"),
					new XAttribute(XNamespace.Xmlns + "media", mediaNamespace),
					new XAttribute(XNamespace.Xmlns + "yandex", yandexNamespace),
					new XAttribute(XNamespace.Xmlns + "turbo", turboNamespace),
					new XElement(
						"channel",
						itemElements)));

			return new ContentResult
			{
				Content = rssDocument.ToString(),
				ContentType = "text/xml",
				StatusCode = 200
			};
		}

		private async Task<IActionResult> RssFeed(BlogItem blog, string host, PostItem[] posts)
		{
			var feed = new SyndicationFeed(
				blog.Title,
				blog.Description,
				new Uri(host),
				host,
				posts.FirstOrDefault().Published
			);

			var items = new List<SyndicationItem>();
			if (posts != null && posts.Count() > 0)
			{
				foreach (var post in posts)
				{
					var atomEntry = await FeedService.GetEntry(post, host);
					var item = new SyndicationItem(
						atomEntry.Title,
						atomEntry.Description.MdToHtml(),
						new Uri(atomEntry.Id),
						atomEntry.Id,
						atomEntry.Published
					);
					item.PublishDate = post.Published;

					item.ElementExtensions.Add("summary", "", $"{post.Description.MdToHtml()}");
					item.ElementExtensions.Add("cover", "", $"{host}{post.Cover}");

					if (atomEntry.Categories != null)
					{
						foreach (var category in atomEntry.Categories)
						{
							item.Categories.Add(new SyndicationCategory(category.Name));
						}
					}

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

		[HttpPost("upload/{uploadType}")]
		[Authorize]
		public async Task<ActionResult> Upload(IFormFile file, UploadType uploadType, int postId = 0)
		{
			var path = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Month);
			var asset = await StorageService.UploadFormFile(file, Url.Content("~/"), path);

			if(uploadType == UploadType.AppLogo)
			{
				var logo = DataService.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogLogo);
				if (logo == null)
					DataService.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogLogo, Content = asset.Url });
				else
					logo.Content = asset.Url;
				DataService.Complete();
			}
			if(uploadType == UploadType.AppCover)
			{
				var cover = DataService.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogCover);
				if (cover == null)
					DataService.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogCover, Content = asset.Url });
				else
					cover.Content = asset.Url;
				DataService.Complete();
			}
			if(uploadType == UploadType.PostCover)
			{
				await DataService.BlogPosts.SaveCover(postId, asset.Url);
				DataService.Complete();
			}
			if(uploadType == UploadType.Avatar)
			{
				var user = DataService.Authors.Single(a => a.AppUserName == User.Identity.Name);
				user.Avatar = asset.Url;
				DataService.Complete();
			}

			return Json(new { asset.Url });
		}

		[HttpPost]
		public IActionResult Subscribe(string email)
		{
			if (!string.IsNullOrEmpty(email) && email.IsEmail())
			{
				try
				{
					var existing = DataService.Newsletters.Single(n => n.Email == email);
					if (existing == null)
					{
						DataService.Newsletters.Add(new Newsletter { Email = email });
						DataService.Complete();
					}
				}
				catch (Exception ex)
				{
					return Ok(ex.Message);
				}
			}
			return Ok();
		}

		[HttpGet("account/logout")]
		public async Task<IActionResult> Logout()
		{
			await SignInManager.SignOutAsync();
			return Redirect("~/blog");
		}
	}
}