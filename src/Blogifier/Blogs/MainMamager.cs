using AutoMapper;
using Blogifier.Identity;
using Blogifier.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blogifier.Blogs;

public class MainMamager
{
  private readonly IMapper _mapper;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly BlogManager _blogManager;
  public MainMamager(
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    BlogManager blogManager)
  {
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
    _blogManager = blogManager;
  }

  public async Task<MainDto> GetAsync()
  {
    var blog = await _blogManager.GetAsync();
    var categoryItemes = await _blogManager.GetCategoryItemesAsync();
    var main = _mapper.Map<MainDto>(blog);
    main.Categories = categoryItemes;
    var httpContext = _httpContextAccessor.HttpContext;
    if (httpContext != null)
    {
      var request = httpContext.Request;
      main.AbsoluteUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}";
      main.Claims = BlogifierClaims.Analysis(httpContext.User);
    }
    return main;
  }
}
