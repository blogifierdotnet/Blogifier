using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<string>> GetBlogCategories()
        {
            var blogCats = new List<string>();
            var profile = await GetProfile();
            var cats = await _db.Categories.Where(c => c.ProfileId == profile.Id).ToListAsync();
            foreach (var cat in cats)
            {
                blogCats.Add(cat.Title);
            }
            return blogCats;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryItem>> Get(int page)
        {
            var profile = await GetProfile();
            return await CategoryList.Items(_db, profile.Id);
        }

        [HttpGet("{slug}")]
        public async Task<IEnumerable<CategoryItem>> GetBySlug(string slug)
        {
            var post = await _db.BlogPosts.Single(p => p.Slug == slug);
            var postId = post == null ? 0 : post.Id;
            var profile = await GetProfile();

            return await CategoryList.Items(_db, profile.Id, postId);
        }

        [HttpGet("{id:int}")]
        public async Task<CategoryItem> GetById(int id)
        {
            return await GetItem(await _db.Categories.Single(c => c.Id == id));
        }

        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategory([FromBody]AdminCategoryModel model)
        {
            var profile = await GetProfile();
            var existing = await _db.Categories.Single(c => c.Title == model.Title && c.ProfileId == profile.Id);
            if (existing == null)
            {
                var newCategory = new Category
                {
                    ProfileId = profile.Id,
                    Title = model.Title,
                    Description = model.Title,
                    Slug = model.Title.ToSlug(),
                    LastUpdated = SystemClock.Now()
                };
                await _db.Categories.Add(newCategory);
                await _db.Complete();
                existing = await _db.Categories.Single(c => c.Title == model.Title && c.ProfileId == profile.Id);
            }
            var callback = new { existing.Id, existing.Title };
            return new CreatedResult("blogifier/api/categories/" + existing.Id, callback);
        }

        [HttpPut("addcategorytopost")]
        public async Task AddCategoryToPost([FromBody]AdminCategoryModel model)
        {
            var existing = await _db.Categories.Single(c => c.Title == model.Title);
            if(existing == null)
            {
                var profile = await GetProfile();
                var newCategory = new Category
                {
                    ProfileId = profile.Id,
                    Title = model.Title,
                    Description = model.Title,
                    Slug = model.Title.ToSlug(),
                    LastUpdated = SystemClock.Now()
                };
                await _db.Categories.Add(newCategory);
                await _db.Complete();

                existing = await _db.Categories.Single(c => c.Title == model.Title);
            }
            await _db.Categories.AddCategoryToPost(int.Parse(model.PostId), existing.Id);
            await _db.Complete();
        }

        [HttpPut("removecategoryfrompost")]
        public async Task RemoveCategoryFromPost([FromBody]AdminCategoryModel model)
        {
            await _db.Categories.RemoveCategoryFromPost(int.Parse(model.PostId), int.Parse(model.CategoryId));
            await _db.Complete();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]CategoryItem category)
        {
            var profile = await GetProfile();
            if (ModelState.IsValid)
            {
                int id = string.IsNullOrEmpty(category.Id) ? 0 : int.Parse(category.Id);
                if (id > 0)
                {
                    var existing = await _db.Categories.Single(c => c.Id == id);
                    if (existing == null)
                        return NotFound();

                    existing.Title = category.Title;
                    existing.Description = string.IsNullOrEmpty(category.Description) ? category.Title : category.Description;
                    existing.LastUpdated = SystemClock.Now();
                    await _db.Complete();
                }
                else
                {
                    var newCategory = new Category
                    {
                        ProfileId = profile.Id,
                        Title = category.Title,
                        Description = string.IsNullOrEmpty(category.Description) ? category.Title : category.Description,
                        Slug = category.Title.ToSlug(),
                        LastUpdated = SystemClock.Now()
                    };
                    await _db.Categories.Add(newCategory);
                    await _db.Complete();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _db.Categories.Single(c => c.Id == id);
            if (category == null)
                return NotFound();

            _db.Categories.Remove(category);
            await _db.Complete();
            return new NoContentResult();
        }

        async Task<CategoryItem> GetItem(Category category)
        {
            var vCount = 0;
            var pCount = 0;
            if (category.PostCategories != null && category.PostCategories.Count > 0)
            {
                pCount = category.PostCategories.Count;
                foreach (var pc in category.PostCategories)
                {
                    var blogPost = await _db.BlogPosts.Single(p => p.Id == pc.BlogPostId);
                    vCount += blogPost.PostViews;
                }
                await _db.Complete();
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

        async Task<Profile> GetProfile()
        {
            var key = "_BLOGIFIER_CACHE_BLOG_KEY";
            Profile publisher;
            if (_cache.TryGetValue(key, out publisher))
            {
                return publisher;
            }
            else
            {
                publisher = await _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
                _cache.Set(key, publisher,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(System.TimeSpan.FromHours(2)));
                return publisher;
            }
        }
    }
}
