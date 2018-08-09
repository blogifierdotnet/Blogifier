using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class RegisterModel : AdminPageModel
    {
        #region Properties

        [Required]
        [BindProperty]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [BindProperty]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [BindProperty]
        public string ConfirmPassword { get; set; }

        #endregion

        UserManager<AppUser> _um;
        IDataService _db;

        public RegisterModel(IDataService db, UserManager<AppUser> um)
        {
            _db = db;
            _um = um;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var author = await _db.Authors.GetItem(a => a.AppUserName == User.Identity.Name);
            IsAdmin = author.IsAdmin;

            if (!IsAdmin)
                return RedirectToPage("../Shared/_Error", new { code = 403 });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // register new app user account
            var result = await _um.CreateAsync(new AppUser { UserName = UserName, Email = Email }, Password);

            // add user as author to app database
            var user = _db.Authors.Single(a => a.AppUserName == UserName);
            if (user == null)
            {
                user = new Author
                {
                    AppUserName = UserName,
                    Email = Email
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