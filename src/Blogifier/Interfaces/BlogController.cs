using Blogifier.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/blog")]
[ApiController]
public class BlogController : ControllerBase
{
  private readonly BlogProvider _blogProvider;

  public BlogController(BlogProvider blogProvider)
  {
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
    return await _blogProvider.Update(blog);
  }
}
