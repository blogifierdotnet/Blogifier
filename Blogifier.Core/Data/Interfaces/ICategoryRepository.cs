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
        Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> predicate, Pager pager);

        Task<IEnumerable<SelectListItem>> PostCategories(int postId);
        Task<IEnumerable<SelectListItem>> CategoryList(Expression<Func<Category, bool>> predicate);

        Task<Category> SingleIncluded(Expression<Func<Category, bool>> predicate);

        Task<bool> AddCategoryToPost(int postId, int categoryId);
        Task<bool> RemoveCategoryFromPost(int postId, int categoryId);
    }
}
