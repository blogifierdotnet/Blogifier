using Blogifier.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blogifier.Blogs;

public class MainManager
{
  protected readonly ILogger _logger;
  protected readonly IHttpContextAccessor _httpContextAccessor;
  protected readonly BlogManager _blogManager;

  public MainManager(
    ILogger<MainManager> logger,
    IHttpContextAccessor httpContextAccessor,
    BlogManager blogManager)
  {
    _logger = logger;
    _httpContextAccessor = httpContextAccessor;
    _blogManager = blogManager;
  }

  public async Task<MainData> GetMainAsync()
  {
    var blogData = await _blogManager.GetBlogDataAsync();
    var categoryItemes = await _blogManager.GetCategoryItemesAsync();
    var httpContext = _httpContextAccessor.HttpContext;
    if (httpContext != null)
    {
      var request = httpContext.Request;
      var absoluteUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}";
      var claims = BlogifierClaims.Analysis(httpContext.User);
      return new MainData(blogData, categoryItemes, absoluteUrl, claims);
    }
    return new MainData(blogData, categoryItemes);
  }
}
