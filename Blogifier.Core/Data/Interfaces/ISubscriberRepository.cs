using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blogifier.Core.Data.Interfaces
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        IEnumerable<Subscriber> Find(Expression<Func<Subscriber, bool>> predicate, Pager pager);
    }
}