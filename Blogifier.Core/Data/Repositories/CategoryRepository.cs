using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> predicate, Pager pager)
        {
            if (pager == null)
            {
                return _db.Categories.AsNoTracking()
                    .Include(c => c.PostCategories)
                    .Where(predicate)
                    .OrderBy(c => c.Title);
            }

            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var categories = _db.Categories.AsNoTracking()
                .Include(c => c.PostCategories)
                .Where(predicate)
                .OrderBy(c => c.Title);

            pager.Configure(await categories.CountAsync());

            return await categories.Skip(skip).Take(pager.ItemsPerPage).ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> PostCategories(int postId)
        {
            var items = new List<SelectListItem>();
            var postCategories = await _db.PostCategories.Include(pc => pc.Category).Where(c => c.BlogPostId == postId).ToListAsync();
            foreach (var item in postCategories)
            {
                var newItem = new SelectListItem { Value = item.Id.ToString(), Text = item.Category.Title };
                if (!items.Contains(newItem))
                {
                    items.Add(newItem);
                }
            }
            return items.OrderBy(c => c.Text);
        }

        public async Task<IEnumerable<SelectListItem>> CategoryList(Expression<Func<Category, bool>> predicate)
        {
            return await _db.Categories.Where(predicate).OrderBy(c => c.Title)
                .Select(c => new SelectListItem { Text = c.Title, Value = c.Id.ToString() }).ToListAsync();
        }

        public async Task<Category> SingleIncluded(Expression<Func<Category, bool>> predicate)
        {
            return await _db.Categories.AsNoTracking()
                .Include(c => c.PostCategories)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AddCategoryToPost(int postId, int categoryId)
        {
            try
            {
                var existing = await _db.PostCategories.Where(
                    pc => pc.BlogPostId == postId &&
                    pc.CategoryId == categoryId).SingleOrDefaultAsync();

                if (existing == null)
                {
                    await _db.PostCategories.AddAsync(new PostCategory
                    {
                        BlogPostId = postId,
                        CategoryId = categoryId,
                        LastUpdated = SystemClock.Now()
                    });
                    await _db.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveCategoryFromPost(int postId, int categoryId)
        {
            try
            {
                var existing = await _db.PostCategories.Where(
                    pc => pc.BlogPostId == postId &&
                    pc.CategoryId == categoryId).SingleOrDefaultAsync();

                if (existing == null)
                {
                    return false;
                }

                _db.PostCategories.Remove(existing);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}