using Core;
using Core.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class ProfileModel : AdminPageModel
    {
        IUnitOfWork _db;

        public ProfileModel(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task OnGetAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Author = await _db.Authors.GetItem(u => u.AppUserName == User.Identity.Name);
            }
            else
            {
                if (!IsAdmin())
                    RedirectToPage("~/error/403");

                Author = await _db.Authors.GetItem(u => u.AppUserName == name);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var author = _db.Authors.Single(a => a.Id == Author.Id);
            author.DisplayName = Author.DisplayName;
            author.Email = Author.Email;

            await _db.Authors.Save(author);
            Message = Resources.Updated;

            if (Author.AppUserName == User.Identity.Name)
                return RedirectToPage("Profile");
            else
                return Redirect($"~/admin/settings/profile?name={Author.AppUserName}");
        }

        public bool IsAdmin()
        {
            return _db.Authors.Single(a => a.AppUserName == User.Identity.Name).IsAdmin;
        }
    }
}