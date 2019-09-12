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
using Core.Helpers;
using Microsoft.AspNetCore.Cors;

namespace Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        IDataService _data;
        IImportService _feed;
        IOptions<RequestLocalizationOptions> _options;

        public SettingsController(IDataService data, IImportService feed, IOptions<RequestLocalizationOptions> options)
        {
            _data = data;
            _feed = feed;
            _options = options;
        }

        /// <summary>
        /// Get list of cultures
        /// </summary>
        /// <returns>List of supported languages</returns>
        [HttpGet("cultures")]
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

        /// <summary>
        /// Get blog settings (CORS enabled)
        /// </summary>
        /// <returns>Blog settings</returns>
        [HttpGet]
        [EnableCors("AllowOrigin")]
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

        /// <summary>
        /// Save blog settings (admins only)
        /// </summary>
        /// <param name="model">Blog settings item</param>
        /// <returns>Saved blog item</returns>
        [HttpPost]
        [Administrator]
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

        /// <summary>
        /// Import posts from RSS feed (admins only)
        /// </summary>
        /// <param name="file">XML file</param>
        /// <returns>List of messages</returns>
        [HttpPost("importfeed")]
        [Administrator]
        public async Task<IEnumerable<ImportMessage>> ImportFeed(IFormFile file)
        {
            var author = _data.Authors.Single(a => a.AppUserName == User.Identity.Name);

            if (!author.IsAdmin)
                throw new ApplicationException("Unauthorized");

            var webRoot = Url.Content("~/");

            return await _feed.Import(file, User.Identity.Name, webRoot);
        }

        /// <summary>
        /// Remove notification (admins only)
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Completed task</returns>
        [HttpDelete("removenotification/{id}")]
        [Administrator]
        public async Task RemoveNotification(int id)
        {
            var note = _data.Notifications.Single(n => n.Id == id);
            _data.Notifications.Remove(note);
            _data.Complete();
            await Task.CompletedTask;
        }
    }
}