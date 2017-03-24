using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Core.Views.Blogifier.Widgets
{
	[ViewComponent(Name = "Blogifier.Core.Views.Blogifier.Widgets.BlogList")]
	public class BlogListViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(int number)
		{
			var blogs = new List<string>();
			blogs.Add("foo");
			blogs.Add("bar");

			return View(blogs);
		}
	}
}
