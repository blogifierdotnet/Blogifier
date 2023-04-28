using Blogifier.Data;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Blogifier.Blogs;

public class BlogManager
{
  private readonly ILogger _logger;
  private readonly IDistributedCache _distributedCache;
  private readonly AppDbContext _dbContext;
  public BlogManager(
    ILogger<BlogManager> logger,
    IDistributedCache distributedCache,
    AppDbContext dbContext)
  {
    _logger = logger;
    _distributedCache = distributedCache;
    _dbContext = dbContext;
  }
}
