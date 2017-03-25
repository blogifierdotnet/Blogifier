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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        BlogifierDbContext _db;
        public CategoryRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate, Pager pager)
        {
            if(pager == null)
            {
                return _db.Categories.AsNoTracking()
                    .Include(c => c.PublicationCategories)
                    .Where(predicate)
                    .OrderBy(c => c.Title);
            }

            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var categories = _db.Categories.AsNoTracking()
                .Include(c => c.PublicationCategories)
                .Where(predicate)
                .OrderBy(c => c.Title)
                .ToList();

            pager.Configure(categories.Count());
            
            return categories.Skip(skip).Take(pager.ItemsPerPage);
        }

        public async Task<Category> SingleIncluded(Expression<Func<Category, bool>> predicate)
        {
            return await _db.Categories.AsNoTracking()
                .Include(c => c.PublicationCategories)
                .FirstOrDefaultAsync(predicate);
        }
    }
}