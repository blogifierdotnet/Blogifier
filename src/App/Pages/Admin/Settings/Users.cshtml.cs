using Core.Data;
using Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class UsersModel : PageModel
    {
        IUnitOfWork _db;

        [BindProperty]
        public IEnumerable<Author> Authors { get; set; }

        public UsersModel(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task OnGet(int page = 1)
        {
            var pager = new Pager(page);
            Authors = await _db.Authors.GetItems(u => u.Created > DateTime.MinValue, pager);
        }
    }
}