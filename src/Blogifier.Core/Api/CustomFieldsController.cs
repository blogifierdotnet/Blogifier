using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomFieldsController : ControllerBase
    {
        IDataService _data;

        public CustomFieldsController(IDataService data)
        {
            _data = data;
        }

        [HttpGet("listbyid/{id:int}")]
        public IEnumerable<CustomField> GetField(int id)
        {
            return _data.CustomFields.Find(f => f.AuthorId == id);
        }

        [HttpPost]
        [Administrator]
        public async Task<ActionResult<CustomField>> Post(CustomField model)
        {
            try
            {
                var created = await _data.CustomFields.SaveCustomField(model);
                return Created($"/api/customfields/{created.Id}", created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
