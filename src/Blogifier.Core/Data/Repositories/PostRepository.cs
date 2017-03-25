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
    public class PostRepository : Repository<Publication>, IPostRepository
    {
        BlogifierDbContext _db;

        public PostRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<PostListItem>> Find(Expression<Func<Publication, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var items = _db.Publications.AsNoTracking().Where(predicate).OrderByDescending(p => p.LastUpdated)
                .Include(p => p.PublicationCategories).Include(p => p.Publisher).ToList();

            pager.Configure(items.Count);
            return await Task.Run(() => GetItems(items).Skip(skip).Take(pager.ItemsPerPage).ToList());
        }

        public Task<List<PostListItem>> ByCategory(string slug, Pager pager, string blog = "")
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var list = new List<PostListItem>();

            var cat = _db.Categories.Where(c => c.Slug == slug).FirstOrDefault();
            var posts = blog == "" ?
                _db.Publications.AsNoTracking().Include(p => p.Publisher).Include(p => p.PublicationCategories).ToList() :
                _db.Publications.AsNoTracking().Include(p => p.Publisher).Include(p => p.PublicationCategories).Where(p => p.Publisher.Slug == blog).ToList();

            var categorized = new List<Publication>();
            if (cat != null)
            {
                foreach (var p in posts)
                {
                    foreach (var c in p.PublicationCategories)
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
            _db.PublicationCategories.RemoveRange(_db.PublicationCategories.Where(c => c.PublicationId == postId));
            _db.SaveChanges();

            if (catIds != null && catIds.Count() > 0)
            {
                foreach (var id in catIds)
                {
                    _db.PublicationCategories.Add(new PublicationCategory
                    {
                        PublicationId = postId,
                        CategoryId = int.Parse(id),
                        LastUpdated = DateTime.UtcNow
                    });
                    _db.SaveChanges();
                }
            }
            await _db.SaveChangesAsync();
        }

        public async Task<Publication> SingleIncluded(Expression<Func<Publication, bool>> predicate)
        {
            var post = await _db.Publications.FirstOrDefaultAsync(predicate);

            if (post == null)
                return null;

            // to count post views
            post.PostViews++;

            _db.Publications.Update(post);
            await _db.SaveChangesAsync();

            return await _db.Publications.AsNoTracking()
                .Include(p => p.PublicationCategories)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(predicate);
        }

        #region Private methods

        private List<PostListItem> GetItems(List<Publication> postList)
        {
            var posts = new List<PostListItem>();
            foreach (var p in postList)
            {
                posts.Add(GetItem(p));
            }
            return posts;
        }
        private PostListItem GetItem(Publication post)
        {
            var item = new PostListItem
            {
                PublicationId = post.Id,
                Slug = post.Slug,
                Title = post.Title,
                Image = post.Image,
                Content = post.Description,
                Published = post.Published,
                AuthorName = post.Publisher.AuthorName,
                BlogSlug = post.Publisher.Slug,
                AuthorEmail = post.Publisher.AuthorEmail,
                PostViews = post.PostViews,
                Categories = new List<SelectListItem>()
            };
            item.Categories = GetCategories(post);
            return item;
        }
        private List<SelectListItem> GetCategories(Publication post)
        {
            var catList = new List<SelectListItem>();
            if (post.PublicationCategories != null && post.PublicationCategories.Count > 0)
            {
                foreach (var pc in post.PublicationCategories)
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
