using Blogifier.Core.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IBlogProvider
	{
		Task<Blog> GetBlog();
		Task<ICollection<Category>> GetBlogCategories();
		Task<BlogItem> GetBlogItem();
		Task<bool> Update(Blog blog);
	}

	public class BlogProvider : IBlogProvider
	{
		private readonly AppDbContext _db;
		private readonly IStorageProvider _storageProvider;

		public BlogProvider(AppDbContext db, IStorageProvider storageProvider)
		{
			_db = db;
			_storageProvider = storageProvider;
		}

		public async Task<BlogItem> GetBlogItem()
		{
			var blog = await _db.Blogs.FirstAsync();
			return new BlogItem
			{
				Title = blog.Title,
				Description = blog.Description,
				Theme = blog.Theme,
				ItemsPerPage = blog.ItemsPerPage,
				SocialFields = new List<SocialField>(),
				Cover = string.IsNullOrEmpty(blog.Cover) ? blog.Cover : "img/cover.png",
				Logo = string.IsNullOrEmpty(blog.Logo) ? blog.Logo : "img/logo.png",
				values = await GetValues(blog.Theme)
			};
		}

		public async Task<Blog> GetBlog()
		{
			return await _db.Blogs.AsNoTracking().FirstAsync();
		}

		public async Task<ICollection<Category>> GetBlogCategories()
		{
			return await _db.Categories.AsNoTracking().ToListAsync();
		}

		public async Task<bool> Update(Blog blog)
		{
			var existing = await _db.Blogs.FirstAsync();

			existing.Title = blog.Title;
			existing.Description = blog.Description;
			existing.ItemsPerPage = blog.ItemsPerPage;
			existing.IncludeFeatured = blog.IncludeFeatured;
			existing.Theme = blog.Theme;
			existing.Cover = blog.Cover;
			existing.Logo = blog.Logo;

			return await _db.SaveChangesAsync() > 0;
		}

		private async Task<dynamic> GetValues(string theme)
		{
			var settings = await _storageProvider.GetThemeSettings(theme);
			var values = new Dictionary<string, string>();

			if (settings != null && settings.Sections != null)
			{
				foreach (var section in settings.Sections)
				{
					if (section.Fields != null)
					{
						foreach (var field in section.Fields)
						{
							values.Add(field.Id, field.Value);
						}
					}
				}
			}
			return values;
		}
	}
}
