using Core.Data;
using Core.Helpers;
using Core.Services;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        IDataService _data;

        public PostsController(IDataService data)
        {
            _data = data;
        }

        /// <summary>
        /// Search blog posts by term
        /// </summary>
        /// <param name="term">Search term</param>
        /// <param name="author">Author</param>
        /// <param name="include">Posts to include: all by default; F - featured, D - drafts, P - published</param>
        /// <param name="page">Page number</param>
        /// <param name="format">Otput format: html or markdown; default = html;</param>
        /// <returns>Model with list of posts and pager</returns>
        [HttpGet("search/{term}")]
        public async Task<ActionResult<PageListModel>> Search(
            string term, 
            [FromQuery]string author = "",
            [FromQuery]string include = "",
            [FromQuery]int page = 1,
            [FromQuery]string format = "html")
        {
            try
            {
                var blog = await _data.CustomFields.GetBlogSettings();
                IEnumerable<PostItem> results;
                var pager = new Pager(page, blog.ItemsPerPage);
                var authorId = GetUserId(author);

                results = await _data.BlogPosts.Search(pager, term, authorId, include, !User.Identity.IsAuthenticated);

                if(format.ToUpper() == "HTML")
                {
                    foreach (var p in results)
                    {
                        p.Description = Markdown.ToHtml(p.Description);
                        p.Content = Markdown.ToHtml(p.Content);
                    }
                }

                return Ok(new PageListModel { Posts = results, Pager = pager });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        /// <summary>
        /// Get list of blog posts
        /// </summary>
        /// <param name="author">Post author</param>
        /// <param name="category">Post category</param>
        /// <param name="include">Posts to include: all by default; F - featured, D - drafts, P - published</param>
        /// <param name="page">Page number</param>
        /// <param name="format">Otput format: html or markdown; default = html;</param>
        /// <returns>Model with list of posts and pager</returns>
        [HttpGet]
        public async Task<ActionResult<PageListModel>> Get(
            [FromQuery]string author = "",
            [FromQuery]string category = "",
            [FromQuery]string include = "",
            [FromQuery]int page = 1,
            [FromQuery]string format = "html")
        {
            try
            {
                var blog = await _data.CustomFields.GetBlogSettings();
                IEnumerable<PostItem> results;
                var pager = new Pager(page, blog.ItemsPerPage);
                int authorId = GetUserId(author);

                results = await _data.BlogPosts.GetList(pager, authorId, category, include, !User.Identity.IsAuthenticated);

                if (format.ToUpper() == "HTML")
                {
                    foreach (var p in results)
                    {
                        p.Description = Markdown.ToHtml(p.Description);
                        p.Content = Markdown.ToHtml(p.Content);
                    }
                }

                return Ok(new PageListModel { Posts = results, Pager = pager });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        /// <summary>
        /// Get single post by ID
        /// </summary>
        /// <param name="id">Post ID</param>
        /// <param name="format">Otput format: html or markdown; default = html;</param>
        /// <returns>Post item</returns>
        [HttpGet("{id}")]
        public async Task<PostItem> GetPost(int id, [FromQuery]string format = "html")
        {
            if (id > 0)
            {
                var post = await _data.BlogPosts.GetItem(p => p.Id == id, !User.Identity.IsAuthenticated);
                if (format.ToUpper() == "HTML")
                {
                    post.Description = Markdown.ToHtml(post.Description);
                    post.Content = Markdown.ToHtml(post.Content);
                }
                return post;
            }
            else
            {
                var author = await _data.Authors.GetItem(a => a.AppUserName == User.Identity.Name, !User.Identity.IsAuthenticated);
                var blog = await _data.CustomFields.GetBlogSettings();
                return new PostItem { Author = author, Cover = blog.Cover };
            }               
        }

        /// <summary>
        /// Set post as published or draft (authentication required)
        /// </summary>
        /// <param name="id">Post ID</param>
        /// <param name="flag">Flag; P - publish, U - unpublish</param>
        /// <returns>Success of failure</returns>
        [HttpPut("publish")]
        [Authorize]
        public async Task<ActionResult> Publish(int id, string flag)
        {
            try
            {
                var post = _data.BlogPosts.Single(p => p.Id == id);
                var author = _data.Authors.Single(a => a.Id == post.AuthorId);
                var user = _data.Authors.Single(a => a.AppUserName == User.Identity.Name);
                if (!string.IsNullOrEmpty(flag) && (user.IsAdmin || author.AppUserName == User.Identity.Name))
                {
                    if (flag == "P") post.Published = DateTime.UtcNow;
                    if (flag == "U") post.Published = DateTime.MinValue;
                    _data.Complete();
                }
                await Task.CompletedTask;

                return Ok(Resources.Updated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Set post as featured (admins only)
        /// </summary>
        /// <param name="id">Post ID</param>
        /// <param name="flag">Flag; F - featured, U - remove from featured</param>
        /// <returns></returns>
        [HttpPut("feature")]
        [Administrator]
        public async Task<ActionResult> Feature(int id, string flag)
        {
            try
            {
                var post = _data.BlogPosts.Single(p => p.Id == id);
                var author = _data.Authors.Single(a => a.Id == post.AuthorId);
                var user = _data.Authors.Single(a => a.AppUserName == User.Identity.Name);
                if (!string.IsNullOrEmpty(flag) && (user.IsAdmin || author.AppUserName == User.Identity.Name))
                {
                    if (flag == "F") post.IsFeatured = true;
                    if (flag == "U") post.IsFeatured = false;
                    _data.Complete();
                }
                await Task.CompletedTask;

                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Save blog post (authentication required)
        /// </summary>
        /// <param name="post">Post item</param>
        /// <returns>Saved post item</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostItem>> Post(PostItem post)
        {
            try
            {
                post.Slug = await GetSlug(post.Id, post.Title);
                var saved = await _data.BlogPosts.SaveItem(post);
                return Created($"admin/posts/edit?id={saved.Id}", saved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Remove post item (authentication required)
        /// </summary>
        /// <param name="id">Post ID</param>
        /// <returns>Success or failure</returns>
        [HttpDelete("remove/{id}")]
        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {           
            try
            {
                var post = _data.BlogPosts.Single(p => p.Id == id);
                var author = _data.Authors.Single(a => a.Id == post.AuthorId);
                var user = _data.Authors.Single(a => a.AppUserName == User.Identity.Name);

                if (user.IsAdmin || author.AppUserName == User.Identity.Name)
                {
                    _data.BlogPosts.Remove(post);
                    _data.Complete();
                }    
                await Task.CompletedTask;

                return Ok(Resources.Removed);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        async Task<string> GetSlug(int id, string title)
        {
            string slug = title.ToSlug();
            BlogPost post;

            if (id == 0)
                post = _data.BlogPosts.Single(p => p.Slug == slug);
            else
                post = _data.BlogPosts.Single(p => p.Slug == slug && p.Id != id);

            if (post == null)
                return await Task.FromResult(slug);

            for (int i = 2; i < 100; i++)
            {
                if (id == 0)
                    post = _data.BlogPosts.Single(p => p.Slug == $"{slug}{i}");
                else
                    post = _data.BlogPosts.Single(p => p.Slug == $"{slug}{i}" && p.Id != id);

                if (post == null)
                {
                    return await Task.FromResult(slug + i.ToString());
                }
            }

            return await Task.FromResult(slug);
        }

        int GetUserId(string author)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(author))
            {
                var objAuthor = _data.Authors.Single(a => a.AppUserName == author);
                if (objAuthor != null)
                    id = objAuthor.Id;
            }
            return id;
        }
    }
}