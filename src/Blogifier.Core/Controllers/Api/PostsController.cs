using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers.Api
{
    [Authorize]
    [Route("blogifier/api/[controller]")]
    class PostsController : Controller
    {
        IUnitOfWork _db;

        public PostsController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpGet("{page:int}")]
        public async Task<IEnumerable<PostListItem>> Index(int page)
        {
            var pager = new Pager(page);
            var blog = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);

            return await _db.BlogPosts.Find(p => p.Profile.IdentityName == blog.IdentityName, pager);
        }

        [HttpGet("post/{id:int}")]
        public PostEditModel GetById(int id)
        {
            var post = _db.BlogPosts.SingleIncluded(p => p.Id == id).Result;
            var model = new PostEditModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Published = post.Published == System.DateTime.MinValue ? false : true,
                Categories = new List<int>()
            };
            if (post.PostCategories != null && post.PostCategories.Count > 0)
            {
                foreach (var pc in post.PostCategories)
                {
                    model.Categories.Add(pc.CategoryId);
                }
            }
            return model;
        }

        [HttpPost]
        public void Post([FromBody]PostEditModel model)
        {
            BlogPost post;
            if (model.Id == 0)
            {
                var blog = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
                post = new BlogPost();
                var desc = model.Content.StripHtml();
                post.ProfileId = blog.Id;
                post.Title = model.Title;
                post.Slug = model.Title.ToSlug();
                post.Content = model.Content;
                post.LastUpdated = SystemClock.Now();
                post.Published = model.Published ? SystemClock.Now() : System.DateTime.MinValue;
                post.Description = desc.Length > 300 ? desc.Substring(0, 300) : desc;
                _db.BlogPosts.Add(post);
            }
            else
            {
                post = _db.BlogPosts.Single(p => p.Id == model.Id);
                post.Title = model.Title;
                post.Content = model.Content;
                post.Published = model.Published ? SystemClock.Now() : System.DateTime.MinValue;
            }
            _db.Complete();

            // handle post categories
            var savedPost = _db.BlogPosts.Single(p => p.Slug == post.Slug);
            if (savedPost != null)
            {
                var cats = new List<string>();
                if (model.Categories != null && model.Categories.Count > 0)
                {
                    foreach (var item in model.Categories)
                    {
                        cats.Add(item.ToString());
                    }
                }
                _db.BlogPosts.UpdatePostCategories(savedPost.Id, cats);
                _db.Complete();
            }
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
