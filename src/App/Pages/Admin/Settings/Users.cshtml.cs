using Core.Data;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class UsersModel : AdminPageModel
    {
        IDataService _db;
        INotificationService _ns;

        [BindProperty]
        public IEnumerable<Author> Authors { get; set; }

        public UsersModel(IDataService db, INotificationService ns)
        {
            _db = db;
            _ns = ns;
        }

        public async Task<IActionResult> OnGetAsync(int page = 1)
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            Notifications = await _ns.GetNotifications(author.Id);

            if (!IsAdmin)
                return RedirectToPage("../Shared/Error", new { code = 403 });

            var pager = new Pager(page);
            Authors = await _db.Authors.GetList(u => u.Created > DateTime.MinValue, pager);

            return Page();
        }
    }
}