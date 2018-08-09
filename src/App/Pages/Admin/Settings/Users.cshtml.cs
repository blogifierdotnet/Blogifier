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

        [BindProperty]
        public IEnumerable<Author> Authors { get; set; }

        public UsersModel(IDataService db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(int page = 1)
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            if (!IsAdmin)
                return RedirectToPage("../Shared/_Error", new { code = 403 });

            var pager = new Pager(page);
            Authors = await _db.Authors.GetList(u => u.Created > DateTime.MinValue, pager);

            return Page();
        }
    }
}