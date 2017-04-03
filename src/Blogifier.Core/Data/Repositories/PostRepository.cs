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
    public class PostRepository : Repository<BlogPost>, IPostRepository
    {
        BlogifierDbContext _db;

        public PostRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<PostListItem>> Find(Expression<Func<BlogPost, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var items = _db.BlogPosts.AsNoTracking().Where(predicate).OrderByDescending(p => p.LastUpdated)
                .Include(p => p.BlogPostCategories).Include(p => p.Profile).ToList();

            pager.Configure(items.Count);
            return await Task.Run(() => GetItems(items).Skip(skip).Take(pager.ItemsPerPage).ToList());
        }

        public Task<List<PostListItem>> ByCategory(string slug, Pager pager, string blog = "")
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var list = new List<PostListItem>();

            var cat = _db.Categories.Where(c => c.Slug == slug).FirstOrDefault();
            var posts = blog == "" ?
                _db.BlogPosts.AsNoTracking().Include(p => p.Profile).Include(p => p.BlogPostCategories).ToList() :
                _db.BlogPosts.AsNoTracking().Include(p => p.Profile).Include(p => p.BlogPostCategories).Where(p => p.Profile.Slug == blog).ToList();

            var categorized = new List<BlogPost>();
            if (cat != null)
            {
                foreach (var p in posts)
                {
                    foreach (var c in p.BlogPostCategories)
                    {
                        if (c.CategoryId == cat.Id)
                        {
                            categorized.Add(p);
                            break;
                        }
                    }
                }
            }
            pager.Configure(categorized.Count);
            return Task.Run(() => GetItems(categorized).Skip(skip).Take(pager.ItemsPerPage).ToList());
        }

        public async Task UpdatePostCategories(int postId, IEnumerable<string> catIds)
        {
            _db.PostCategories.RemoveRange(_db.PostCategories.Where(c => c.BlogPostId == postId));
            _db.SaveChanges();

            if (catIds != null && catIds.Count() > 0)
            {
                foreach (var id in catIds)
                {
                    _db.PostCategories.Add(new BlogPostCategory
                    {
                        BlogPostId = postId,
                        CategoryId = int.Parse(id),
                        LastUpdated = DateTime.UtcNow
                    });
                    _db.SaveChanges();
                }
            }
            await _db.SaveChangesAsync();
        }

        public async Task<BlogPost> SingleIncluded(Expression<Func<BlogPost, bool>> predicate)
        {
            var post = await _db.BlogPosts.FirstOrDefaultAsync(predicate);

            if (post == null)
                return null;

            // to count post views
            post.PostViews++;

            _db.BlogPosts.Update(post);
            await _db.SaveChangesAsync();

            return await _db.BlogPosts.AsNoTracking()
                .Include(p => p.BlogPostCategories)
                .Include(p => p.Profile)
                .FirstOrDefaultAsync(predicate);
        }

        #region Private methods

        private List<PostListItem> GetItems(List<BlogPost> postList)
        {
            var posts = new List<PostListItem>();
            foreach (var p in postList)
            {
                posts.Add(GetItem(p));
            }
            return posts;
        }
        private PostListItem GetItem(BlogPost post)
        {
            var item = new PostListItem
            {
                BlogPostId = post.Id,
                Slug = post.Slug,
                Title = post.Title,
                Image = post.Image,
                Content = post.Description,
                Published = post.Published,
                Name = post.Profile.Name,
                BlogSlug = post.Profile.Slug,
                AuthorEmail = post.Profile.AuthorEmail,
                PostViews = post.PostViews,
                Categories = new List<SelectListItem>()
            };
            item.Categories = GetCategories(post);
            return item;
        }
        private List<SelectListItem> GetCategories(BlogPost post)
        {
            var catList = new List<SelectListItem>();
            if (post.BlogPostCategories != null && post.BlogPostCategories.Count > 0)
            {
                foreach (var pc in post.BlogPostCategories)
                {
                    var cat = _db.Categories.AsNoTracking().Where(c => c.Id == pc.CategoryId).FirstOrDefault();
                    catList.Add(new SelectListItem { Value = cat.Slug, Text = cat.Title });
                }
            }
            return catList;
        }

        #endregion

    }
}
