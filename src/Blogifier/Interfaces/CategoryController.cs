using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/category")]
[Authorize]
[ApiController]
public class CategoryController : ControllerBase
{
  private readonly CategoryProvider _categoryProvider;

  public CategoryController(CategoryProvider categoryProvider)
  {
    _categoryProvider = categoryProvider;
  }

  [HttpGet("items")]
  public async Task<IEnumerable<CategoryItemDto>> GetItemsAsync()
  {
    return await _categoryProvider.GetItemsAsync();
  }

  [HttpDelete("{id:int}")]
  public async Task DeleteAsync([FromRoute] int id)
  {
    await _categoryProvider.DeleteAsync(id);
  }

  [HttpDelete("{idsString}")]
  public async Task DeleteAsync([FromRoute] string idsString)
  {
    var ids = idsString.Split(',').Select(int.Parse);
    await _categoryProvider.DeleteAsync(ids);
  }

  [HttpGet("{postId:int}")]
  public async Task<IEnumerable<Category>> GetPostCategories(int postId)
  {
    return await _categoryProvider.GetPostCategories(postId);
  }

  [HttpGet("byId/{id:int}")]
  public async Task<Category> GetCategory(int id)
  {
    return await _categoryProvider.GetCategory(id);
  }

  [HttpGet("{term}")]
  public async Task<List<CategoryItemDto>> SearchCategories(string term = "*")
  {
    return await _categoryProvider.SearchCategories(term);
  }

  [HttpPut]
  public async Task<ActionResult<bool>> SaveCategory(Category category)
  {
    return await _categoryProvider.SaveCategory(category);
  }
}
