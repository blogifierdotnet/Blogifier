using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Blogifier.Core.Controllers.Api
{
    [Authorize]
    [Route("blogifier/api/[controller]")]
    public class CategoriesController : Controller
    {
        IUnitOfWork _db;
        IMemoryCache _cache;

        public CategoriesController(IUnitOfWork db, IMemoryCache memoryCache)
        {
            _db = db;
            _cache = memoryCache;
        }

        [HttpGet("blogcategories")]
        public IEnumerable<string> GetBlogCategories()
        {
            var blogCats = new List<string>();
            var cats = _db.Categories.Find(c => c.ProfileId == GetProfile().Id);
            foreach (var cat in cats)
            {
                blogCats.Add(cat.Title);
            }
            return blogCats;
        }

        [HttpGet]
        public IEnumerable<CategoryItem> Get(int page)
        {
            return CategoryList.Items(_db, GetProfile().Id);
        }

        [HttpGet("{slug}")]
        public IEnumerable<CategoryItem> GetBySlug(string slug)
        {
            var post = _db.BlogPosts.Single(p => p.Slug == slug);
            var postId = post == null ? 0 : post.Id;

            return CategoryList.Items(_db, GetProfile().Id, postId);
        }

        [HttpGet("{id:int}")]
        public CategoryItem GetById(int id)
        {
            return GetItem(_db.Categories.Single(c => c.Id == id));
        }

        [HttpPost("addcategory")]
        public IActionResult AddCategory([FromBody]AdminCategoryModel model)
        {
            var existing = _db.Categories.Single(c => c.Title == model.Title);
            if (existing == null)
            {
                var newCategory = new Category
                {
                    ProfileId = GetProfile().Id,
                    Title = model.Title,
                    Description = model.Title,
                    Slug = model.Title.ToSlug(),
                    LastUpdated = SystemClock.Now()
                };
                _db.Categories.Add(newCategory);
                _db.Complete();
                existing = _db.Categories.Single(c => c.Title == model.Title);
            }
            var callback = new { Id = existing.Id, Title = existing.Title };
            return new CreatedResult("blogifier/api/categories/" + existing.Id, callback);
        }

        [HttpPut("addcategorytopost")]
        public void AddCategoryToPost([FromBody]AdminCategoryModel model)
        {
            var existing = _db.Categories.Single(c => c.Title == model.Title);
            if(existing == null)
            {
                var newCategory = new Category
                {
                    ProfileId = GetProfile().Id,
                    Title = model.Title,
                    Description = model.Title,
                    Slug = model.Title.ToSlug(),
                    LastUpdated = SystemClock.Now()
                };
                _db.Categories.Add(newCategory);
                _db.Complete();

                existing = _db.Categories.Single(c => c.Title == model.Title);
            }
            _db.Categories.AddCategoryToPost(int.Parse(model.PostId), existing.Id);
            _db.Complete();
        }

        [HttpPut("removecategoryfrompost")]
        public void RemoveCategoryFromPost([FromBody]AdminCategoryModel model)
        {
            _db.Categories.RemoveCategoryFromPost(int.Parse(model.PostId), int.Parse(model.CategoryId));
            _db.Complete();
        }

        [HttpPut]
        public IActionResult Put([FromBody]CategoryItem category)
        {
            var blog = GetProfile();
            if (ModelState.IsValid)
            {
                int id = string.IsNullOrEmpty(category.Id) ? 0 : int.Parse(category.Id);
                if (id > 0)
                {
                    var existing = _db.Categories.Single(c => c.Id == id);
                    if (existing == null)
                        return NotFound();

                    existing.Title = category.Title;
                    existing.Description = string.IsNullOrEmpty(category.Description) ? category.Title : category.Description;
                    existing.LastUpdated = SystemClock.Now();
                    _db.Complete();
                }
                else
                {
                    var newCategory = new Category
                    {
                        ProfileId = blog.Id,
                        Title = category.Title,
                        Description = string.IsNullOrEmpty(category.Description) ? category.Title : category.Description,
                        Slug = category.Title.ToSlug(),
                        LastUpdated = SystemClock.Now()
                    };
                    _db.Categories.Add(newCategory);
                    _db.Complete();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.Single(c => c.Id == id);
            if (category == null)
                return NotFound();

            _db.Categories.Remove(category);
            _db.Complete();
            return new NoContentResult();
        }

        CategoryItem GetItem(Category category)
        {
            var vCount = 0;
            var pCount = 0;
            if (category.PostCategories != null && category.PostCategories.Count > 0)
            {
                pCount = category.PostCategories.Count;
                foreach (var pc in category.PostCategories)
                {
                    vCount += _db.BlogPosts.Single(p => p.Id == pc.BlogPostId).PostViews;
                }
                _db.Complete();
            }
            return new CategoryItem
            {
                Id = category.Id.ToString(),
                Title = category.Title,
                Description = category.Description,
                Selected = false,
                PostCount = pCount,
                ViewCount = vCount
            };
        }

        Profile GetProfile()
        {
            var key = "_BLOGIFIER_CACHE_BLOG_KEY";
            Profile publisher;
            if (_cache.TryGetValue(key, out publisher))
            {
                return publisher;
            }
            else
            {
                publisher = _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
                _cache.Set(key, publisher,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(System.TimeSpan.FromHours(2)));
                return publisher;
            }
        }
    }
}
