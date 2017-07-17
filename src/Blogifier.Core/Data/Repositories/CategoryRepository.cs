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

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate, Pager pager)
        {
            if(pager == null)
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
                .OrderBy(c => c.Title)
                .ToList();

            pager.Configure(categories.Count());
            
            return categories.Skip(skip).Take(pager.ItemsPerPage);
        }

        //public IEnumerable<SelectListItem> CategoryMenu(Expression<Func<Category, bool>> predicate, int take)
        //{
        //    return _db.Categories.Include(c => c.PostCategories).Where(predicate)
        //        .OrderBy(c => c.Title).GroupBy(c => c.Title).Select(group => group.First()).Take(take)
        //        .Select(c => new SelectListItem { Text = c.Title, Value = c.Slug }).ToList();
        //}

        public IEnumerable<SelectListItem> PostCategories(int postId)
        {
            var items = new List<SelectListItem>();
            var postCategories = _db.PostCategories.Include(pc => pc.Category).Where(c => c.BlogPostId == postId);
            foreach (var item in postCategories)
            {
                items.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Category.Title });
            }
            return items;
        }

        public IEnumerable<SelectListItem> CategoryList(Expression<Func<Category, bool>> predicate)
        {
            var items = new List<SelectListItem>();
            var categories = _db.Categories.Include(c => c.PostCategories).Where(predicate).ToList();
            foreach (var item in categories)
            {
                items.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Title });
            }
            return items;
        }

        public async Task<Category> SingleIncluded(Expression<Func<Category, bool>> predicate)
        {
            return await _db.Categories.AsNoTracking()
                .Include(c => c.PostCategories)
                .FirstOrDefaultAsync(predicate);
        }

        public bool AddCategoryToPost(int postId, int categoryId)
        {
            try
            {
                var existing = _db.PostCategories.Where(
                    pc => pc.BlogPostId == postId &&
                    pc.CategoryId == categoryId).SingleOrDefault();

                if (existing == null)
                {
                    _db.PostCategories.Add(new PostCategory
                    {
                        BlogPostId = postId,
                        CategoryId = categoryId,
                        LastUpdated = SystemClock.Now()
                    });
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveCategoryFromPost(int postId, int categoryId)
        {
            try
            {
                var existing = _db.PostCategories.Where(
                    pc => pc.BlogPostId == postId &&
                    pc.CategoryId == categoryId).SingleOrDefault();
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