using Core.Data;
using Core.Helpers;
using Core.Services;
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

        public async Task<ActionResult<PageListModel>> Get([FromQuery]string term = "", [FromQuery]string status = "", [FromQuery]int page = 1)
        {
            try
            {
                var blog = await _data.CustomFields.GetBlogSettings();
                var pager = new Pager(page, blog.ItemsPerPage);
                var author = _data.Authors.Single(a => a.AppUserName == User.Identity.Name);
                IEnumerable<PostItem> results;

                if(!string.IsNullOrEmpty(term))
                {
                    results = author.IsAdmin ? 
                        await _data.BlogPosts.Search(pager, term) :
                        await _data.BlogPosts.Search(pager, term, author.Id);
                }
                else
                {
                    if(!author.IsAdmin)
                    {
                        if(status == "P")
                            results = await _data.BlogPosts.GetList(p => p.Published > DateTime.MinValue && p.AuthorId == author.Id, pager);
                        else if(status == "D")
                            results = await _data.BlogPosts.GetList(p => p.Published == DateTime.MinValue && p.AuthorId == author.Id, pager);
                        else
                            results = await _data.BlogPosts.GetList(p => p.AuthorId == author.Id, pager);  
                    }
                    else
                    {
                        if(status == "P")
                            results = await _data.BlogPosts.GetList(p => p.Published > DateTime.MinValue, pager);
                        else if(status == "D")
                            results = await _data.BlogPosts.GetList(p => p.Published == DateTime.MinValue, pager);
                        else
                            results = await _data.BlogPosts.GetList(p => p.Id > 0, pager);
                    }                
                }
                return Ok(new PageListModel { Posts = results, Pager = pager });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

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

                return Ok(Resources.Updated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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
    }
}