using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.Custom;
using Blogifier.Core.Services.Search;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Blogifier.Core.Services.Data
{
    public class DataService : IDataService
    {
        IUnitOfWork _db;
        ICustomService _custom;
        IRssService _rss;
        ISearchService _search;

        public DataService(IUnitOfWork db, ICustomService custom, IRssService rss, ISearchService search)
        {
            _db = db;
            _custom = custom;
            _rss = rss;
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
                CustomFields = new Dictionary<string, string>()
            };
        }

        public BlogAuthorModel GetPostsByAuthor(string auth, int page, bool pub = false)
        {
            var pager = new Pager(page);
            var profile = _db.Profiles.Single(p => p.Slug == auth);
            var posts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id && p.Published > DateTime.MinValue, pager);

            if (page < 1 || page > pager.LastPage)
                return null;

            if (pub) posts = SantizePostListItems(posts);

            return new BlogAuthorModel
            {
                CustomFields = _custom.GetProfileCustomFields(profile).Result,
                Profile = profile,
                Posts = posts,
                Pager = pager
            };
        }

        public BlogCategoryModel GetPostsByCategory(string auth, string cat, int page, bool pub = false)
        {
            var pager = new Pager(page);
            IEnumerable<PostListItem> posts = _db.BlogPosts.ByCategory(cat, pager).Result;

            if (page < 1 || page > pager.LastPage)
                return null;

            if (pub) posts = SantizePostListItems(posts);
            var profile = _db.Profiles.Single(p => p.Slug == auth);

            return new BlogCategoryModel
            {
                Profile = profile,
                CustomFields = _custom.GetProfileCustomFields(profile).Result,
                Category = _db.Categories.Single(c => c.Slug == cat && c.ProfileId == profile.Id),
                Posts = posts,
                Pager = pager
            };
        }

        public BlogPostDetailModel GetPostBySlug(string slug, bool pub = false)
        {
            var vm = new BlogPostDetailModel();
            vm.BlogPost = _db.BlogPosts.SingleIncluded(p => p.Slug == slug && p.Published > DateTime.MinValue).Result;

            if (vm.BlogPost == null)
                return null;

            vm.Profile = vm.BlogPost.Profile;

            if (string.IsNullOrEmpty(vm.BlogPost.Image))
            {
                vm.BlogPost.Image = ApplicationSettings.ProfileImage;
                if (!string.IsNullOrEmpty(ApplicationSettings.PostImage)) { vm.BlogPost.Image = ApplicationSettings.PostImage; }
                if (!string.IsNullOrEmpty(vm.Profile.Image)) { vm.BlogPost.Image = vm.Profile.Image; }
            }

            vm.BlogCategories = new List<SelectListItem>();
            if (vm.BlogPost.PostCategories != null && vm.BlogPost.PostCategories.Count > 0)
            {
                foreach (var pc in vm.BlogPost.PostCategories)
                {
                    var cat = _db.Categories.Single(c => c.Id == pc.CategoryId);
                    vm.BlogCategories.Add(new SelectListItem { Value = cat.Slug, Text = cat.Title });
                }
            }
            vm.CustomFields = _custom.GetProfileCustomFields(vm.Profile).Result;

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
            var vm = new BlogPostsModel();
            vm.Pager = new Pager(page);
            vm.Posts = _search.Find(vm.Pager, term).Result;

            if (page < 1 || page > vm.Pager.LastPage)
                return null;

            if (pub) vm.Posts = SantizePostListItems(vm.Posts);
            vm.CustomFields = _custom.GetProfileCustomFields(null).Result;

            return vm;
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
