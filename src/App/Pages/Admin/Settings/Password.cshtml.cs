using Core;
using Core.Data;
using Core.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class PasswordModel : AdminPageModel
    {
        IUnitOfWork _db;
        UserManager<AppUser> _um;
        SignInManager<AppUser> _sm;

        [BindProperty]
        public ChangePasswordModel ChangePasswordModel { get; set; }

        public PasswordModel(IUnitOfWork db, UserManager<AppUser> um, SignInManager<AppUser> sm)
        {
            _db = db;
            _um = um;
            _sm = sm;
        }

        public void OnGet()
        {
            ChangePasswordModel = new ChangePasswordModel { UserName = User.Identity.Name };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

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