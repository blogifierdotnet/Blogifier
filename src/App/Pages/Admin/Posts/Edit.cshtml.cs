using App.Helpers;
using Core;
using Core.Data;
using Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Pages.Admin.Posts
{
    public class EditModel : AdminPageModel
    {
        [BindProperty]
        public PostItem PostItem { get; set; }

        IUnitOfWork _db;

        public EditModel(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task OnGetAsync(int id)
        {
            PostItem = new PostItem { Author = await GetAuthor(), Cover = AppSettings.Cover };

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

            PostItem.Author = await GetAuthor();

            if (ModelState.IsValid)
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

            return Page();
        }

        async Task<Author> GetAuthor()
        {
            return await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
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