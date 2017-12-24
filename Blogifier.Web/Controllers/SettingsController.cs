using Blogifier.Core.Common;
using Blogifier.Core.Controllers;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Email;
using Blogifier.Core.Services.FileSystem;
using Blogifier.Models;
using Blogifier.Models.AccountViewModels;
using Blogifier.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Controllers
{
    [Authorize]
    [Route("admin/[controller]")]
    public class SettingsController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailSender;
        private readonly ILogger _logger;
        private readonly string _theme;
        private readonly string _pwdTheme = "~/Views/Account/ChangePassword.cshtml";

        public SettingsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailSender,
            ILogger<AccountController> logger,
            IUnitOfWork db
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _db = db;
            _theme = $"~/{ApplicationSettings.BlogAdminFolder}/";
        }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        [MustBeAdmin]
        [Route("users")]
        public async Task<IActionResult> Users(int page = 1)
        {
            var profile = GetProfile();
            var pager = new Pager(page);
            var blogs = _db.Profiles.ProfileList(p => p.Id > 0, pager);

            var model = await GetUsersModel();
            model.Blogs = blogs;
            model.Pager = pager;

            return View(_theme + "Settings/Users.cshtml", model);
        }

        [HttpPost]
        [MustBeAdmin]
        [ValidateAntiForgeryToken]
        [Route("users")]
        public async Task<IActionResult> Register(UsersViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["AdminPage"] = true;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.RegisterModel.Email, Email = model.RegisterModel.Email };
                var result = await _userManager.CreateAsync(user, model.RegisterModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation(string.Format("Created a new account for {0}", user.UserName));

                    // create new profile
                    var profile = new Profile();

                    if (_db.Profiles.All().ToList().Count == 0 || model.RegisterModel.IsAdmin)
                    {
                        profile.IsAdmin = true;
                    }

                    profile.AuthorName = model.RegisterModel.AuthorName;
                    profile.AuthorEmail = model.RegisterModel.Email;
                    profile.Title = "New blog";
                    profile.Description = "New blog description";

                    profile.IdentityName = user.UserName;
                    profile.Slug = SlugFromTitle(profile.AuthorName);
                    profile.Avatar = ApplicationSettings.ProfileAvatar;
                    profile.BlogTheme = BlogSettings.Theme;

                    profile.LastUpdated = SystemClock.Now();

                    _db.Profiles.Add(profile);
                    await _db.Complete();

                    _logger.LogInformation(string.Format("Created a new profile at /{0}", profile.Slug));

                    if (model.RegisterModel.SendEmailNotification)
                    {
                        var userUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, profile.Slug);
                        await _emailSender.SendEmailWelcomeAsync(model.RegisterModel.Email, model.RegisterModel.AuthorName, userUrl);
                    }

                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            var pager = new Pager(1);
            var blogs = _db.Profiles.ProfileList(p => p.Id > 0, pager);

            var regModel = await GetUsersModel();
            regModel.Blogs = blogs;
            regModel.Pager = pager;

            return View(_theme + "Settings/Users.cshtml", regModel);
        }

        [MustBeAdmin]
        [HttpDelete("{id}")]
        [Route("users/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var admin = await GetProfile();

            if (!admin.IsAdmin || admin.Id == id)
                return NotFound();

            var profile = await _db.Profiles.Single(p => p.Id == id);

            _logger.LogInformation(string.Format("Delete blog {0} by {1}", profile.Title, profile.AuthorName));

            var assets = _db.Assets.Find(a => a.ProfileId == id);
            _db.Assets.RemoveRange(assets);
            await _db.Complete();
            _logger.LogInformation("Assets deleted");

            var categories = _db.Categories.Find(c => c.ProfileId == id);
            _db.Categories.RemoveRange(categories);
            await _db.Complete();
            _logger.LogInformation("Categories deleted");

            var posts = _db.BlogPosts.Find(p => p.ProfileId == id);
            _db.BlogPosts.RemoveRange(posts);
            await _db.Complete();
            _logger.LogInformation("Posts deleted");

            var fields = _db.CustomFields.Find(f => f.CustomType == CustomType.Profile && f.ParentId == id);
            _db.CustomFields.RemoveRange(fields);
            await _db.Complete();
            _logger.LogInformation("Custom fields deleted");

            var profileToDelete = await _db.Profiles.Single(b => b.Id == id);

            var storage = new BlogStorage(profileToDelete.Slug);
            storage.DeleteFolder("");
            _logger.LogInformation("Storage deleted");

            _db.Profiles.Remove(profileToDelete);
            await _db.Complete();
            _logger.LogInformation("Profile deleted");

            // remove login

            var user = await _userManager.FindByNameAsync(profile.IdentityName);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred removing login for user with ID '{user.Id}'.");
            }
            return new NoContentResult();
        }

        [HttpGet]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction("Login", "Account");
            }

            var profile = await _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage, Profile = profile };
            return View(_pwdTheme, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            model.Profile = await GetProfile();

            if (!ModelState.IsValid)
            {
                return View(_pwdTheme, model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                model.StatusMessage = $"Error: Unable to load user with ID '{_userManager.GetUserId(User)}'";
                return View(_pwdTheme, model);
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                model.StatusMessage = $"Error: {changePasswordResult.Errors.ToList()[0].Description}";
                return View(_pwdTheme, model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        #region Helpers

        private async Task<Profile> GetProfile()
        {
            return await _db.Profiles.Single(b => b.IdentityName == User.Identity.Name);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(BlogController.Index), "Blog");
            }
        }

        string SlugFromTitle(string title)
        {
            var slug = title.ToSlug();
            if (_db.Profiles.Single(b => b.Slug == slug) != null)
            {
                for (int i = 2; i < 100; i++)
                {
                    if (_db.Profiles.Single(b => b.Slug == slug + i.ToString()) == null)
                    {
                        return slug + i.ToString();
                    }
                }
            }
            return slug;
        }

        async Task<UsersViewModel> GetUsersModel()
        {
            var profile = await GetProfile();

            var model = new UsersViewModel
            {
                Profile = profile,
                RegisterModel = new RegisterViewModel()
            };
            model.RegisterModel.SendGridApiKey = _db.CustomFields.GetValue(
                CustomType.Application, profile.Id, Constants.SendGridApiKey);

            return model;
        }

        #endregion
    }
}