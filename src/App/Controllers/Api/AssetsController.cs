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
    public class AssetsController : ControllerBase
    {
        IDataService _data;
        IStorageService _store;

        public AssetsController(IDataService data, IStorageService store)
        {
            _data = data;
            _store = store;
        }

        public async Task<ActionResult<IList<string>>> Get(string path = "", int page = 1)
        {
            try
            {
                var results = _store.GetAssets(path);

                return results == null || results.Count > 0 ? 
                    Ok(await Task.FromResult(results)) : StatusCode(StatusCodes.Status404NotFound, "Not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}