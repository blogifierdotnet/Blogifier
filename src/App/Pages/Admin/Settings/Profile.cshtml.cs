using Core;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace App.Pages.Admin.Settings
{
    public class ProfileModel : AdminPageModel
    {
        IDataService _db;
        IStorageService _ss;
        UserManager<AppUser> _um;
        INotificationService _ns;

        public string Action { get; set; }

        [BindProperty]
        public Author Author { get; set; }

        public ProfileModel(IDataService db, IStorageService ss, UserManager<AppUser> um, INotificationService ns)
        {
            _db = db;
            _ss = ss;
            _um = um;
            _ns = ns;
        }

        public async Task OnGetAsync(string name, string delete)
        {
            Author = await _db.Authors.GetItem(u => u.AppUserName == User.Identity.Name);
            IsAdmin = Author.IsAdmin;

            Notifications = await _ns.GetNotifications(Author.Id);

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

        public async Task<IActionResult> OnPostConfirm(string name)
        {
            Author = await _db.Authors.GetItem(u => u.AppUserName == name);
            Action = "Confirm";
            return Page();
        }

        public async Task<IActionResult> OnPostRemove(string name)
        {
            // TODO: add security checks

            Author = await _db.Authors.GetItem(u => u.AppUserName == name);

            // remove posts
            var posts = _db.BlogPosts.All().Where(p => p.AuthorId == Author.Id).ToList();
            _db.BlogPosts.RemoveRange(posts);

            // remove author
            _db.Authors.Remove(Author);
            _db.Complete();

            // remove files
            _ss.DeleteAuthor(name);

            // remove user
            var user = await _um.FindByNameAsync(Author.AppUserName);
            await _um.DeleteAsync(user);
            
            Action = "Remove";

            return Page();
        }

    }
}