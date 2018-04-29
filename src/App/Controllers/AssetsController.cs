using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Authorize]
    public class AssetsController : Controller
    {
        IUnitOfWork _db;
        IStorageService _storage;
        UserManager<AppUser> _um;

        public AssetsController(IUnitOfWork db, IStorageService storage, UserManager<AppUser> um)
        {
            _db = db;
            _um = um;
            _storage = storage;
        }

        public async Task<AssetsModel> Index(int page = 1, string filter = "", string search = "")
        {
            var pager = new Pager(page);
            var user = _um.Users.Single(u => u.UserName == User.Identity.Name);
            IEnumerable<Asset> items;

            if (string.IsNullOrEmpty(search))
            {
                if (filter == "filterImages")
                {
                    items = await _db.Assets.Find(a => a.UserId == user.Id && a.AssetType == AssetType.Image, pager);
                }
                else if(filter == "filterAttachments")
                {
                    items = await _db.Assets.Find(a => a.UserId == user.Id && a.AssetType == AssetType.Attachment, pager);
                }
                else
                {
                    items = await _db.Assets.Find(a => a.UserId == user.Id, pager);
                }
            }
            else
            {
                items = await _db.Assets.Find(a => a.Title.Contains(search), pager);
            }

            if (page < 1 || page > pager.LastPage)
                return null;

            return new AssetsModel
            {
                Assets = items,
                Pager = pager
            };
        }

        public async Task<Asset> Pick(string type, string asset, string post)
        {
            var url = _db.Assets.Single(a => a.Id == int.Parse(asset)).Url;

            if (type == "postCover")
            {
                return await _db.Assets.SavePostCover(int.Parse(post), int.Parse(asset));
            }
            else if(type == "appCover")
            {
                await _db.Settings.SaveSetting("app-cover", url);
            }
            else if (type == "appLogo")
            {
                await _db.Settings.SaveSetting("app-logo", url);
            }
            else if (type == "avatar")
            {
                var user = await _um.FindByNameAsync(User.Identity.Name);
                user.Avatar = url;
                await _um.UpdateAsync(user);
            }

            var dbAsset = _db.Assets.Single(a => a.Id == int.Parse(asset));
            return await Task.FromResult(dbAsset);
        }

        public async Task<Asset> Single(int id)
        {
            return await Task.FromResult(_db.Assets.Single(a => a.Id == id));
        }

        public IActionResult Remove(int id)
        {
            var asset = _db.Assets.Single(a => a.Id == id);

            _storage.DeleteFile(asset.Path);

            _db.Assets.Remove(asset);
            _db.Complete();

            return Ok("Deleted");
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            foreach (var file in files)
            {
                await SaveFile(file);
            }
            return Ok("Created");
        }

        async Task SaveFile(IFormFile file)
        {
            var user = _um.Users.Where(u => u.UserName == User.Identity.Name)
                    .FirstOrDefault();

            var path = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Month);

            var asset = await _storage.UploadFormFile(file, Url.Content("~/"), path);

            // sometimes we just want to override uploaded file
            // only add DB record if asset does not exist yet
            var existingAsset = _db.Assets.Find(a => a.Path == asset.Path).FirstOrDefault();
            if (existingAsset == null)
            {
                asset.UserId = user.Id;
                asset.Published = SystemClock.Now();

                if (IsImageFile(asset.Url))
                {
                    asset.AssetType = AssetType.Image;
                }
                else
                {
                    asset.AssetType = AssetType.Attachment;
                }
                _db.Assets.Add(asset);
            }
            else
            {
                existingAsset.Published = SystemClock.Now();
            }
            _db.Complete();
        }

        bool IsImageFile(string file)
        {
            if (file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                file.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}