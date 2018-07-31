using Core;
using Core.Data;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        IUnitOfWork _db;
        ISearchService _ss;

        public ContentController(IUnitOfWork db, ISearchService ss)
        {
            _db = db;
            _ss = ss;
        }

        public async Task<IActionResult> Index(int page = 1, string status = "A", string search = "")
        {
            var author = await GetAuthor();
            Expression<Func<BlogPost, bool>> predicate = p => p.AuthorId == author.Id;
            var pager = new Pager(page);
            IEnumerable<PostItem> posts;

            if (!string.IsNullOrEmpty(search))
            {
                posts = await _ss.Find(pager, search);
            }
            else
            {
                if (status == "P")
                    predicate = p => p.Published > DateTime.MinValue && p.AuthorId == author.Id;
                if (status == "D")
                    predicate = p => p.Published == DateTime.MinValue && p.AuthorId == author.Id;

                posts = await _db.BlogPosts.Find(predicate, pager);
            }

            return View(new PostListModel { Posts = posts, Pager = pager });
        }

        public async Task<IActionResult> Edit(string slug = "", string msg = "")
        {
            var post = new PostItem { Author = await GetAuthor(), Cover = AppSettings.Cover };

            if (!string.IsNullOrEmpty(slug))
                post = await _db.BlogPosts.GetItem(p => p.Slug == slug);

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostItem model)
        {
            model.Author = await GetAuthor();

            if (ModelState.IsValid)
            {
                if (model.Status == SaveStatus.Publishing)
                    model.Published = DateTime.UtcNow;

                if (model.Status == SaveStatus.Unpublishing)
                    model.Published = DateTime.MinValue;

                model.Slug = await GetSlug(model.Id, model.Title);

                var item = await _db.BlogPosts.SaveItem(model);

                return RedirectToAction(nameof(Edit), new { slug = item.Slug, msg = "ok" });
            }

            return View(model);
        }

        [HttpPut]
        public IActionResult Publish(int id, string flag)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            if (!string.IsNullOrEmpty(flag))
            {
                if (flag == "P") post.Published = DateTime.UtcNow;
                if (flag == "U") post.Published = DateTime.MinValue;
                _db.Complete();
            }
            return Redirect("~/content");
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            _db.BlogPosts.Remove(post);
            _db.Complete();

            await Task.CompletedTask;
            return Redirect("~/content");
        }

        async Task<Author> GetAuthor()
        {
            return await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
        }

        public async Task<string> GetSlug(int id, string title)
        {
            string slug = title.ToSlug();
            BlogPost post;

            if(id == 0)
                post = _db.BlogPosts.Single(p => p.Slug == slug);
            else
                post = _db.BlogPosts.Single(p => p.Slug == slug && p.Id != id);

            if(post == null)
                return await Task.FromResult(slug);

            for(int i = 2; i < 100; i++)
            {
                if(id == 0)
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