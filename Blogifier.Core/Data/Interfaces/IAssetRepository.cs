using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IAssetRepository : IRepository<Asset>
    {
        Task<IEnumerable<Asset>> Find(Expression<Func<Asset, bool>> predicate, Pager pager);
    }
}
