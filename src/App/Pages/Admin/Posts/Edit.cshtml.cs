using App.Helpers;
using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Pages.Admin.Posts
{
    public class EditModel : AdminPageModel
    {
        [BindProperty]
        public PostItem PostItem { get; set; }

        IDataService _db;

        public EditModel(IDataService db)
        {
            _db = db;
        }

        public async Task OnGetAsync(int id)
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin; 
            //?? Was this going to be used someplace? 
            //Should this be removed   --Manuta 9-16-2018
            
            PostItem = new PostItem { Author = author, Cover = AppSettings.Cover };

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
                    if (PostItem.Status == SaveStatus.Publishing)
                        PostItem.Published = DateTime.UtcNow;

                    if (PostItem.Status == SaveStatus.Unpublishing)
                        PostItem.Published = DateTime.MinValue;

                    PostItem.Slug = await GetSlug(PostItem.Id, PostItem.Title);

                    var item = await _db.BlogPosts.SaveItem(PostItem);

                    PostItem = item;
                    Message = Resources.Saved;

                    return Redirect($"~/Admin/Posts/Edit?id={PostItem.Id}");
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