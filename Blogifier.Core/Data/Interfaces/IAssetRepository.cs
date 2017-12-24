using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IAssetRepository : IRepository<Asset>
    {
        IEnumerable<Asset> Find(Expression<Func<Asset, bool>> predicate, Pager pager);
    }
}
