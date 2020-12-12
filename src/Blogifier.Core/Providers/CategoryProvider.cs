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
		Task<bool> AddCategory(int postId, string tag);
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
				foreach (var p in _db.Posts)
				{
					if (p.Categories != null && p.Categories.Count() > 0)
					{
						foreach (var pc in p.Categories)
						{
							if (!cats.Exists(c => c.Category.ToLower() == pc.Content.ToLower()))
							{
								cats.Add(new CategoryItem { Category = pc.Content, PostCount = 1 });
							}
							else
							{
								// update post count
								var tmp = cats.Where(c => c.Category.ToLower() == pc.Content.ToLower()).FirstOrDefault();
								tmp.PostCount++;
							}
						}
					}
				}
			}
			return await Task.FromResult(cats);
		}

		public async Task<ICollection<Category>> GetPostCategories(int postId)
		{
			return await _db.Posts.Where(p => p.Id == postId).SelectMany(p => p.Categories).ToListAsync();
		}

		public async Task<bool> AddCategory(int postId, string tag)
		{
			var category = await _db.Categories.AsNoTracking().Where(c => c.Content == tag).FirstOrDefaultAsync();
			if (category == null)
			{
				_db.Categories.Add(new Category() { Content = tag, DateCreated = DateTime.UtcNow });
				await _db.SaveChangesAsync();
				category = await _db.Categories.Where(c => c.Content == tag).FirstOrDefaultAsync();
			}			
						
			if (category == null)
				return false;

			var post = await _db.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();
			if (post == null)
				return false;

			if (post.Categories == null)
				post.Categories = new List<Category>();

			post.Categories.Add(category);
			try
			{
				await _db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				var x = ex.Message;
				return false;
			}
			return true;
		}

		public async Task<bool> RemoveCategory(int postId, int categoryId)
		{
			Post post = await _db.Posts.Include(p => p.Categories)
				.Where(p => p.Id == postId)
				.FirstOrDefaultAsync();

			if (post != null)
			{
				var category = post.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

				try
				{
					if (category != null)
					{
						post.Categories.Remove(category);
						await _db.SaveChangesAsync();
					}
				}
				catch
				{
					return false;
				}
				
				//foreach (var category in post.Categories)
				//{
				//	if (category.Id == categoryId)
				//	{
				//		var tag = category.Content;
				//		post.Categories.Remove(category);
				//		if (await _db.SaveChangesAsync() > 0)
				//		{
				//			//if (LastCategory(tag))
				//			//{
				//			//	var cat = _db.Categories.AsNoTracking().Where(c => c.Content == tag).FirstOrDefault();
				//			//	_db.Categories.Remove(cat);
				//			//	return await _db.SaveChangesAsync() > 0;
				//			//}
				//		}
				//		return true;
				//	}
				//}
			}
			return true;
		}
	}
}
