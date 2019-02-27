using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using System.Linq;

namespace App.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        IDataService _data;
        IOptions<RequestLocalizationOptions> _options;

        public SettingsController(IDataService data, IOptions<RequestLocalizationOptions> options)
        {
            _data = data;
            _options = options;
        }

        [HttpGet("{cultures}")]
        public async Task<ActionResult<List<SelectListItem>>> GetCultures()
        {
            try
            {
                var results = _options.Value.SupportedUICultures
                    .Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName })
                    .ToList();

                return Ok(await Task.FromResult(results));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        public async Task<ActionResult<BlogItem>> Get()
        {
            try
            {
                var results = await _data.CustomFields.GetBlogSettings();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BlogItem>> Post(BlogItem model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data");

                await _data.CustomFields.SaveBlogSettings(model);
                var updated = await _data.CustomFields.GetBlogSettings();

                // set language culture here
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(updated.Culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
                return Created($"/api/settings", updated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}