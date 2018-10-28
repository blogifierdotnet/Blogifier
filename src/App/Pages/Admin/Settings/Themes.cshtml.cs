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
    public class ThemesModel : AdminPageModel
    {
        IDataService _db;
        IStorageService _storage;
        INotificationService _ns;

        [BindProperty]
        public IEnumerable<SelectListItem> Themes { get; set; }
        public BlogItem BlogItem { get; set; }

        public ThemesModel(IDataService db, IStorageService storage, INotificationService ns)
        {
            _db = db;
            _storage = storage;
            _ns = ns;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            Notifications = await _ns.GetNotifications(author.Id);

            if (!IsAdmin)
                return RedirectToPage("../Shared/_Error", new { code = 403 });

            BlogItem = await _db.CustomFields.GetBlogSettings();
            Themes = GetThemes();

            return Page();
        }

        List<SelectListItem> GetThemes()
        {
            var themes = new List<SelectListItem>();
            var combined = new List<string>();
            
            var storageThemes = _storage.GetThemes();

            if (storageThemes != null)
                combined.AddRange(storageThemes);

            if (AppConfig.EmbeddedThemes != null)
                combined.AddRange(AppConfig.EmbeddedThemes);

            combined = combined.Distinct().ToList();
            combined.Sort();

            if (combined != null && combined.Count > 0)
            {
                var current = new SelectListItem();
                foreach (var theme in combined)
                {
                    var item = new SelectListItem { Text = theme, Value = theme };
                    if (theme == BlogItem.Theme)
                        current = item;
                    else
                        themes.Add(item);
                }
                themes.Insert(0, current);
            }

            return themes;
        }
    }
}