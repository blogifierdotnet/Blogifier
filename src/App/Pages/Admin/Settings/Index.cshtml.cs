using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class IndexModel : AdminPageModel
    {
        [BindProperty]
        public AppItem AppItem { get; set; }

        IUnitOfWork _db;
        IAppSettingsService<AppItem> _app;
        IStorageService _storage;

        public IndexModel(IUnitOfWork db, IAppSettingsService<AppItem> app, IStorageService storage)
        {
            _db = db;
            _app = app;
            _storage = storage;
            _app.Value.BlogThemes = GetThemes();
        }

        public async Task<IActionResult> OnGet()
        {
            Author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);

            if (!Author.IsAdmin)
                return RedirectToPage("../Shared/_Error", new { code = 403 });

            AppItem = _app.Value;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _app.Update(app =>
            {
                app.Title = AppItem.Title;
                app.Description = AppItem.Description;
                app.Logo = AppItem.Logo;
                app.Cover = AppItem.Cover;
                app.Theme = AppItem.Theme;
                app.ItemsPerPage = AppItem.ItemsPerPage;
                app.UseDescInPostList = AppItem.UseDescInPostList;
            });

            //TODO: find better way to wait on config rewrite
            System.Threading.Thread.Sleep(500);

            AppConfig.SetSettings(AppItem);

            Message = Resources.Updated;

            return RedirectToPage("Index");
        }

        List<SelectListItem> GetThemes()
        {
            var themes = new List<SelectListItem>
            {
                new SelectListItem { Text = "Simple", Value = "Simple" }
            };

            var storageThemes = _storage.GetThemes();

            if (storageThemes != null && storageThemes.Count > 0)
            {
                foreach (var theme in storageThemes)
                {
                    themes.Add(new SelectListItem { Text = theme, Value = theme });
                }
            }
            return themes;
        }
    }
}