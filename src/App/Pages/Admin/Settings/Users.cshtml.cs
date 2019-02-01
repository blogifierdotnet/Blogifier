using Core;
using Core.Data;
using Core.Data.Models;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Identity;
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
        UserManager<AppUser> _um;

        [BindProperty]
        public IEnumerable<Author> Authors { get; set; }

        public UsersModel(IDataService db, INotificationService ns, UserManager<AppUser> um)
        {
            _db = db;
            _ns = ns;
            _um = um;
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

            ModelState.Clear();

            return Page();
        }

        public async Task<IActionResult> OnPostRegister(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return Page();

            // register new app user account
            var result = await _um.CreateAsync(new AppUser { UserName = model.UserName, Email = model.Email }, model.Password);

            if (!result.Succeeded)
            {
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("Custom", er.Description);
                    return Page();
                }
            }

            // add user as author to app database
            var user = _db.Authors.Single(a => a.AppUserName == model.UserName);
            if (user == null)
            {
                user = new Author
                {
                    AppUserName = model.UserName,
                    Email = model.Email,
                    IsAdmin = model.SetAsAdmin
                };

                await _db.Authors.Save(user);

                Message = Resources.Created;
                return RedirectToPage("Users");
            }
            else
            {
                ModelState.AddModelError("Custom", Resources.UserExists);
            }

            return Page();
        }
    }
}