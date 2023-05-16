using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Models;
using Blogifier.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Interfaces;

[Route("api/analytics")]
[ApiController]
public class AnalyticsController : ControllerBase
{
  protected readonly AnalyticsProvider _analyticsProvider;
  protected readonly BlogManager _blogManager;
  protected readonly IMapper _mapper;

  public AnalyticsController(AnalyticsProvider analyticsProvider, BlogManager blogManager, IMapper mapper)
  {
    _analyticsProvider = analyticsProvider;
    _blogManager = blogManager;
    _mapper = mapper;
  }

  [Authorize]
  [HttpGet]
  public async Task<AnalyticsDto> GetAnalytics()
  {
    var blogs = await _blogManager.GetBlogSumInfoAsync();
    var blogsDto = _mapper.Map<IEnumerable<BlogSumDto>>(blogs);
    return new AnalyticsDto { Blogs = blogsDto };
  }

  [Authorize]
  [HttpPut("displayType/{typeId:int}")]
  public async Task<ActionResult<bool>> SaveDisplayType(int typeId)
  {
    return await _analyticsProvider.SaveDisplayType(typeId);
  }

  [Authorize]
  [HttpPut("displayPeriod/{typeId:int}")]
  public async Task<ActionResult<bool>> SaveDisplayPeriod(int typeId)
  {
    return await _analyticsProvider.SaveDisplayPeriod(typeId);
  }
}
