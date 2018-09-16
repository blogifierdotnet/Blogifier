using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class ImportModel : AdminPageModel
    {
        IDataService _db;
        INotificationService _ns;

        public ImportModel(IDataService db, INotificationService ns)
        {
            _db = db;
            _ns = ns;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            Notifications = await _ns.GetNotifications(author.Id);

            if (!author.IsAdmin)
                return RedirectToPage("../Shared/_Error", new { code = 403 });

            return Page();
        }
    }
}