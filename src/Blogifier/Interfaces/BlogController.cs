using AutoMapper;
using Blogifier.Blogs;
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
  protected readonly IMapper _mapper;
  protected readonly BlogManager _blogManager;

  public BlogController(BlogProvider blogProvider, IMapper mapper, BlogManager blogManager)
  {
    _blogProvider = blogProvider;
    _mapper = mapper;
    _blogManager = blogManager;
  }

  [HttpGet("setting")]
  public async Task<BlogSettingDto> GetSetting()
  {
    var data = await _blogManager.GetAsync();
    var dataDto = _mapper.Map<BlogSettingDto>(data);
    return dataDto;
  }

  [Authorize]
  [HttpPut("setting")]
  public async Task PutSetting([FromBody] BlogSettingDto blog)
  {
    var data = await _blogManager.GetAsync();
    data.IncludeFeatured = blog.IncludeFeatured;
    data.ItemsPerPage = blog.ItemsPerPage;
    await _blogManager.SetBlogAsync(data);
  }

  [HttpGet("categories")]
  public async Task<ICollection<Category>> GetBlogCategories()
  {
    return await _blogProvider.GetBlogCategories();
  }


}
