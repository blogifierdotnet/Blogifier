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
    public class AuthorsController : ControllerBase
    {
        IDataService _data;

        public AuthorsController(IDataService data)
        {
            _data = data;
        }

        public async Task<ActionResult<IEnumerable<Author>>> Get(int page = 1)
        {
            try
            {
                var results = _data.Authors.All();

                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}