using App.Helpers;
using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Pages.Admin.Posts
{
    public class EditModel : AdminPageModel
    {
        [BindProperty]
        public PostItem PostItem { get; set; }
        public BlogItem Blog { get; set; }

        IDataService _db;
        INotificationService _ns;
        IEmailService _es;

        public EditModel(IDataService db, INotificationService ns, IEmailService es)
        {
            _db = db;
            _ns = ns;
            _es = es;
        }

        public async Task OnGetAsync(int id)
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            Notifications = await _ns.GetNotifications(author.Id);

            Blog = await _db.CustomFields.GetBlogSettings();
            PostItem = new PostItem { Author = author, Cover = Blog.Cover };

            if (id > 0)
                PostItem = await _db.BlogPosts.GetItem(p => p.Id == id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Error = ModelHelper.GetFirstValidationError(ModelState);
                return Page();
            }

            var user = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = user.IsAdmin;

            PostItem.Author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            

            if (ModelState.IsValid )
            {
                if (PostItem.Id > 0)
                { 
                    // post can be updated by admin, so use post author id
                    // instead of identity user name
                    var post = _db.BlogPosts.Single(p => p.Id == PostItem.Id);
                    if(post != null)
                    {
                        PostItem.Author = await _db.Authors.GetItem(a => a.Id == post.AuthorId);
                    }
                }
                //This is to prevent users from modifiyng other users or admin posts. -- manuta  9-16-2018
                if (IsAdmin || _db.Authors.Single(a => a.Id == PostItem.Author.Id).AppUserName == User.Identity.Name)
                {
                    var status = PostItem.Status;

                    if (status == SaveStatus.Publishing)
                        PostItem.Published = DateTime.UtcNow;

                    if (status == SaveStatus.Unpublishing)
                        PostItem.Published = DateTime.MinValue;

                    // fix for linux default datetime
                    if (PostItem.Published == DateTime.Parse("1/1/2001"))
                        PostItem.Published = DateTime.MinValue;

                    PostItem.Slug = await GetSlug(PostItem.Id, PostItem.Title);

                    var item = await _db.BlogPosts.SaveItem(PostItem);

                    PostItem = item;
                    Message = Resources.Saved;

                    if(status == SaveStatus.Publishing)
                    {
                        var siteUrl = $"{Request.Scheme}://{Request.Host}";
                        List<string> emails = _db.Newsletters.All().Select(n => n.Email).ToList();
                        await _es.SendNewsletters(PostItem, emails, siteUrl);
                    }

                    return Redirect($"~/admin/posts/edit?id={PostItem.Id}");
                }
            }
            return Page();
        }

        public async Task<string> GetSlug(int id, string title)
        {
            string slug = title.ToSlug();
            BlogPost post;

            if (id == 0)
                post = _db.BlogPosts.Single(p => p.Slug == slug);
            else
                post = _db.BlogPosts.Single(p => p.Slug == slug && p.Id != id);

            if (post == null)
                return await Task.FromResult(slug);

            for (int i = 2; i < 100; i++)
            {
                if (id == 0)
                    post = _db.BlogPosts.Single(p => p.Slug == $"{slug}{i}");
                else
                    post = _db.BlogPosts.Single(p => p.Slug == $"{slug}{i}" && p.Id != id);

                if (post == null)
                {
                    return await Task.FromResult(slug + i.ToString());
                }
            }

            return await Task.FromResult(slug);
        }
    }
}