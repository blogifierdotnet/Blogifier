using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class IndexModel : AdminPageModel
    {
        [BindProperty]
        public BlogItem BlogItem { get; set; }

        IDataService _db;
        INotificationService _ns;

        public IndexModel(IDataService db, INotificationService ns)
        {
            _db = db;
            _ns = ns;
            BlogItem = new BlogItem();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await SetModel();
            if (!IsAdmin)
                return RedirectToPage("../Shared/_Error", new { code = 403 });

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _db.CustomFields.SaveBlogSettings(BlogItem);
            Message = Resources.Updated;

            await SetModel();
            if (!IsAdmin)
                return RedirectToPage("../Shared/_Error", new { code = 403 });

            return Page();
        }

        async Task SetModel()
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;
            Notifications = await _ns.GetNotifications(author.Id);
            BlogItem = await _db.CustomFields.GetBlogSettings();
        }
    }
}