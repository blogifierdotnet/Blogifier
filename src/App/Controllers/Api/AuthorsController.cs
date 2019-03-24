using Core.Data;
using Core.Data.Models;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        IDataService _data;
        UserManager<AppUser> _umgr;

        public AuthorsController(IDataService data, UserManager<AppUser> umgr)
        {
            _data = data;
            _umgr = umgr;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> Get(int page = 1)
        {
            try
            {
                var pager = new Pager(page);
                var authors = await _data.Authors.GetList(u => u.Created > DateTime.MinValue, pager);
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{author}")]
        public async Task<ActionResult<Author>> Get(string author)
        {
            try
            {
                var result = _data.Authors.Single(a => a.AppUserName == author);
                if (result == null) return NotFound();

                return Ok(await Task.FromResult(result));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

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
    }
}