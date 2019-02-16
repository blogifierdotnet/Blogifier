using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers.Api
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

        [Authorize]
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
                _data.Complete();

                return Ok(Resources.Updated);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [Authorize]
        [HttpDelete("remove/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var themeContent = $"{AppSettings.WebRootPath}{slash}themes{slash}{id.ToLower()}";
                var themeViews = $"{AppSettings.ContentRootPath}{slash}Views{slash}Themes{slash}{id}";
                try
                {
                    if (Directory.Exists(themeContent))
                        Directory.Delete(themeContent, true);

                    if (Directory.Exists(themeViews))
                        Directory.Delete(themeViews, true);
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
            var combined = new List<string>();

            var storageThemes = _store.GetThemes();

            if (storageThemes != null)
                combined.AddRange(storageThemes);

            if (AppConfig.EmbeddedThemes != null)
                combined.AddRange(AppConfig.EmbeddedThemes);

            combined = combined.Distinct().ToList();
            combined.Sort();

            if (combined != null && combined.Count > 0)
            {
                var current = new ThemeItem();
                foreach (var theme in combined)
                {
                    var slash = Path.DirectorySeparatorChar.ToString();
                    var file = $"{AppSettings.WebRootPath}{slash}themes{slash}{theme}{slash}{theme}.png";
                    var item = new ThemeItem
                    {
                        Title = theme,
                        Cover = System.IO.File.Exists(file) ? $"themes/{theme}/{theme}.png" : "lib/img/img-placeholder.png",
                        IsCurrent = theme == _blog.Theme
                    };

                    if (theme == _blog.Theme)
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
    }
}