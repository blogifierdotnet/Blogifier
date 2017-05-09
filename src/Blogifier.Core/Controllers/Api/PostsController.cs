using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("{page:int}")]
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
                var desc = model.Content.StripHtml();
                bp.ProfileId = blog.Id;
                bp.Title = model.Title;
                bp.Slug = model.Title.ToSlug();
                bp.Content = model.Content;
                bp.LastUpdated = SystemClock.Now();
                bp.Description = desc.Length > 300 ? desc.Substring(0, 300) : desc;
                _db.BlogPosts.Add(bp);
            }
            else
            {
                bp = _db.BlogPosts.Single(p => p.Id == model.Id);
                bp.Title = model.Title;
                bp.Content = model.Content;
            }
            _db.Complete();

            model.Id = bp.Id;
            model.Slug = bp.Slug;

            return model;
        }

        [HttpPut("publish/{id:int}")]
        public void Publish(int id)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            post.Published = SystemClock.Now();
            _db.Complete();
        }

        [HttpPut("unpublish/{id:int}")]
        public void Unpublish(int id)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            post.Published = System.DateTime.MinValue;
            _db.Complete();
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            _db.BlogPosts.Remove(post);
            _db.Complete();
        }
    }
}
