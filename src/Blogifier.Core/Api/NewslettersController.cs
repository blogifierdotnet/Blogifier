using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blogifier.Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewslettersController: ControllerBase
    {
        IDataService _data;

        public NewslettersController(IDataService data)
        {
            _data = data;
        }

        [Administrator]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existing = _data.Newsletters.Single(n => n.Id == id);
                if (existing != null)
                {
                    _data.Newsletters.Remove(existing);
                    _data.Complete();
                }
                return Ok(Resources.Removed);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
