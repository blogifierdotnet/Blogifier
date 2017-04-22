using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate, Pager pager);
        IEnumerable<SelectListItem> PostCategories(int postId);
        Task<Category> SingleIncluded(Expression<Func<Category, bool>> predicate);
    }
}
