using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class ProfileModel : AdminPageModel
    {
        IDataService _db;

        [BindProperty]
        public Author Author { get; set; }

        public ProfileModel(IDataService db)
        {
            _db = db;
        }

        public async Task OnGetAsync(string name)
        {
            Author = await _db.Authors.GetItem(u => u.AppUserName == User.Identity.Name);
            IsAdmin = Author.IsAdmin;

            if (!string.IsNullOrEmpty(name))
            {
                Author = await _db.Authors.GetItem(u => u.AppUserName == name);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var author = _db.Authors.Single(a => a.Id == Author.Id);
            author.DisplayName = Author.DisplayName;
            author.Bio = Author.Bio;
            author.Email = Author.Email;

            await _db.Authors.Save(author);
            Message = Resources.Updated;

            if (Author.AppUserName == User.Identity.Name)
                return RedirectToPage("Profile");
            else
                return Redirect($"~/admin/settings/profile?name={Author.AppUserName}");
        }
    }
}