using AutoMapper;
using Blogifier.Identity;
using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Blogifier.Blogs;

public class MainMamager
{
  private readonly IMapper _mapper;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly BlogManager _blogManager;
  private readonly CategoryProvider _categoryProvider;
  public MainMamager(
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    BlogManager blogManager,
    CategoryProvider categoryProvider)
  {
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
    _blogManager = blogManager;
    _categoryProvider = categoryProvider;
  }

  public async Task<MainDto> GetAsync()
  {
    var blog = await _blogManager.GetAsync();
    var categoryItemes = await _categoryProvider.GetItemsAsync();
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
