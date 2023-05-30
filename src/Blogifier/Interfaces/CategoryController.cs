using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/category")]
[Authorize]
[ApiController]
public class CategoryController : ControllerBase
{
  private readonly CategoryProvider _categoryProvider;

  [HttpGet("items")]
  public async Task<IEnumerable<CategoryItemDto>> GetItemsAsync()
  {
    return await _categoryProvider.GetItemsAsync();
  }

  public CategoryController(CategoryProvider categoryProvider)
  {
    _categoryProvider = categoryProvider;
  }

  [HttpGet("{postId:int}")]
  public async Task<ICollection<Category>> GetPostCategories(int postId)
  {
    return await _categoryProvider.GetPostCategories(postId);
  }

  [HttpGet("byId/{categoryId:int}")]
  public async Task<Category> GetCategory(int categoryId)
  {
    return await _categoryProvider.GetCategory(categoryId);
  }

  [HttpGet("{term}")]
  public async Task<List<CategoryItemDto>> SearchCategories(string term = "*")
  {
    return await _categoryProvider.SearchCategories(term);
  }

  [HttpPost("{postId:int}/{tag}")]
  public async Task<ActionResult<bool>> AddPostCategory(int postId, string tag)
  {
    return await _categoryProvider.AddPostCategory(postId, tag);
  }

  [HttpPut]
  public async Task<ActionResult<bool>> SaveCategory(Category category)
  {
    return await _categoryProvider.SaveCategory(category);
  }

  [HttpPut("{postId:int}")]
  public async Task<ActionResult<bool>> SavePostCategories(int postId, List<Category> categories)
  {
    return await _categoryProvider.SavePostCategories(postId, categories);
  }

  [HttpDelete("{categoryId:int}")]
  public async Task<ActionResult<bool>> RemoveCategory(int categoryId)
  {
    return await _categoryProvider.RemoveCategory(categoryId);
  }
}
