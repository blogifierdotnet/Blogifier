using Core.Data;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        IDataService _data;
        IStorageService _store;
        BlogItem _blog;
        string slash = Path.DirectorySeparatorChar.ToString();

        public ThemesController(IDataService data, IStorageService store)
        {
            _data = data;
            _store = store;
        }

        /// <summary>
        /// Get list of themes (authentication required)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <returns>List of themes</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemeItem>>> Get(int page = 1)
        {
            try
            {
                _blog = await _data.CustomFields.GetBlogSettings();
                var results = GetThemes();

                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        /// <summary>
        /// Set theme as current for a blog (admins only)
        /// </summary>
        /// <param name="id">Theme ID</param>
        /// <returns>Success or failure</returns>
        [Administrator]
        [HttpPut("select/{id}")]
        public ActionResult Put(string id)
        {
            try
            {
                var theme = _data.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogTheme);
                if (theme == null)
                {
                    theme = new CustomField { AuthorId = 0, Name = Constants.BlogTheme, Content = id };
                    _data.CustomFields.Add(theme);
                }
                else
                {
                    theme.Content = id;
                }

                if (_store.SelectTheme(theme.Content))
                {
                    _data.Complete();
                    return Ok(Resources.Updated);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "File Storage Failure");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        /// <summary>
        /// Get theme settings from data.json (admins only)
        /// </summary>
        /// <param name="theme">Theme name</param>
        /// <returns>Json data</returns>
        [Administrator]
        [HttpGet("data")]
        public ActionResult<string> GetThemeData(string theme)
        {
            try
            {
                var results = _store.GetThemeData(theme);

                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "File System Failure");
            }
        }

        /// <summary>
        /// Saves theme data (theme/assets/data.json file, admins only)
        /// </summary>
        /// <param name="model">Theme data model</param>
        /// <returns>Ok or error</returns>
        [Administrator]
        [HttpPost("data")]
        public async Task<IActionResult> SaveThemeData(ThemeDataModel model)
        {
            try
            {
                var settings = await _data.CustomFields.GetBlogSettings();
                var isActive = settings.Theme == model.Theme;

                await _store.SaveThemeData(model, isActive);
                return Ok("Created");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "File save error");
            }
        }

        /// <summary>
        /// Remove and unistall theme from the blog (admins only)
        /// </summary>
        /// <param name="id">Theme ID</param>
        /// <returns>Success or failure</returns>
        [Administrator]
        [HttpDelete("remove/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var themeContent = $"{AppSettings.WebRootPath}{slash}themes{slash}{id.ToLower()}";
                try
                {
                    if (Directory.Exists(themeContent))
                        Directory.Delete(themeContent, true);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
                return Ok(Resources.Removed);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        List<ThemeItem> GetThemes()
        {
            var themes = new List<ThemeItem>();
            var themeList = _store.GetThemes();

            if (themeList != null && themeList.Count > 0)
            {
                var current = new ThemeItem();
                foreach (var themeTitle in themeList)
                {
                    var theme = themeTitle.ToLower();
                    var slash = Path.DirectorySeparatorChar.ToString();
                    var file = $"{AppSettings.WebRootPath}{slash}themes{slash}{theme}{slash}{Constants.ThemeScreenshot}";
                    var data = $"{AppSettings.WebRootPath}{slash}themes{slash}{theme}{slash}assets{slash}{Constants.ThemeDataFile}";
                    var item = new ThemeItem
                    {
                        Title = themeTitle,
                        Cover = System.IO.File.Exists(file) ? $"themes/{theme}/{Constants.ThemeScreenshot}" : Constants.ImagePlaceholder,
                        IsCurrent = theme == _blog.Theme.ToLower(),
                        HasSettings = System.IO.File.Exists(data)
                    };

                    if (theme == _blog.Theme.ToLower())
                        current = item;
                    else
                        themes.Add(item);
                }
                themes.Insert(0, current);
            }

            return themes;
        }
    }

    public class ThemeItem
    {
        public string Title { get; set; }
        public string Cover { get; set; }
        public bool IsCurrent { get; set; }
        public bool HasSettings { get; set; }
    }
}