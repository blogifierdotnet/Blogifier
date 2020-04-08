using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Pages.Account
{
    public class LoginModel : PageModel
    {
        [Required]
        [BindProperty]
        public string UserName { get; set; }

        [Required]
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        SignInManager<AppUser> SignInManager;
        IDataService DataService;

        public LoginModel(SignInManager<AppUser> signInManager, IDataService dataService)
        {
            SignInManager = signInManager;
            DataService = dataService;
        }

        public IActionResult OnGet()
        {
            if (DataService.Authors.All().Count() == 0)
            {
                return Redirect("~/account/register?returnUrl=admin");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromQuery]string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(UserName, Password, RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
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