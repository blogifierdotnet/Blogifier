﻿using Blogifier.Core.Helpers;
using Blogifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Data
{
    public interface IPostRepository : IRepository<BlogPost>
    {
        Task<IEnumerable<PostItem>> GetList(Expression<Func<BlogPost, bool>> predicate, Pager pager);
        Task<IEnumerable<PostItem>> GetList(Pager pager, int author = 0, string category = "", string include = "", bool sanitize = false);
        Task<IEnumerable<PostItem>> GetPopular(Pager pager, int author = 0);
        Task<IEnumerable<PostItem>> Search(Pager pager, string term, int author = 0, string include = "", bool sanitize = false);
        Task<PostItem> GetItem(Expression<Func<BlogPost, bool>> predicate, bool sanitize = false);
        Task<PostModel> GetModel(string slug);
        Task<PostItem> SaveItem(PostItem item);
        Task SaveCover(int postId, string asset);
        Task<IEnumerable<CategoryItem>> Categories();
    }

    public class PostRepository : Repository<BlogPost>, IPostRepository
    {
        AppDbContext _db;
        ICustomFieldRepository _customFieldRepository;

        public PostRepository(AppDbContext db, ICustomFieldRepository customFieldRepository) : base(db)
        {
            _db = db;
            _customFieldRepository = customFieldRepository;
        }

        public async Task<IEnumerable<PostItem>> GetList(Expression<Func<BlogPost, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var drafts = _db.BlogPosts
                .Where(p => p.Published == DateTime.MinValue).Where(predicate)
                .OrderByDescending(p => p.Published).ToList();

            var pubs = _db.BlogPosts
                .Where(p => p.Published > DateTime.MinValue).Where(predicate)
                .OrderByDescending(p => p.IsFeatured)
                .ThenByDescending(p => p.Published).ToList();

            var items = drafts.Concat(pubs).ToList();
            pager.Configure(items.Count);

            var postPage = items.Skip(skip).Take(pager.ItemsPerPage).ToList();

            return await Task.FromResult(PostListToItems(postPage));
        }

        public async Task<IEnumerable<PostItem>> GetList(Pager pager, int author = 0, string category = "", string include = "", bool sanitize = true)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var posts = new List<BlogPost>();
            foreach (var p in GetPosts(include, author))
            {
                if (string.IsNullOrEmpty(category))
                {
                    posts.Add(p);
                }
                else
                {
                    if (!string.IsNullOrEmpty(p.Categories))
                    {
                        var cats = p.Categories.ToLower().Split(',');
                        if (cats.Contains(category.ToLower()))
                        {
                            posts.Add(p);
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

            var posts = new List<BlogPost>();

            if(author > 0)
            {
                posts = _db.BlogPosts.Where(p => p.Published > DateTime.MinValue && p.AuthorId == author)
                    .OrderByDescending(p => p.PostViews).ThenByDescending(p => p.Published).ToList();
            }
            else
            {
                posts = _db.BlogPosts.Where(p => p.Published > DateTime.MinValue)
                    .OrderByDescending(p => p.PostViews).ThenByDescending(p => p.Published).ToList();
            }

            pager.Configure(posts.Count);

            var items = new List<PostItem>();
            foreach (var p in posts.Skip(skip).Take(pager.ItemsPerPage).ToList())
            {
                items.Add(await PostToItem(p, true));
            }
            return await Task.FromResult(items);
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

                    if (!string.IsNullOrEmpty(p.Categories))
                    {
                        var catList = p.Categories.ToLower().Split(',').ToList();
                        foreach (var catItem in catList)
                        {
                            if (catItem == termItem) rank += 10;
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

        public async Task<PostItem> GetItem(Expression<Func<BlogPost, bool>> predicate, bool sanitize = false)
        {
            var post = _db.BlogPosts.Single(predicate);
            var item = await PostToItem(post);

            item.Author.Avatar = string.IsNullOrEmpty(item.Author.Avatar) ? Constants.DefaultAvatar : item.Author.Avatar;
            item.Author.Email = sanitize ? Constants.DummyEmail : item.Author.Email;

            post.PostViews++;
            await _db.SaveChangesAsync();
            await SaveStatsTotals(post.Id);

            return await Task.FromResult(item);
        }

        public async Task<PostModel> GetModel(string slug)
        {
            var model = new PostModel();

            var all = _db.BlogPosts
                .OrderByDescending(p => p.IsFeatured)
                .ThenByDescending(p => p.Published).ToList();

            if(all != null && all.Count > 0)
            {
                for (int i = 0; i < all.Count; i++)
                {
                    if(all[i].Slug == slug)
                    {
                        model.Post = await PostToItem(all[i]);

                        if(i > 0 && all[i - 1].Published > DateTime.MinValue)
                        {
                            model.Newer = await PostToItem(all[i - 1]);
                        }

                        if (i + 1 < all.Count && all[i + 1].Published > DateTime.MinValue)
                        {
                            model.Older = await PostToItem(all[i + 1]);
                        }

                        break;
                    }
                }
            }

            var post = _db.BlogPosts.Single(p => p.Slug == slug);
            post.PostViews++;
            await _db.SaveChangesAsync();
            await SaveStatsTotals(post.Id);

            return await Task.FromResult(model);
        }

        public async Task<PostItem> SaveItem(PostItem item)
        {
            BlogPost post;
            var field = _db.CustomFields.Where(f => f.AuthorId == 0 && f.Name == Constants.BlogCover).FirstOrDefault();
            var cover = field == null ? "" : field.Content;

            if (item.Id == 0)
            {
                post = new BlogPost
                {
                    Title = item.Title,
                    Slug = item.Slug,
                    Content = item.Content,
                    Description = item.Description ?? item.Title,
                    Categories = item.Categories,
                    Cover = item.Cover ?? cover,
                    AuthorId = item.Author.Id,
                    IsFeatured = item.Featured,
                    Published = item.Published
                };
                _db.BlogPosts.Add(post);
                await _db.SaveChangesAsync();

                post = _db.BlogPosts.Single(p => p.Slug == post.Slug);
                item = await PostToItem(post);
            }
            else
            {
                post = _db.BlogPosts.Single(p => p.Id == item.Id);

                post.Slug = item.Slug;
                post.Title = item.Title;
                post.Content = item.Content;
                post.Description = item.Description ?? item.Title;
                post.Categories = item.Categories;
                post.AuthorId = item.Author.Id;
                post.Published = item.Published;
                post.IsFeatured = item.Featured;
                await _db.SaveChangesAsync();
            }
            return await Task.FromResult(item);
        }

        public async Task SaveCover(int postId, string asset)
        {
            var item = _db.BlogPosts.Single(p => p.Id == postId);
            item.Cover = asset;

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryItem>> Categories()
        {
            var cats = new List<CategoryItem>();

            if (_db.BlogPosts.Any())
            {
                foreach (var p in _db.BlogPosts.Where(p => p.Categories != null && p.Published > DateTime.MinValue))
                {
                    var postcats = p.Categories.ToLower().Split(',');
                    if (postcats.Any())
                    {
                        foreach (var pc in postcats)
                        {
                            if (!cats.Exists(c => c.Category.ToLower() == pc.ToLower()))
                            {
                                cats.Add(new CategoryItem { Category = pc, PostCount = 1 });
                            }
                            else
                            {
                                // update post count
                                var tmp = cats.Where(c => c.Category.ToLower() == pc.ToLower()).FirstOrDefault();
                                tmp.PostCount++;
                            }
                        }
                    }
                }
            }
            return await Task.FromResult(cats.OrderBy(c => c));
        }

        async Task<PostItem> PostToItem(BlogPost p, bool sanitize = false)
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
                SocialFields = await _customFieldRepository.GetSocial(p.AuthorId)
            };

            if(post.Author != null)
            {
                post.Author.Avatar = string.IsNullOrEmpty(post.Author.Avatar) ?
                    AppSettings.Avatar : post.Author.Avatar;
                post.Author.Email = sanitize ? Constants.DummyEmail : post.Author.Email;
            }
            return post;
        }

        public List<PostItem> PostListToItems(List<BlogPost> posts)
        {
            return posts.Select(p => new PostItem
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
                Author = _db.Authors.Single(a => a.Id == p.AuthorId)
            }).Distinct().ToList();
        }

        List<BlogPost> GetPosts(string include, int author)
        {
            var items = new List<BlogPost>();
            var pubfeatured = new List<BlogPost>();

            if (include.ToUpper().Contains("D") || string.IsNullOrEmpty(include))
            {
                var drafts = author > 0 ?
                    _db.BlogPosts.Where(p => p.Published == DateTime.MinValue && p.AuthorId == author).ToList() :
                    _db.BlogPosts.Where(p => p.Published == DateTime.MinValue).ToList();
                items = items.Concat(drafts).ToList();
            }

            if (include.ToUpper().Contains("F") || string.IsNullOrEmpty(include))
            {
                var featured = author > 0 ?
                    _db.BlogPosts.Where(p => p.Published > DateTime.MinValue && p.IsFeatured && p.AuthorId == author).OrderByDescending(p => p.Published).ToList() :
                    _db.BlogPosts.Where(p => p.Published > DateTime.MinValue && p.IsFeatured).OrderByDescending(p => p.Published).ToList();
                pubfeatured = pubfeatured.Concat(featured).ToList();
            }

            if (include.ToUpper().Contains("P") || string.IsNullOrEmpty(include))
            {
                var published = author > 0 ?
                    _db.BlogPosts.Where(p => p.Published > DateTime.MinValue && !p.IsFeatured && p.AuthorId == author).OrderByDescending(p => p.Published).ToList() :
                    _db.BlogPosts.Where(p => p.Published > DateTime.MinValue && !p.IsFeatured).OrderByDescending(p => p.Published).ToList();
                pubfeatured = pubfeatured.Concat(published).ToList();
            }

            pubfeatured = pubfeatured.OrderByDescending(p => p.Published).ToList();
            items = items.Concat(pubfeatured).ToList();

            return items;
        }

        async Task SaveStatsTotals(int postId)
        {
            try
            {
                var existentTotal = _db.StatsTotals.Where(s => s.PostId == postId 
                    && s.DateCreated == SystemClock.Now().Date).FirstOrDefault();

                if (existentTotal == null)
                {
                    await _db.StatsTotals.AddAsync(new StatsTotal
                    {
                        PostId = postId,
                        Total = 1,
                        DateCreated = SystemClock.Now().Date
                    });
                }
                else
                {
                    existentTotal.Total = existentTotal.Total + 1;
                }
                await _db.SaveChangesAsync();
            }
            catch { }
        }
    }

    internal class SearchResult
    {
        public int Rank { get; set; }
        public PostItem Item { get; set; }
    }
}