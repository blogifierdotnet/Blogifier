using Core.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public class ItemMapper
    {
        AppDbContext _db;
        UserManager<AppUser> _um;
        readonly string _avatar = "lib/img/avatar.jpg";

        public ItemMapper(AppDbContext db, UserManager<AppUser> um)
        {
            _db = db;
            _um = um;
        }

        public List<PostItem> MapPostsToItems(List<BlogPost> posts)
        {
            return posts.Select(p => new PostItem
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                Description = p.Description,
                Content = p.Content,
                Cover = p.Cover,
                PostViews = p.PostViews,
                Rating = p.Rating,
                Published = p.Published,
                Author = (from usr in _um.Users
                    where usr.Id == p.UserId
                    select new AuthorItem
                    {
                        Id = usr.Id,
                        UserName = usr.UserName,
                        DisplayName = usr.DisplayName,
                        Avatar = usr.Avatar ?? _avatar,
                        Created = usr.Created
                    }).FirstOrDefault()
            }).Distinct().ToList();
        }

        public PostItem MapPostToItem(BlogPost p)
        {
            return new PostItem
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                Description = p.Description,
                Content = p.Content,
                Cover = p.Cover,
                PostViews = p.PostViews,
                Rating = p.Rating,
                Published = p.Published,
                Author = (from usr in _um.Users
                    where usr.Id == p.UserId
                    select new AuthorItem
                    {
                        Id = usr.Id,
                        UserName = usr.UserName,
                        DisplayName = usr.DisplayName,
                        Avatar = usr.Avatar ?? _avatar,
                        Created = usr.Created
                    }).FirstOrDefault()
            };
        }

        public List<AuthorItem> MapUsersToAuthors(List<AppUser> users)
        {
            return users.Select(u => new AuthorItem
            {
                Id = u.Id,
                UserName = u.UserName,
                DisplayName = string.IsNullOrEmpty(u.DisplayName) ? u.UserName : u.DisplayName,
                Email = u.Email,
                Avatar = u.Avatar ?? _avatar,
                Created = u.Created,
                IsAdmin = u.IsAdmin
            }).Distinct().ToList();
        }

        public AuthorItem MapUserToAuthor(AppUser user)
        {
            return new AuthorItem
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Avatar = user.Avatar ?? _avatar,
                Created = user.Created,
                IsAdmin = user.IsAdmin
            };
        }
    }
}