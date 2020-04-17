using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier
{
    public class Register : PageModel
    {
        [Required]
        [BindProperty]
        public string UserName { get; set; }
        [Required]
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [BindProperty]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        UserManager<AppUser> UserManager;
        IDataService DataService;

        public Register(UserManager<AppUser> userManager, IDataService dataService)
        {
            UserManager = userManager;
            DataService = dataService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync([FromQuery]string returnUrl = null)
        {
            if(DataService.Authors.All().Count() > 0)
            {
                // only if we want single user for a personal blog
                ModelState.AddModelError(string.Empty, "Application user already created");
                return Page();
            }

            if (ModelState.IsValid)
            {
                // password compare attribute does not work for some reason..
                if (!(Password == ConfirmPassword))
                {
                    ModelState.AddModelError(string.Empty, "Password and confirm password do not match");
                    return Page();
                }

                // create new user identity
                var createResult = await UserManager.CreateAsync(new AppUser
                {
                    UserName = UserName,
                    Email = $"{UserName}@blog.com"
                }, Password);

                if (!createResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Error creating new user");
                    return Page();
                }

                // create new author
                var user = new Author
                {
                    AppUserName = UserName,
                    Email = $"{UserName}@blog.com",
                    IsAdmin = true
                };
                await DataService.Authors.Save(user);
                var created = DataService.Authors.Single(a => a.AppUserName == UserName);

                return RedirectToLocal(returnUrl);
            }
            return Page();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            returnUrl = returnUrl.SanitizePath();
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("~/");
            }
        }
    }
}