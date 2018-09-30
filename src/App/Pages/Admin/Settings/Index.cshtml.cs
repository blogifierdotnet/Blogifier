using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
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
        INotificationService _ns;

        public IndexModel(IDataService db, IAppService<AppItem> app, IStorageService storage, INotificationService ns)
        {
            _db = db;
            _app = app;
            _storage = storage;
            _ns = ns;
            _app.Value.BlogThemes = GetThemes();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            Notifications = await _ns.GetNotifications(author.Id);

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
            var themes = new List<SelectListItem>();
            var combined = new List<string>();

            var storageThemes = _storage.GetThemes();

            if(storageThemes != null)
                combined.AddRange(storageThemes);

            if(AppConfig.EmbeddedThemes != null)
                combined.AddRange(AppConfig.EmbeddedThemes);

            combined = combined.Distinct().ToList();
            combined.Sort();
            
            if (combined != null && combined.Count > 0)
            {
                foreach (var theme in combined)
                {
                    themes.Add(new SelectListItem { Text = theme, Value = theme });
                }
            }
           
            return themes;
        }
    }
}