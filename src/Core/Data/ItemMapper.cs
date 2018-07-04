using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public class ItemMapper
    {
        AppDbContext _db;
        readonly string _avatar = "lib/img/avatar.jpg";

        public ItemMapper(AppDbContext db)
        {
            _db = db;
        }

        //public List<PostItem> MapPostsToItems(List<BlogPost> posts)
        //{
        //    return posts.Select(p => new PostItem
        //    {
        //        Id = p.Id,
        //        Slug = p.Slug,
        //        Title = p.Title,
        //        Description = p.Description,
        //        Content = p.Content,
        //        Cover = p.Cover,
        //        PostViews = p.PostViews,
        //        Rating = p.Rating,
        //        Published = p.Published,
        //        Author = _db.Authors.Single(a => a.Id == p.AuthorId)
        //    }).Distinct().ToList();
        //}

        //public PostItem MapPostToItem(BlogPost p)
        //{
        //    return new PostItem
        //    {
        //        Id = p.Id,
        //        Slug = p.Slug,
        //        Title = p.Title,
        //        Description = p.Description,
        //        Content = p.Content,
        //        Cover = p.Cover,
        //        PostViews = p.PostViews,
        //        Rating = p.Rating,
        //        Published = p.Published,
        //        Author = _db.Authors.Single(a => a.Id == p.AuthorId)
        //    };
        //}

        //public List<Author> MapUsersToAuthors(List<AppUser> users)
        //{
        //    return users.Select(u => new Author
        //    {
        //        AppUserId = u.Id,
        //        AppUserName = u.UserName,
        //        DisplayName = string.IsNullOrEmpty(u.DisplayName) ? u.UserName : u.DisplayName,
        //        Email = u.Email,
        //        Avatar = u.Avatar ?? _avatar,
        //        Created = u.Created,
        //        IsAdmin = u.IsAdmin
        //    }).Distinct().ToList();
        //}

        //public Author MapUserToAuthor(AppUser user)
        //{
        //    return new Author
        //    {
        //        AppUserId = user.Id,
        //        AppUserName = user.UserName
        //        //Email = user.Email
        //        //DisplayName = user.DisplayName,
        //        //Avatar = user.Avatar ?? _avatar,
        //        //Created = user.Created,
        //        //IsAdmin = user.IsAdmin
        //    };
        //}
    }
}