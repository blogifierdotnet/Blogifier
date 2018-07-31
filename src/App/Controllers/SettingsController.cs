using Core;
using Core.Data;
using Core.Data.Models;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        IUnitOfWork _db;
        IFeedImportService _feed;
        IStorageService _storage;
        IAppSettingsService<AppItem> _app;
        UserManager<AppUser> _um;

        public SettingsController(IUnitOfWork db, IFeedImportService feed, IStorageService storage, IAppSettingsService<AppItem> app, UserManager<AppUser> um)
        {
            _db = db;
            _feed = feed;
            _storage = storage;
            _app = app;
            _um = um;
        }

        public IActionResult Index()
        {
            ViewBag.IsAdmin = IsAdmin();

            if (!ViewBag.IsAdmin)
                return RedirectToAction(nameof(Profile));

            _app.Value.BlogThemes = GetThemes();

            return View(_app.Value);
        }

        [HttpPost]
        public IActionResult Index(AppItem model)
        {
            if (ModelState.IsValid)
            {
                _app.Update(app =>
                {
                    app.Title = model.Title;
                    app.Description = model.Description;
                    app.Logo = model.Logo;
                    app.Cover = model.Cover;
                    app.Theme = model.Theme;
                    app.PostListType = model.PostListType;
                    app.ItemsPerPage = model.ItemsPerPage;
                });

                //TODO: find better way to wait on config rewrite
                System.Threading.Thread.Sleep(500);

                AppConfig.SetSettings(model);

                TempData["msg"] = Resources.Updated;
                return RedirectToAction(nameof(Index));
            }

            model.BlogThemes = GetThemes();
            ViewBag.IsAdmin = IsAdmin();

            return View(model);
        }

        public async Task<IActionResult> Users(int page = 1)
        {
            ViewBag.IsAdmin = IsAdmin();

            if (!ViewBag.IsAdmin)
                return RedirectToAction(nameof(Profile));

            var pager = new Pager(page);
            var authors = await _db.Authors.GetItems(u => u.Created > DateTime.MinValue, pager);

            return View(authors);
        }

        public async Task<IActionResult> Profile(string name = "")
        {
            Author author;

            if (string.IsNullOrEmpty(name))
            {
                author = await _db.Authors.GetItem(u => u.AppUserName == User.Identity.Name);
            }
            else
            {
                if (!IsAdmin())
                    return Redirect("~/error/403");

                author = await _db.Authors.GetItem(u => u.AppUserName == name);
            }
            
            ViewBag.IsAdmin = IsAdmin();
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(Author model)
        {
            ViewBag.IsAdmin = IsAdmin();

            if (ModelState.IsValid)
            {
                var author = _db.Authors.Single(a => a.Id == model.Id);
                author.DisplayName = model.DisplayName;
                author.Email = model.Email;

                await _db.Authors.Save(author);
                TempData["msg"] = Resources.Updated;

                if (model.AppUserName == User.Identity.Name)
                    return RedirectToAction(nameof(Profile));
                else
                    return Redirect($"~/settings/profile?name={model.AppUserName}");
            }
            
            return View(model);
        }

        public IActionResult Password()
        {
            ViewBag.IsAdmin = IsAdmin();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Password(ChangePasswordModel model)
        {
            model.UserName = User.Identity.Name;
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                try
                {
                    await _db.Authors.ChangePassword(model);
                    TempData["msg"] = Resources.Updated;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Custom", ex.Message);
                }
            }

            ViewBag.IsAdmin = IsAdmin();
            return View(model);
        }

        public IActionResult Register()
        {
            if (!IsAdmin())
                return Redirect("~/error/403");

            ViewBag.IsAdmin = IsAdmin();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!IsAdmin())
                return Redirect("~/error/403");

            if (ModelState.IsValid)
            {
                // register new app user account
                var result = await _um.CreateAsync(new AppUser { UserName = model.UserName, Email = model.Email }, model.Password);

                // add user as author to app database
                var user = _db.Authors.Single(a => a.AppUserName == model.UserName);
                if (user == null)
                {
                    user = new Author
                    {
                        AppUserName = model.UserName,
                        Email = model.Email
                    };

                    await _db.Authors.Save(user);

                    TempData["msg"] = Resources.Created;
                    return RedirectToAction(nameof(Users));
                }
                else
                {
                    ModelState.AddModelError("Custom", Resources.UserExists);
                }
            }
            ViewBag.IsAdmin = IsAdmin();
            return View();
        }

        public async Task<IActionResult> Remove(string name)
        {
            var author = _db.Authors.Single(a => a.AppUserName == name);

            if (!IsAdmin() || name == User.Identity.Name)
                Redirect("~/error/403");

            await _db.Authors.Remove(author.Id);
            _storage.DeleteFolder(author.AppUserName);

            TempData["msg"] = Resources.Removed;

            return RedirectToAction(nameof(Users));
        }

        public IActionResult RssImport()
        {
            if (!IsAdmin())
                return Redirect("~/error/403");

            ViewBag.IsAdmin = IsAdmin();

            return View();
        }

        bool IsAdmin()
        {
            return _db.Authors.Single(a => a.AppUserName == User.Identity.Name).IsAdmin;
        }

        List<SelectListItem> GetThemes()
        {
            var themes = new List<SelectListItem>
            {
                new SelectListItem { Text = "Simple", Value = "Simple" }
            };

            var storageThemes = _storage.GetThemes();

            if(storageThemes != null && storageThemes.Count > 0)
            {
                foreach (var theme in storageThemes)
                {
                    themes.Add(new SelectListItem { Text = theme, Value = theme });
                }
            }           
            return themes;
        }
    }
}