using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Blogifier.Core.Data.Interfaces;
using System.Threading.Tasks;
using Blogifier.Core.Services.Syndication.Rss;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Common;
using Blogifier.Core.Extensions;

namespace Blogifier.Core.Controllers
{
	[Route("admin")]
	public class AdminController : Controller
	{
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly string _theme;
		private string _identity;
		IUnitOfWork _db;
		IRssService _rss;

		public AdminController(IUnitOfWork db, IRssService rss)
		{
			_db = db;
			_rss = rss;
			_identity = "test"; //---------------------------------------------- User.Identity.Name
			_theme = "~/Views/Blogifier/Themes/Admin/Standard/";
		}

		public IActionResult Index()
		{
			//var adminThemesDir = System.IO.Path.Combine(contentRootPath, "Views\\Blogifier\\Themes\\Admin");

			//var dirInfo = new System.IO.DirectoryInfo(adminThemesDir);
			//var themes = new List<string>();
			//themes.Add("Standard");

			//foreach (var item in dirInfo.GetDirectories())
			//{
			//	// System.Diagnostics.Debug.WriteLine(item.ToString());
			//	themes.Add(item.Name);
			//}

			//ViewBag.Themes = themes;

			//var blog = _db.Blogs.Single(b => b.IdentityName == _identity);

			var posts = _db.Posts.All();

			return View(_theme + "Index.cshtml", posts);
		}

		[Route("about")]
		public IActionResult About()
		{
			return View(_theme + "About.cshtml");
		}

		[HttpGet]
		[Route("syndication")]
		public IActionResult Syndication()
		{
			return View(_theme + "Syndication.cshtml");
		}

		[HttpPost]
		[Route("rssimport")]
		public async Task<IActionResult> RssImport(RssImportModel model)
		{
			//var blog = _db.Blogs.Single(b => b.IdentityName == User.Identity.Name);
			var blog = _db.Blogs.Single(b => b.IdentityName == _identity);

			if (blog == null)
				return View("Error");

			model.PublisherId = blog.Id;
			await _rss.Import(model, Url.Content("~/"));

			return RedirectToAction("Index", "Admin");
		}

		[HttpGet]
		[Route("profile")]
		public IActionResult Profile()
		{
			var blog = _db.Blogs.Single(b => b.IdentityName == _identity);

			return View(_theme + "Profile.cshtml", blog);
		}

		[HttpPost]
		[Route("profile")]
		public async Task<IActionResult> Profile(Publisher model)
		{
			model.LastUpdated = SystemClock.Now();

			if (model.Id == 0)
			{
				model.Slug = BlogSlugFromTitle(model.Title);
				model.IdentityName = _identity;
				model.Theme = "Standard"; //------------------------------------------------
			}
			ModelState.Clear();
			TryValidateModel(model);

			if (ModelState.IsValid)
			{
				if (model.Id > 0)
				{
					var blog = _db.Blogs.Single(b => b.Id == model.Id);
					blog.Title = model.Title;
					blog.Description = model.Description;
					blog.AuthorName = model.AuthorName;
					blog.AuthorEmail = model.AuthorEmail;
				}
				else
				{
					_db.Blogs.Add(model);
				}
				_db.Complete();
				var view = _db.Blogs.Single(b => b.IdentityName == model.IdentityName);
				return View(_theme + "Profile.cshtml", view);
			}
			return RedirectToAction("Index", "Admin");
		}

		#region Private members
		private string BlogSlugFromTitle(string title)
		{
			var slug = title.ToSlug();
			if (_db.Blogs.Single(b => b.Slug == slug) != null)
			{
				for (int i = 2; i < 100; i++)
				{
					if (_db.Blogs.Single(b => b.Slug == slug + i.ToString()) == null)
					{
						return slug + i.ToString();
					}
				}
			}
			return slug;
		}
		#endregion
	}
}
