using AutoMapper;
using Blogifier.Caches;
using Blogifier.Identity;
using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blogifier.Blogs;

public class MainMamager
{
  private readonly IMapper _mapper;
  private readonly IDistributedCache _distributedCache;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly BlogManager _blogManager;
  private readonly CategoryProvider _categoryProvider;
  public MainMamager(
    IMapper mapper,
    IDistributedCache distributedCache,
    IHttpContextAccessor httpContextAccessor,
    BlogManager blogManager,
    CategoryProvider categoryProvider)
  {
    _mapper = mapper;
    _distributedCache = distributedCache;
    _httpContextAccessor = httpContextAccessor;
    _blogManager = blogManager;
    _categoryProvider = categoryProvider;
  }

  public async Task<MainDto> GetAsync()
  {
    var blog = await _blogManager.GetAsync();
    var main = _mapper.Map<MainDto>(blog);
    main.Categories = await GetCategoryItemesAsync();
    var httpContext = _httpContextAccessor.HttpContext;
    if (httpContext != null)
    {
      var request = httpContext.Request;
      main.AbsoluteUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}";
      main.PathUrl = request.Path;
      main.Claims = BlogifierClaims.Analysis(httpContext.User);
    }
    return main;
  }

  public async Task<List<CategoryItemDto>> GetCategoryItemesCacheAsync()
  {
    var key = CacheKeys.CategoryItemes;
    var cache = await _distributedCache.GetAsync(key);
    if (cache != null)
    {
      var value = Encoding.UTF8.GetString(cache);
      return JsonSerializer.Deserialize<List<CategoryItemDto>>(value)!;
    }
    else
    {
      var data = await GetCategoryItemesAsync();
      var value = JsonSerializer.Serialize(data);
      var bytes = Encoding.UTF8.GetBytes(value);
      await _distributedCache.SetAsync(key, bytes, new() { SlidingExpiration = TimeSpan.FromMinutes(15) });
      return data;
    }
  }

  public async Task<List<CategoryItemDto>> GetCategoryItemesAsync()
  {
    return await _categoryProvider.GetItemsExistPostAsync();
  }
}
