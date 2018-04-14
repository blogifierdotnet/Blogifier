using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Data.Models;

namespace Core.Data
{
    public interface IPostRepository : IRepository<BlogPost>
    {
        Task<IEnumerable<PostItem>> Find(Expression<Func<BlogPost, bool>> predicate, Pager pager);
        Task<PostItem> GetItem(Expression<Func<BlogPost, bool>> predicate);
        Task<PostItem> SaveItem(PostItem item);
    }

    public class PostRepository : Repository<BlogPost>, IPostRepository
    {
        AppDbContext _db;
        UserManager<AppUser> _um;
        ItemMapper _map;

        public PostRepository(AppDbContext db, UserManager<AppUser> um) : base(db)
        {
            _db = db;
            _um = um;
            _map = new ItemMapper(_db, _um);
        }

        public async Task<IEnumerable<PostItem>> Find(Expression<Func<BlogPost, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var drafts = _db.BlogPosts
                .Where(p => p.Published == DateTime.MinValue).Where(predicate)
                .OrderByDescending(p => p.Published).ToList();

            var pubs = _db.BlogPosts
                .Where(p => p.Published > DateTime.MinValue).Where(predicate)
                .OrderByDescending(p => p.Published).ToList();

            var items = drafts.Concat(pubs).ToList();
            pager.Configure(items.Count);

            var postPage = items.Skip(skip).Take(pager.ItemsPerPage).ToList();

            return await Task.FromResult(_map.MapPostsToItems(postPage));
        }

        public async Task<PostItem> GetItem(Expression<Func<BlogPost, bool>> predicate)
        {
            var post = _db.BlogPosts.Single(predicate);
            return await Task.FromResult(_map.MapPostToItem(post));
        }

        public async Task<PostItem> SaveItem(PostItem item)
        {
            BlogPost post;

            if(item.Id == 0)
            {
                post = new BlogPost
                {
                    Title = item.Title,
                    Slug = item.Title.ToSlug(), //--------------
                    Content = item.Content,
                    Description = string.IsNullOrEmpty(item.Description) ? item.Title : item.Description,
                    UserId = item.Author.Id,
                    Published = item.Published
                };
                _db.BlogPosts.Add(post);
                await _db.SaveChangesAsync();

                post = _db.BlogPosts.Single(p => p.Slug == post.Slug);
                item = _map.MapPostToItem(post);
            }
            else
            {
                post = _db.BlogPosts.Single(p => p.Id == item.Id);

                post.Title = item.Title;
                post.Slug = item.Title.ToSlug(); //--------------
                post.Content = item.Content;
                post.Description = string.IsNullOrEmpty(item.Description) ? item.Title : item.Description;
                post.UserId = item.Author.Id;
                post.Published = item.Published;
                await _db.SaveChangesAsync();

                item.Slug = post.Slug;
            }
            return await Task.FromResult(item);
        }
    }
}