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

        IDataService _db;
        IAppService<AppItem> _app;
        IStorageService _storage;

        public IndexModel(IDataService db, IAppService<AppItem> app, IStorageService storage)
        {
            _db = db;
            _app = app;
            _storage = storage;
            _app.Value.BlogThemes = GetThemes();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            Notifications = _db.Notifications.Find(n => n.Active && (n.AuthorId == 0 || n.AuthorId == author.Id));

            if (!author.IsAdmin)
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
                app.ImportTypes = AppItem.ImportTypes;
                app.ImageExtensions = AppItem.ImageExtensions;
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