using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Search;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Data
{
    public class DataService : IDataService
    {
        IUnitOfWork _db;
        ISearchService _search;

        public DataService(IUnitOfWork db, ISearchService search)
        {
            _db = db;
            _search = search;
        }

        public BlogPostsModel GetPosts(int page, bool pub = false)
        {
            var pager = new Pager(page);
            IEnumerable<PostListItem> posts = _db.BlogPosts.Find(p => p.Published > DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return null;

            if(pub) posts = SantizePostListItems(posts);

            return new BlogPostsModel
            {
                Posts = posts,
                Pager = pager,
                CustomFields = GetCustom()
            };
        }

        public async Task<BlogAuthorModel> GetPostsByAuthor(string auth, int page, bool pub = false)
        {
            var pager = new Pager(page);
            var profile = await _db.Profiles.Single(p => p.Slug == auth);
            var posts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id && p.Published > DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return null;

            if (pub) posts = SantizePostListItems(posts);

            return new BlogAuthorModel
            {
                CustomFields = GetCustom(profile.Id),
                Profile = profile,
                Posts = posts,
                Pager = pager
            };
        }

        public async Task<BlogCategoryModel> GetPostsByCategory(string auth, string cat, int page, bool pub = false)
        {
            var pager = new Pager(page);
            IEnumerable<PostListItem> posts = _db.BlogPosts.ByCategory(cat, pager).Result;

            if (page < 1 || page > pager.LastPage)
                return null;

            if (pub) posts = SantizePostListItems(posts);
            var profile = await _db.Profiles.Single(p => p.Slug == auth);

            return new BlogCategoryModel
            {
                Profile = profile,
                CustomFields = GetCustom(profile.Id),
                Category = await _db.Categories.Single(c => c.Slug == cat && c.ProfileId == profile.Id),
                Posts = posts,
                Pager = pager
            };
        }

        public async Task<BlogCategoryModel> GetAllPostsByCategory(string cat, int page, bool pub = false)
        {
            var pager = new Pager(page);
            IEnumerable<PostListItem> posts = _db.BlogPosts.ByCategory(cat, pager).Result;

            if (page < 1 || page > pager.LastPage)
                return null;

            if (pub) posts = SantizePostListItems(posts);

            return new BlogCategoryModel
            {
                Profile = null,
                CustomFields = GetCustom(),
                Category = await _db.Categories.Single(c => c.Slug == cat),
                Posts = posts,
                Pager = pager
            };
        }

        public async Task<BlogPostDetailModel> GetPostBySlug(string slug, bool pub = false)
        {
            var vm = new BlogPostDetailModel();
            vm.BlogPost = _db.BlogPosts.SingleIncluded(p => p.Slug == slug).Result;

            if (vm.BlogPost == null)
                return null;

            vm.Profile = vm.BlogPost.Profile != null ? vm.BlogPost.Profile : await _db.Profiles.Single(p => p.Id == vm.BlogPost.ProfileId);

            if (string.IsNullOrEmpty(vm.BlogPost.Image))
            {
                vm.BlogPost.Image = BlogSettings.Cover;
                if (!string.IsNullOrEmpty(BlogSettings.PostCover)) { vm.BlogPost.Image = BlogSettings.PostCover; }
                if (!string.IsNullOrEmpty(vm.Profile.Image)) { vm.BlogPost.Image = vm.Profile.Image; }
            }

            vm.BlogCategories = new List<SelectListItem>();
            if (vm.BlogPost.PostCategories != null && vm.BlogPost.PostCategories.Count > 0)
            {
                foreach (var pc in vm.BlogPost.PostCategories)
                {
                    var cat = await _db.Categories.Single(c => c.Id == pc.CategoryId);
                    vm.BlogCategories.Add(new SelectListItem { Value = cat.Slug, Text = cat.Title });
                }
            }

            vm.CustomFields = GetCustom(vm.Profile.Id);

            if (pub)
            {
                // sanitize the post so that any potentially private information is not contained
                vm.BlogPost.Profile.AuthorEmail = "";
                vm.BlogPost.Profile.IdentityName = "";
                vm.BlogPost.Profile.IsAdmin = false;

                // clear variables which will cause looping issues during serialization 
                vm.BlogPost.Profile.BlogPosts = null;
                vm.BlogPost.Profile.Assets = null;
            }
            return vm;
        }

        public BlogPostsModel SearchPosts(string term, int page, bool pub = false)
        {
            var pager = new Pager(page);
            IEnumerable<PostListItem> posts = _search.Find(pager, term).Result;

            if (page < 1 || page > pager.LastPage)
                return null;

            if (pub) posts = SantizePostListItems(posts);

            return new BlogPostsModel
            {
                Posts = posts,
                Pager = pager,
                CustomFields = GetCustom()
            };
        }

        Dictionary<string, string> GetCustom(int profileId = 0)
        {
            if (_db.CustomFields == null)
                return null;

            var fields = _db.CustomFields.GetBlogFields().Result;
            if (fields == null)
                fields = new Dictionary<string, string>();

            if (profileId > 0)
            {
                var usrFields = _db.CustomFields.GetUserFields(profileId).Result;
                usrFields.ToList().ForEach(x => fields.Add(x.Key, x.Value));
            }
            return fields;
        }

        // Remove potentially private information from the PostListItem for the public API
        IEnumerable<PostListItem> SantizePostListItems(IEnumerable<PostListItem> posts)
        {
            foreach (PostListItem post in posts)
            {
                post.AuthorEmail = "";
            }
            return posts;
        }
    }
}
