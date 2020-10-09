﻿using Blogifier.Models;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Data;

namespace Blogifier.Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        IDataService _data;
        IStorageService _store;
        UserManager<AppUser> _umgr;
        SignInManager<AppUser> _smgr;

        public AuthorsController(IDataService data, IStorageService store, UserManager<AppUser> umgr, SignInManager<AppUser> smgr)
        {
            _data = data;
            _umgr = umgr;
            _smgr = smgr;
            _store = store;
        }

        /// <summary>
        /// Get list of blog authors (CORS enabled)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <returns>List of authors</returns>
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<Author>>> Get(int page = 1)
        {
            try
            {
                var pager = new Pager(page);
                var authors = await _data.Authors.GetList(u => u.Created > DateTime.MinValue, pager, !User.Identity.IsAuthenticated);
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get single author by name
        /// </summary>
        /// <param name="author">Author name used during registration</param>
        /// <returns>Author object</returns>
        [HttpGet("{author}")]
        public async Task<ActionResult<Author>> Get(string author)
        {
            try
            {
                var result = await _data.Authors.GetItem(a => a.AppUserName == author, !User.Identity.IsAuthenticated);
                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        /// <summary>
        /// Register new author (admins only)
        /// </summary>
        /// <param name="model">Author model</param>
        /// <returns>Created Author object</returns>
        [HttpPost]
        [Administrator]
        public async Task<ActionResult<Author>> Post(RegisterModel model)
        {
            try
            {
                var existing = _data.Authors.Single(a => a.AppUserName == model.UserName);
                if (existing != null)
                    return BadRequest("User already exists");

                if (!ModelState.IsValid)
                    return BadRequest("Invalid data");

                // register new app user account
                var result = await _umgr.CreateAsync(new AppUser { UserName = model.UserName, Email = model.Email }, model.Password);

                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new user");

                // add user as author to app database
                var user = new Author
                {
                    AppUserName = model.UserName,
                    Email = model.Email,
                    IsAdmin = model.SetAsAdmin
                };
                await _data.Authors.Save(user);
                var created = _data.Authors.Single(a => a.AppUserName == model.UserName);

                return Created($"/api/authors/{user.AppUserName}", created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update author (authentication required)
        /// </summary>
        /// <param name="model">Author model</param>
        /// <returns>Success or 500 error</returns>
        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult> Update(Author model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data");

                // can only update own profile or should be admin
                if(model.AppUserName != User.Identity.Name)
                {
                    var loggedIn = _data.Authors.Single(a => a.AppUserName == User.Identity.Name);
                    if(loggedIn == null || !loggedIn.IsAdmin)
                    {
                        return Unauthorized("Not authorized");
                    }
                }

                await _data.Authors.Save(model);

                return Ok(Resources.Updated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Change author password (authentication required)
        /// </summary>
        /// <param name="model">Author model</param>
        /// <returns>Success or 500 error</returns>
        [HttpPut("changepwd")]
        [Authorize]
        public async Task<ActionResult> ChangePwd(ChangePasswordModel model)
        {
            try
            {
                if (AppSettings.DemoMode)
                {
                    return Ok(Resources.Updated);
                }
                var user = await _umgr.GetUserAsync(User);
                var result = await _umgr.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error changing password");
                }
                await _smgr.SignInAsync(user, isPersistent: false);
                return Ok(Resources.Updated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete author, from membership, database and file system (admins only)
        /// </summary>
        /// <param name="id">Author ID</param>
        /// <returns>Success or 500 error</returns>
        [Administrator]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var author = await _data.Authors.GetItem(u => u.AppUserName == id);

                // remove posts
                var posts = _data.BlogPosts.All().Where(p => p.AuthorId == author.Id).ToList();
                _data.BlogPosts.RemoveRange(posts);

                // remove author
                _data.Authors.Remove(author);
                _data.Complete();

                // remove files
                _store.DeleteAuthor(id);

                // remove user
                var user = await _umgr.FindByNameAsync(author.AppUserName);
                await _umgr.DeleteAsync(user);

                return Ok(Resources.Removed);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}