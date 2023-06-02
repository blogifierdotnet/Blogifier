using AutoMapper;
using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Newsletters;

public class SubscriberProvider : AppProvider<Subscriber, int>
{
  private readonly IMapper _mapper;
  public SubscriberProvider(
    IMapper mapper,
    AppDbContext dbContext)
    : base(dbContext)
  {
    _mapper = mapper;
  }

  public async Task<IEnumerable<SubscriberDto>> GetItemsAsync()
  {
    var query = _dbContext.Subscribers
     .AsNoTracking()
     .OrderByDescending(n => n.CreatedAt);
    return await _mapper.ProjectTo<SubscriberDto>(query).ToListAsync();
  }
}
