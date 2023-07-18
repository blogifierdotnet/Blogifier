using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Data;

public class AppProvider<T, TKey> where T : AppEntity<TKey> where TKey : IEquatable<TKey>
{
  protected readonly AppDbContext _dbContext;

  protected AppProvider(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  protected async Task AddAsync(T entity)
  {
    _dbContext.Set<T>().Add(entity);
    await _dbContext.SaveChangesAsync();
  }

  public Task DeleteAsync(TKey id)
  {
    var query = _dbContext.Set<T>()
      .Where(m => id.Equals(m.Id));
    return DeleteInternalAsync(query);
  }

  public Task DeleteAsync(IEnumerable<TKey>? ids)
  {
    if (ids != null && ids.Any())
    {
      var query = _dbContext.Set<T>()
        .Where(m => ids.Contains(m.Id));
      return DeleteInternalAsync(query);
    }
    return Task.CompletedTask;
  }

  protected static async Task DeleteInternalAsync(IQueryable<T> query)
  {
    await query.ExecuteDeleteAsync();
  }
}
