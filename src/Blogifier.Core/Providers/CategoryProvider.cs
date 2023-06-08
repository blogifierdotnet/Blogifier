using Blogifier.Core.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface ICategoryProvider
	{
        Task<List<CategoryItem>> Categories();
        Task<List<CategoryItem>> SearchCategories(string term);

        Task<Category> GetCategory(int categoryId);
        Task<ICollection<Category>> GetPostCategories(int postId);

        Task<bool> SaveCategory(Category category);
        Task<Category> SaveCategory(string tag);

        Task<bool> AddPostCategory(int postId, string tag);
        Task<bool> SavePostCategories(int postId, List<Category> categories);

        Task<bool> RemoveCategory(int categoryId);
    }

	public class CategoryProvider : ICategoryProvider
	{
		private readonly AppDbContext _db;

		public CategoryProvider(AppDbContext db)
		{
			_db = db;
		}

        public async Task<List<CategoryItem>> Categories()
        {
            var cats = new List<CategoryItem>();

            if (_db.Posts != null && _db.Posts.Count() > 0)
            {
                foreach (var pc in _db.PostCategories.Include(pc => pc.Category).AsNoTracking())
                {
                    if (!cats.Exists(c => c.Category.ToLower() == pc.Category.Content.ToLower()))
                    {
                        cats.Add(new CategoryItem
                        {
                            Selected = false,
                            Id = pc.CategoryId,
                            Category = pc.Category.Content.ToLower(),
                            PostCount = 1,
                            DateCreated = pc.Category.DateCreated
                        });
                    }
                    else
                    {
                        // update post count
                        var tmp = cats.Where(c => c.Category.ToLower() == pc.Category.Content.ToLower()).FirstOrDefault();
                        tmp.PostCount++;
                    }
                }
            }
            return await Task.FromResult(cats);
        }

        public async Task<List<CategoryItem>> SearchCategories(string term)
        {
            var cats = await Categories();

            if (term == "*")
                return cats;

            return cats.Where(c => c.Category.ToLower().Contains(term.ToLower())).ToList();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await _db.Categories.AsNoTracking()
                .Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Category>> GetPostCategories(int postId)
		{
            return await _db.PostCategories.AsNoTracking()
                .Where(pc => pc.PostId == postId)
                .Select(pc => pc.Category)
                .ToListAsync();
		}

        public async Task<bool> SaveCategory(Category category)
        {
            //Category existing = await _db.Categories.AsNoTracking()
            //    .Where(c => c.Content.ToLower() == category.Content.ToLower()).FirstOrDefaultAsync();

            //if (existing != null)
            //    return false; // already exists category with the same title

            Category dbCategory = await _db.Categories.Where(c => c.Id == category.Id).FirstOrDefaultAsync();
            if (dbCategory == null)
                return false;

            dbCategory.Content = category.Content;
            dbCategory.Description = category.Description;
            dbCategory.DateUpdated = DateTime.UtcNow;

            return await _db.SaveChangesAsync() > 0;
        }

		public async Task<Category> SaveCategory(string tag)
		{
			Category category = await _db.Categories
                .AsNoTracking()
                .Where(c => c.Content == tag)
                .FirstOrDefaultAsync();

            if (category != null)
                return category;

            category = new Category()
            {
                Content = tag,
                DateCreated = DateTime.UtcNow
            };
            _db.Categories.Add(category);
			await _db.SaveChangesAsync();

            return category;
		}

        public async Task<bool> AddPostCategory(int postId, string tag)
        {
            Category category = await SaveCategory(tag);

            if (category == null)
                return false;

            Post post = await _db.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();
            if (post == null)
                return false;

            if (post.PostCategories == null)
                post.PostCategories = new List<PostCategory>();

            PostCategory postCategory = await _db.PostCategories
                .AsNoTracking()
                .Where(pc => pc.CategoryId == category.Id)
                .Where(pc => pc.PostId == postId)
                .FirstOrDefaultAsync();

            if (postCategory == null)
            {
                _db.PostCategories.Add(new PostCategory
                {
                    CategoryId = category.Id,
                    PostId = postId
                });
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> SavePostCategories(int postId, List<Category> categories)
        {
            List<PostCategory> existingPostCategories = await _db.PostCategories.AsNoTracking()
                .Where(pc => pc.PostId == postId).ToListAsync();

            _db.PostCategories.RemoveRange(existingPostCategories);

            await _db.SaveChangesAsync();

            foreach (var cat in categories)
            {
                await AddPostCategory(postId, cat.Content);
            }

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveCategory(int categoryId)
        {
            List<PostCategory> postCategories = await _db.PostCategories
                .AsNoTracking()
                .Where(pc => pc.CategoryId == categoryId)
                .ToListAsync();
            _db.PostCategories.RemoveRange(postCategories);

            Category category = _db.Categories
                        .Where(c => c.Id == categoryId)
                        .FirstOrDefault();
            _db.Categories.Remove(category);

            return await _db.SaveChangesAsync() > 0;
        }
    }
}
