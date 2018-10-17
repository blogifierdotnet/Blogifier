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
        public BlogItem BlogItem { get; set; }

        IDataService _db;
        IStorageService _storage;
        INotificationService _ns;

        public IndexModel(IDataService db, IStorageService storage, INotificationService ns)
        {
            _db = db;
            _storage = storage;
            _ns = ns;
            BlogItem = new BlogItem();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            Notifications = await _ns.GetNotifications(author.Id);

            if (!author.IsAdmin)
                return RedirectToPage("../Shared/_Error", new { code = 403 });

            BlogItem = await _db.CustomFields.GetBlogSettings();
            BlogItem.BlogThemes = GetThemes();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _db.CustomFields.SaveBlogSettings(BlogItem);

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