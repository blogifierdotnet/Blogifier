using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
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

        public IEnumerable<PostListItem> Find(Expression<Func<BlogPost, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var drafts = _db.BlogPosts.AsNoTracking().Where(p => p.Published == DateTime.MinValue)
                .Where(predicate).OrderByDescending(p => p.LastUpdated).Include(p => p.Profile).ToList();

            var pubs = _db.BlogPosts.AsNoTracking().Where(p => p.Published > DateTime.MinValue)
                .Where(predicate).OrderByDescending(p => p.Published).Include(p => p.Profile).ToList();

            var items = drafts.Concat(pubs).ToList();

            pager.Configure(items.Count);
            return GetItems(items).Skip(skip).Take(pager.ItemsPerPage);
        }

        public Task<List<PostListItem>> ByCategory(string slug, Pager pager, string blog = "")
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var posts = _db.PostCategories.Include(pc => pc.BlogPost).Include(pc => pc.Category)
                .Where(pc => pc.BlogPost.Published > DateTime.MinValue && pc.Category.Slug == slug)
                .Select(pc => new PostListItem
                {
                    BlogPostId = pc.BlogPostId,
                    Slug = pc.BlogPost.Slug,
                    Title = pc.BlogPost.Title,
                    Image = string.IsNullOrEmpty(pc.BlogPost.Image) ? ApplicationSettings.PostImage : pc.BlogPost.Image,
                    Content = pc.BlogPost.Description,
                    Published = pc.BlogPost.Published,
                    AuthorName = pc.BlogPost.Profile.AuthorName,
                    AuthorEmail = pc.BlogPost.Profile.AuthorEmail,
                    BlogSlug = pc.BlogPost.Profile.Slug,
                    PostViews = pc.BlogPost.PostViews
                }).ToList();

            if (!string.IsNullOrEmpty(blog))
                posts = posts.Where(p => p.BlogSlug == blog).ToList();

            pager.Configure(posts.Count);
            return Task.Run(() => posts.Skip(skip).Take(pager.ItemsPerPage).ToList());
        }

        // posts filtered on status (all, draft or published) and categories
        public Task<List<PostListItem>> ByFilter(string status, List<string> categories, string blog, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var posts = _db.BlogPosts.Include(p => p.PostCategories).Where(p => p.Profile.Slug == blog);

            if(status == "P")
                posts = posts.Where(p => p.Published > DateTime.MinValue);

            if(status == "D")
                posts = posts.Where(p => p.Published == DateTime.MinValue);

            if(categories.Count > 0)
                posts = posts.Where(p => p.PostCategories.Any(pc => pc.BlogPostId == p.Id && categories.Contains(pc.CategoryId.ToString())));

            var postItems = posts.Select(p => new PostListItem
                {
                    BlogPostId = p.Id,
                    Slug = p.Slug,
                    Title = p.Title,
                    Image = string.IsNullOrEmpty(p.Image) ? ApplicationSettings.PostImage : p.Image,
                    Content = p.Description,
                    Published = p.Published,
                    AuthorName = p.Profile.AuthorName,
                    AuthorEmail = p.Profile.AuthorEmail,
                    BlogSlug = p.Profile.Slug,
                    PostViews = p.PostViews
                }).Distinct().ToList();

            pager.Configure(postItems.Count);
            return Task.Run(() => postItems.OrderByDescending(pc => pc.Published).Skip(skip).Take(pager.ItemsPerPage).ToList());
        }

        public async Task UpdatePostCategories(int postId, IEnumerable<string> catIds)
        {
            _db.PostCategories.RemoveRange(_db.PostCategories.Where(c => c.BlogPostId == postId));
            _db.SaveChanges();

            if (catIds != null && catIds.Count() > 0)
            {
                foreach (var id in catIds)
                {
                    _db.PostCategories.Add(new PostCategory
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

        public IEnumerable<BlogPost> AllIncluded(Expression<Func<BlogPost, bool>> predicate)
        {
            return _db.BlogPosts.AsNoTracking().Where(predicate).OrderByDescending(p => p.LastUpdated)
                .Include(p => p.PostCategories).Include(p => p.Profile);
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
                .Include(p => p.PostCategories)
                .Include(p => p.Profile)
                .FirstOrDefaultAsync(predicate);
        }

        #region Private methods

        private List<PostListItem> GetItems(List<BlogPost> postList)
        {
            return postList.Select(p => new PostListItem
            {
                BlogPostId = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                Image = string.IsNullOrEmpty(p.Image) ? ApplicationSettings.PostImage : p.Image,
                Content = p.Description,
                Published = p.Published,
                AuthorName = p.Profile.AuthorName,
                AuthorEmail = p.Profile.AuthorEmail,
                BlogSlug = p.Profile.Slug,
                PostViews = p.PostViews
            }).ToList();
        }

        #endregion
    }
}