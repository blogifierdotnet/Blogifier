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
		Task<IEnumerable<CategoryItem>> Categories();
		Task<ICollection<Category>> GetPostCategories(int postId);
        Task<Category> SaveCategory(string tag);

        Task<bool> AddPostCategory(int postId, string tag);
        Task<bool> SavePostCategories(int postId, List<Category> categories);

        Task<bool> RemoveCategory(int postId, int categoryId);
	}

	public class CategoryProvider : ICategoryProvider
	{
		private readonly AppDbContext _db;

		public CategoryProvider(AppDbContext db)
		{
			_db = db;
		}

		public async Task<IEnumerable<CategoryItem>> Categories()
		{
			var cats = new List<CategoryItem>();

			if (_db.Posts != null && _db.Posts.Count() > 0)
			{
				foreach (var pc in _db.PostCategories.Include(pc => pc.Category).AsNoTracking())
				{
                    if (!cats.Exists(c => c.Category.ToLower() == pc.Category.Content.ToLower()))
                    {
                        cats.Add(new CategoryItem { Category = pc.Category.Content.ToLower(), PostCount = 1 });
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

		public async Task<ICollection<Category>> GetPostCategories(int postId)
		{
            return await _db.PostCategories.AsNoTracking()
                .Where(pc => pc.PostId == postId)
                .Select(pc => pc.Category)
                .ToListAsync();
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

            foreach (var pc in existingPostCategories)
            {
                await RemoveCategory(postId, pc.CategoryId);
            }

            await _db.SaveChangesAsync();

            foreach (var cat in categories)
            {
                await AddPostCategory(postId, cat.Content);
            }

            return await _db.SaveChangesAsync() > 0;
        }

		public async Task<bool> RemoveCategory(int postId, int categoryId)
		{
            PostCategory postCategory = await _db.PostCategories
                .AsNoTracking()
                .Where(pc => pc.CategoryId == categoryId)
                .Where(pc => pc.PostId == postId)
                .FirstOrDefaultAsync();

            if(postCategory != null)
            {
                _db.PostCategories.Remove(postCategory);

                int postCount = await _db.PostCategories.AsNoTracking()
                    .Where(pc => pc.CategoryId == categoryId).CountAsync();

                if(postCount == 1)
                {
                    Category category = _db.Categories
                        .Where(c => c.Id == categoryId)
                        .FirstOrDefault();
                    _db.Categories.Remove(category);
                }

                return await _db.SaveChangesAsync() > 0;
            }

            return true;
		}
	}
}
