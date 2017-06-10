using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public PostEditModel Index([FromBody]PostEditModel model)
        {
            BlogPost bp;
            if (model.Id == 0)
            {
                var blog = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
                bp = new BlogPost();
                bp.ProfileId = blog.Id;
                bp.Title = model.Title;
                bp.Slug = model.Title.ToSlug();
                bp.Content = model.Content;
                bp.LastUpdated = SystemClock.Now();
                bp.Description = model.Content.ToDescription();
                _db.BlogPosts.Add(bp);
            }
            else
            {
                bp = _db.BlogPosts.Single(p => p.Id == model.Id);
                bp.Title = model.Title;
                bp.Content = model.Content;
                bp.Description = model.Content.ToDescription();
            }
            _db.Complete();

            model.Id = bp.Id;
            model.Slug = bp.Slug;

            return model;
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

            post.Published = System.DateTime.MinValue;
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
    }
}
