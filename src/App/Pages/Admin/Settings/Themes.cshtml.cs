using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class ThemesModel : AdminPageModel
    {
        IDataService _db;
        IStorageService _storage;
        INotificationService _ns;

        public IEnumerable<ThemeItem> Themes { get; set; }
        public BlogItem BlogItem { get; set; }

        public ThemesModel(IDataService db, IStorageService storage, INotificationService ns)
        {
            _db = db;
            _storage = storage;
            _ns = ns;
        }

        public async Task<IActionResult> OnGetAsync(string id, string act)
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            Notifications = await _ns.GetNotifications(author.Id);

            if (!IsAdmin)
                return RedirectToPage("../Shared/_Error", new { code = 403 });

            if(act == "set" && !string.IsNullOrEmpty(id))
            {
                var theme = _db.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogTheme);
                theme.Content = id;
                _db.Complete();
                Message = Resources.Updated;
            }

            if(act == "del" && !string.IsNullOrEmpty(id))
            {
                Message = Resources.Removed;
            }

            BlogItem = await _db.CustomFields.GetBlogSettings();
            Themes = GetThemes();

            return Page();
        }

        List<ThemeItem> GetThemes()
        {
            var themes = new List<ThemeItem>();
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
                var current = new ThemeItem();
                foreach (var theme in combined)
                {
                    var slash = Path.DirectorySeparatorChar.ToString();
                    var file = $"{AppSettings.WebRootPath}{slash}themes{slash}{theme}{slash}{theme}.png";
                    var item = new ThemeItem {
                        Title = theme,
                        Cover = System.IO.File.Exists(file) ? $"themes/{theme}/{theme}.png" : "lib/img/img-placeholder.png",
                        IsCurrent = theme == BlogItem.Theme
                    };

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

    public class ThemeItem
    {
        public string Title { get; set; }
        public string Cover { get; set; }
        public bool IsCurrent { get; set; }
    }
}