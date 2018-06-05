using Core;
using Core.Data;
using Core.Data.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        IUnitOfWork _db;
        ISyndicationService _feed;
        IStorageService _storage;
        IAppSettingsServices<AppItem> _app;

        public SettingsController(IUnitOfWork db, ISyndicationService feed, IStorageService storage, IAppSettingsServices<AppItem> app)
        {
            _db = db;
            _feed = feed;
            _storage = storage;
            _app = app;
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
            AuthorItem author;

            if (string.IsNullOrEmpty(name))
            {
                author = await _db.Authors.GetItem(u => u.UserName == User.Identity.Name);
            }
            else
            {
                if (!IsAdmin())
                    return Redirect("~/error/403");

                author = await _db.Authors.GetItem(u => u.UserName == name);
            }
            
            ViewBag.IsAdmin = IsAdmin();
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(AuthorItem model)
        {
            ViewBag.IsAdmin = IsAdmin();

            if (ModelState.IsValid)
            {
                var user = _db.Authors.Single(a => a.Id == model.Id);
                user.DisplayName = model.DisplayName;
                user.Email = model.Email;

                var result = await _db.Authors.SaveUser(user);
                if (result.Succeeded)
                {
                    TempData["msg"] = Resources.Updated;

                    if (model.UserName == User.Identity.Name)
                        return RedirectToAction(nameof(Profile));
                    else
                        return Redirect($"~/settings/profile?name={model.UserName}");
                }
                else
                {
                    ModelState.AddModelError("Custom", result.Errors.First().Description);
                }
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
                var user = _db.Authors.Single(a => a.UserName == model.UserName);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = model.UserName,
                        Email = model.Email
                    };

                    var result = await _db.Authors.SaveUser(user, model.Password);

                    if (result.Succeeded)
                    {
                        TempData["msg"] = Resources.Created;
                        return RedirectToAction(nameof(Users));
                    }
                    else
                    {
                        ModelState.AddModelError("Custom", result.Errors.First().Description);
                    }
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
            var author = _db.Authors.Single(a => a.UserName == name);

            if (!IsAdmin() || name == User.Identity.Name)
                Redirect("~/error/403");

            await _db.Authors.RemoveUser(author);
            _storage.DeleteFolder(author.UserName);

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

        [HttpPost]
        public async Task<IActionResult> RssImport(IFormFile file)
        {
            if (!IsAdmin())
                return Redirect("~/error/403");

            var user = _db.Authors.Single(a => a.UserName == User.Identity.Name);

            await _feed.ImportRss(file, User.Identity.Name);

            ViewBag.IsAdmin = true;
            return View();
        }

        bool IsAdmin()
        {
            return _db.Authors.Single(a => a.UserName == User.Identity.Name).IsAdmin;
        }

        List<SelectListItem> GetThemes()
        {
            var themes = new List<SelectListItem>();
            themes.Add(new SelectListItem { Text = "Simple", Value = "Simple" });

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