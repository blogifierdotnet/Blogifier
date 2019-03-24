using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers.Api
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

        public async Task<ActionResult<IEnumerable<BlogPost>>> Get(int author = 0, int page = 1)
        {
            try
            {
                var results = author == 0 ? 
                    _data.BlogPosts.All() : _data.BlogPosts.Find(p => p.AuthorId == author);

                return Ok(await Task.FromResult(results));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}