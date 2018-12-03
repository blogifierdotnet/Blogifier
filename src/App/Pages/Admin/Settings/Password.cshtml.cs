using Core;
using Core.Data;
using Core.Data.Models;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class PasswordModel : AdminPageModel
    {
        IDataService _db;
        UserManager<AppUser> _um;
        SignInManager<AppUser> _sm;
        INotificationService _ns;

        [BindProperty]
        public ChangePasswordModel ChangePasswordModel { get; set; }

        public PasswordModel(IDataService db, UserManager<AppUser> um, SignInManager<AppUser> sm, INotificationService ns)
        {
            _db = db;
            _um = um;
            _sm = sm;
            _ns = ns;
        }

        public async Task OnGetAsync()
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            Notifications = await _ns.GetNotifications(author.Id);

            ChangePasswordModel = new ChangePasswordModel { UserName = User.Identity.Name };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (AppSettings.DemoMode)
            {
                ModelState.AddModelError("Custom", "Application configured to run in demo mode with change password disabled");
                return Page();
            }

            ModelState.Clear();
            try
            {
                var user = await _um.GetUserAsync(User);
                var result = await _um.ChangePasswordAsync(user, ChangePasswordModel.OldPassword, ChangePasswordModel.NewPassword);

                if (!result.Succeeded)
                {
                    throw new ApplicationException(result.Errors.First().Description);
                }

                await _sm.SignInAsync(user, isPersistent: false);

                Message = Resources.Updated;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Custom", ex.Message);
                return Page();
            }

            return RedirectToPage("Password");
        }
    }
}