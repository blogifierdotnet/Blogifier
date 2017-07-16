using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Blogifier.Core.Controllers.Api
{
    [Authorize]
    [Route("blogifier/api/[controller]")]
    public class PostsController : Controller
    {
        IUnitOfWork _db;

        public PostsController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpGet("{page:int?}")]
        public AdminPostList Index(int page)
        {
            var pager = new Pager(page);
            var model = new AdminPostList();

            model.BlogPosts = _db.BlogPosts.Find(p => p.Profile.IdentityName == User.Identity.Name, pager);
            model.Pager = pager;
            return model;
        }

        [HttpGet("post/{id:int}")]
        public PostEditModel GetById(int id)
        {
            var post = _db.BlogPosts.SingleIncluded(p => p.Id == id).Result;
            if (id < 1)
            {
                post = _db.BlogPosts.SingleIncluded(p => p.Profile.IdentityName == User.Identity.Name).Result;
            }
            var model = new PostEditModel
            {
                Id = post.Id,
                Slug = post.Slug,
                Title = post.Title,
                Content = post.Content,
                Published = post.Published,
                Image = post.Image,
                Categories = _db.Categories.PostCategories(post.Id)
            };
            return model;
        }

        [HttpPost]
        public IActionResult Index([FromBody]PostEditModel model)
        {
            BlogPost bp;
            if (model.Id == 0)
            {
                var blog = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
                bp = new BlogPost();
                bp.ProfileId = blog.Id;
                bp.Title = model.Title;
                bp.Slug = GetSlug(model.Title, model.Id);
                bp.Content = model.Content;
                bp.LastUpdated = SystemClock.Now();
                bp.Description = model.Content.ToDescription();
                bp.Published = model.IsPublished ? SystemClock.Now() : DateTime.MinValue;
                bp.Image = model.Image;
                _db.BlogPosts.Add(bp);
            }
            else
            {
                bp = _db.BlogPosts.Single(p => p.Id == model.Id);
                bp.Title = model.Title;
                bp.Slug = GetSlug(model.Title, model.Id);
                bp.Content = model.Content;
                bp.Description = model.Content.ToDescription();
                // when publish button clicked, save and publish
                // but do not unpublish - use unpublish/{id} for this
                if (model.IsPublished) { bp.Published = SystemClock.Now(); }
                bp.Image = model.Image;
            }
            _db.Complete();

            if(model.Categories != null && model.Categories.Count() > 0)
            {
                _db.BlogPosts.UpdatePostCategories(
                    bp.Id, model.Categories.Select(c => c.Value).ToList());
                _db.Complete();
            }
            var callback = new { Id = bp.Id, Slug = bp.Slug, Published = bp.Published, Image = bp.Image };
            return new CreatedResult("blogifier/api/posts/" + bp.Id, callback);
        }

        [HttpPut("publish/{id:int}")]
        public IActionResult Publish(int id)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            if (post == null)
                return NotFound();

            post.Published = SystemClock.Now();
            _db.Complete();
            return new NoContentResult();
        }

        [HttpPut("unpublish/{id:int}")]
        public IActionResult Unpublish(int id)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            if (post == null)
                return NotFound();

            post.Published = DateTime.MinValue;
            _db.Complete();
            return new NoContentResult();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            if (post == null)
                return NotFound();

            _db.BlogPosts.Remove(post);
            _db.Complete();
            return new NoContentResult();
        }

        string GetSlug(string title, int id)
        {
            var slug = title.ToSlug();
            var cnt = 2;

            var post = _db.BlogPosts.Single(p => p.Slug == slug);
            if(post == null || post.Id == id)
                return slug;

            while (cnt < 100)
            {
                var newSlug = string.Format("{0}{1}", slug, cnt);
                if (_db.BlogPosts.Single(p => p.Slug == newSlug) == null)
                    return newSlug;
                cnt++;
            }
            return slug;
        }
    }
}
