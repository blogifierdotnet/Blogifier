using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Repositories
{
    public class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        BlogifierDbContext _db;

        public SubscriberRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Subscriber>> Find(Expression<Func<Subscriber, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var items = _db.Subscribers.AsNoTracking().Where(predicate).OrderByDescending(s => s.LastUpdated);
            pager.Configure(await items.CountAsync());
            return await items.Skip(skip).Take(pager.ItemsPerPage).ToListAsync();
        }
    }
}