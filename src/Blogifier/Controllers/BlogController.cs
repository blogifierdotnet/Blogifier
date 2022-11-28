using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Blogifier.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly IBlogProvider _blogProvider;
		private readonly IAuthorProvider _authorProvider;
		public BlogController(
			IAuthorProvider authorProvider,
			IBlogProvider blogProvider)
		{
			_authorProvider = authorProvider;
			_blogProvider = blogProvider;
		}

		[HttpGet]
		public async Task<Blog> GetBlog()
		{
			return await _blogProvider.GetBlog();
		}

		[HttpGet("categories")]
		public async Task<ICollection<Category>> GetBlogCategories()
		{
			return await _blogProvider.GetBlogCategories();
		}

		[Authorize]
		[HttpPut]
		public async Task<ActionResult<bool>> ChangeTheme([FromBody] Blog blog)
		{
			var currentUser = await _authorProvider.FindByEmail(User.FindFirstValue(ClaimTypes.Name));
			if(!currentUser.IsAdmin)
			{
				return false;
			}
			return await _blogProvider.Update(blog);
		}
	}
}
