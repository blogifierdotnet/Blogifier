using Blogifier.Core.Data;
using Blogifier.Core.Extensions;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IPostProvider
	{
		Task<List<Post>> GetPosts(PublishedStatus filter);
		Task<List<Post>> SearchPosts(string term);
		Task<Post> GetPostById(int id);
		Task<Post> GetPostBySlug(string slug);
		Task<string> GetSlugFromTitle(string title);
		Task<bool> Add(Post post);
		Task<bool> Update(Post post);
		Task<bool> Publish(int id, bool publish);
		Task<bool> Featured(int id, bool featured);
		Task<IEnumerable<PostItem>> GetPostItems();
		Task<PostModel> GetPostModel(string slug);
		Task<IEnumerable<PostItem>> GetPopular(Pager pager, int author = 0);
		Task<IEnumerable<PostItem>> Search(Pager pager, string term, int author = 0, string include = "", bool sanitize = false);
		Task<IEnumerable<PostItem>> GetList(Pager pager, int author = 0, string category = "", string include = "", bool sanitize = true);
		Task<IEnumerable<CategoryItem>> Categories();
		Task<bool> Remove(int id);

		Task<ICollection<Category>> GetPostCategories(int postId);
		Task<bool> AddCategory(int postId, string tag);
		Task<bool> RemoveCategory(int postId, int categoryId);
	}

	public class PostProvider : IPostProvider
	{
		private readonly AppDbContext _db;

		public PostProvider(AppDbContext db)
		{
			_db = db;
		}

		public async Task<List<Post>> GetPosts(PublishedStatus filter)
		{
			switch (filter)
			{
				case PublishedStatus.Published:
					return await _db.Posts.AsNoTracking().Where(p => p.Published > DateTime.MinValue).OrderByDescending(p => p.Published).ToListAsync();
				case PublishedStatus.Drafts:
					return await _db.Posts.AsNoTracking().Where(p => p.Published == DateTime.MinValue).OrderByDescending(p => p.Id).ToListAsync();
				case PublishedStatus.Featured:
					return await _db.Posts.AsNoTracking().Where(p => p.IsFeatured).OrderByDescending(p => p.Id).ToListAsync();
				default:
					return await _db.Posts.AsNoTracking().OrderByDescending(p => p.Id).ToListAsync();
			}
		}

		public async Task<List<Post>> SearchPosts(string term)
		{
			if (term == "*")
				return await _db.Posts.ToListAsync();

			return await _db.Posts
				.AsNoTracking()
				.Where(p => p.Title.ToLower().Contains(term.ToLower()))
				.ToListAsync();
		}

		public async Task<IEnumerable<PostItem>> Search(Pager pager, string term, int author = 0, string include = "", bool sanitize = false)
		{
			var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

			var results = new List<SearchResult>();
			var termList = term.ToLower().Split(' ').ToList();

			foreach (var p in GetPosts(include, author))
			{
				var rank = 0;
				var hits = 0;
				term = term.ToLower();

				foreach (var termItem in termList)
				{
					if (termItem.Length < 4 && rank > 0) continue;

					if (p.Categories != null && p.Categories.Count > 0)
					{
						foreach (var catItem in p.Categories)
						{
							if (catItem.Content == termItem) rank += 10;
						}
					}
					if (p.Title.ToLower().Contains(termItem))
					{
						hits = Regex.Matches(p.Title.ToLower(), termItem).Count;
						rank += hits * 10;
					}
					if (p.Description.ToLower().Contains(termItem))
					{
						hits = Regex.Matches(p.Description.ToLower(), termItem).Count;
						rank += hits * 3;
					}
					if (p.Content.ToLower().Contains(termItem))
					{
						rank += Regex.Matches(p.Content.ToLower(), termItem).Count;
					}
				}
				if (rank > 0)
				{
					results.Add(new SearchResult { Rank = rank, Item = await PostToItem(p, sanitize) });
				}
			}

			results = results.OrderByDescending(r => r.Rank).ToList();

			var posts = new List<PostItem>();
			for (int i = 0; i < results.Count; i++)
			{
				posts.Add(results[i].Item);
			}
			pager.Configure(posts.Count);
			return await Task.Run(() => posts.Skip(skip).Take(pager.ItemsPerPage).ToList());
		}

		public async Task<Post> GetPostById(int id)
		{
			return await _db.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<PostItem>> GetPostItems()
		{
			var posts = await _db.Posts.ToListAsync();
			var postItems = new List<PostItem>();

			foreach (var post in posts)
			{
				postItems.Add(new PostItem
				{
					Id = post.Id,
					Title = post.Title,
					Description = post.Description,
					Content = post.Content,
					Slug = post.Slug,
					Author = _db.Authors.Where(a => a.Id == post.AuthorId).First(),
					Cover = string.IsNullOrEmpty(post.Cover) ? "img/cover.png" : post.Cover,
					Published = post.Published,
					PostViews = post.PostViews,
					Featured = post.IsFeatured
				});
			}

			return postItems;
		}

		public async Task<PostModel> GetPostModel(string slug)
		{
			var model = new PostModel();

			var all = _db.Posts
				.AsNoTracking()
				.OrderByDescending(p => p.IsFeatured)
				.ThenByDescending(p => p.Published).ToList();

			if (all != null && all.Count > 0)
			{
				for (int i = 0; i < all.Count; i++)
				{
					if (all[i].Slug == slug)
					{
						model.Post = await PostToItem(all[i]);

						if (i > 0 && all[i - 1].Published > DateTime.MinValue)
							model.Newer = await PostToItem(all[i - 1]);

						if (i + 1 < all.Count && all[i + 1].Published > DateTime.MinValue)
							model.Older = await PostToItem(all[i + 1]);

						break;
					}
				}
			}

			var post = _db.Posts.Single(p => p.Slug == slug);
			post.PostViews++;
			await _db.SaveChangesAsync();
			// await SaveStatsTotals(post.Id);

			return await Task.FromResult(model);
		}

		public async Task<Post> GetPostBySlug(string slug)
		{
			return await _db.Posts.Where(p => p.Slug == slug).FirstOrDefaultAsync();
		}

		public async Task<string> GetSlugFromTitle(string title)
		{
			string slug = title.ToSlug();
			var post = _db.Posts.Where(p => p.Slug == slug).FirstOrDefault();

			if (post != null)
			{
				for (int i = 2; i < 100; i++)
				{
					slug = $"{slug}{i}";
					if (_db.Posts.Where(p => p.Slug == slug).FirstOrDefault() == null)
					{
						return await Task.FromResult(slug);
					}
				}
			}
			return await Task.FromResult(slug);
		}

		public async Task<bool> Add(Post post)
		{
			var existing = await _db.Posts.Where(p => p.Slug == post.Slug).FirstOrDefaultAsync();
			if (existing != null)
				return false;

			post.Blog = _db.Blogs.First();

			await _db.Posts.AddAsync(post);
			return await _db.SaveChangesAsync() > 0;
		}

		public async Task<bool> Update(Post post)
		{
			var existing = await _db.Posts.Where(p => p.Slug == post.Slug).FirstOrDefaultAsync();
			if (existing == null)
				return false;

			existing.Slug = post.Slug;
			existing.Title = post.Title;
			existing.Description = post.Description;
			existing.Content = post.Content;
			existing.Cover = post.Cover;
			existing.Published = post.Published;

			return await _db.SaveChangesAsync() > 0;
		}

		public async Task<bool> Publish(int id, bool publish)
		{
			var existing = await _db.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
			if (existing == null)
				return false;

			existing.Published = publish ? DateTime.UtcNow : DateTime.MinValue;

			return await _db.SaveChangesAsync() > 0;
		}

		public async Task<bool> Featured(int id, bool featured)
		{
			var existing = await _db.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
			if (existing == null)
				return false;

			existing.IsFeatured = featured;

			return await _db.SaveChangesAsync() > 0;
		}

		public async Task<IEnumerable<PostItem>> GetList(Pager pager, int author = 0, string category = "", string include = "", bool sanitize = true)
		{
			var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

			var posts = new List<Post>();
			foreach (var p in GetPosts(include, author))
			{
				if (string.IsNullOrEmpty(category))
				{
					posts.Add(p);
				}
				else
				{
					if (p.Categories != null && p.Categories.Count > 0)
					{
						foreach (var item in p.Categories)
						{
							if(item.Content.ToLower() == category.ToLower())
							{
								posts.Add(p);
							}
						}
					}
				}
			}
			pager.Configure(posts.Count);

			var items = new List<PostItem>();
			foreach (var p in posts.Skip(skip).Take(pager.ItemsPerPage).ToList())
			{
				items.Add(await PostToItem(p, sanitize));
			}
			return await Task.FromResult(items);
		}

		public async Task<IEnumerable<PostItem>> GetPopular(Pager pager, int author = 0)
		{
			var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

			var posts = new List<Post>();

			if (author > 0)
				posts = _db.Posts.AsNoTracking().Where(p => p.Published > DateTime.MinValue && p.AuthorId == author)
					 .OrderByDescending(p => p.PostViews).ThenByDescending(p => p.Published).ToList();
			else
				posts = _db.Posts.AsNoTracking().Where(p => p.Published > DateTime.MinValue)
					 .OrderByDescending(p => p.PostViews).ThenByDescending(p => p.Published).ToList();

			pager.Configure(posts.Count);

			var items = new List<PostItem>();
			foreach (var p in posts.Skip(skip).Take(pager.ItemsPerPage).ToList())
			{
				items.Add(await PostToItem(p, true));
			}
			return await Task.FromResult(items);
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

		public async Task<bool> Remove(int id)
		{
			var existing = await _db.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
			if (existing == null)
				return false;

			_db.Posts.Remove(existing);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<ICollection<Category>> GetPostCategories(int postId)
		{
			return await _db.Posts.Where(p => p.Id == postId).SelectMany(p => p.Categories).ToListAsync();
		}

		public async Task<bool> AddCategory(int postId, string tag)
		{
			var category = await _db.Categories.AsNoTracking().Where(c => c.Content == tag).FirstOrDefaultAsync();
			if (category != null)
				return false;

			_db.Categories.Add(new Category() { Content = tag });
			await _db.SaveChangesAsync();

			category = await _db.Categories.Where(c => c.Content == tag).FirstOrDefaultAsync();
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
				foreach (var category in post.Categories)
				{
					if(category.Id == categoryId)
					{
						var tag = category.Content;
						post.Categories.Remove(category);
						if(await _db.SaveChangesAsync() > 0)
						{
							//if (LastCategory(tag))
							//{
							//	var cat = _db.Categories.AsNoTracking().Where(c => c.Content == tag).FirstOrDefault();
							//	_db.Categories.Remove(cat);
							//	return await _db.SaveChangesAsync() > 0;
							//}
						}
						return true;
					}
				}
			}
			return false;
		}

		#region Private methods

		async Task<PostItem> PostToItem(Post p, bool sanitize = false)
		{
			var post = new PostItem
			{
				Id = p.Id,
				Slug = p.Slug,
				Title = p.Title,
				Description = p.Description,
				Content = p.Content,
				Categories = p.Categories,
				Cover = p.Cover,
				PostViews = p.PostViews,
				Rating = p.Rating,
				Published = p.Published,
				Featured = p.IsFeatured,
				Author = _db.Authors.Single(a => a.Id == p.AuthorId),
				SocialFields = new List<SocialField>()
				//SocialFields = await _customFieldRepository.GetSocial(p.AuthorId)
			};

			if (post.Author != null)
			{
				post.Author.Avatar = string.IsNullOrEmpty(post.Author.Avatar) ?
					 "img/avatar.png" : post.Author.Avatar;
				post.Author.Email = sanitize ? "donotreply@us.com" : post.Author.Email;
			}
			return await Task.FromResult(post);
		}

		List<Post> GetPosts(string include, int author)
		{
			var items = new List<Post>();
			var pubfeatured = new List<Post>();

			if (include.ToUpper().Contains("D") || string.IsNullOrEmpty(include))
			{
				var drafts = author > 0 ?
					 _db.Posts.Where(p => p.Published == DateTime.MinValue && p.AuthorId == author).ToList() :
					 _db.Posts.Where(p => p.Published == DateTime.MinValue).ToList();
				items = items.Concat(drafts).ToList();
			}

			if (include.ToUpper().Contains("F") || string.IsNullOrEmpty(include))
			{
				var featured = author > 0 ?
					 _db.Posts.Where(p => p.Published > DateTime.MinValue && p.IsFeatured && p.AuthorId == author).OrderByDescending(p => p.Published).ToList() :
					 _db.Posts.Where(p => p.Published > DateTime.MinValue && p.IsFeatured).OrderByDescending(p => p.Published).ToList();
				pubfeatured = pubfeatured.Concat(featured).ToList();
			}

			if (include.ToUpper().Contains("P") || string.IsNullOrEmpty(include))
			{
				var published = author > 0 ?
					 _db.Posts.Where(p => p.Published > DateTime.MinValue && !p.IsFeatured && p.AuthorId == author).OrderByDescending(p => p.Published).ToList() :
					 _db.Posts.Where(p => p.Published > DateTime.MinValue && !p.IsFeatured).OrderByDescending(p => p.Published).ToList();
				pubfeatured = pubfeatured.Concat(published).ToList();
			}

			pubfeatured = pubfeatured.OrderByDescending(p => p.Published).ToList();
			items = items.Concat(pubfeatured).ToList();

			return items;
		}

		bool LastCategory(string tag)
		{
			foreach (var post in _db.Posts)
			{
				if (post.Categories.Where(c => c.Content == tag).Count() > 0)
					return false;
			}
			return true;
		}

		#endregion
	}
}
