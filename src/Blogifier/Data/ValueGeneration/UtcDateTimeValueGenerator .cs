using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blogifier.Data.ValueGeneration;

public class DateTimetValueGenerator : ValueGenerator<DateTime>
{
  public override bool GeneratesTemporaryValues => false;

  public override DateTime Next(EntityEntry entry)
  {
    return DateTime.UtcNow;
  }

  public override ValueTask<DateTime> NextAsync(EntityEntry entry, CancellationToken cancellationToken = default)
  {
    return ValueTask.FromResult(DateTime.UtcNow);
  }
}
