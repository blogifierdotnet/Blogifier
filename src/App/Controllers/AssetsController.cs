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
        IStorageService _ss;
        UserManager<AppUser> _um;

        public AssetsController(IUnitOfWork db, IStorageService ss, UserManager<AppUser> um)
        {
            _db = db;
            _um = um;
            _ss = ss;
        }

        public async Task<AssetsModel> Index(int page = 1, string filter = "", string search = "")
        {
            var pager = new Pager(page);
            var user = _um.Users.Single(u => u.UserName == User.Identity.Name);
            IEnumerable<AssetItem> items;

            if (string.IsNullOrEmpty(search))
            {
                if (filter == "filterImages")
                {
                    items = await _ss.Find(a => a.AssetType == AssetType.Image, pager);
                }
                else if(filter == "filterAttachments")
                {
                    items = await _ss.Find(a => a.AssetType == AssetType.Attachment, pager);
                }
                else
                {
                    items = await _ss.Find(null, pager);
                }
            }
            else
            {
                items = await _ss.Find(a => a.Title.Contains(search), pager);
            }

            if (page < 1 || page > pager.LastPage)
                return null;

            return new AssetsModel
            {
                Assets = items,
                Pager = pager
            };
        }

        public async Task<AssetItem> Pick(string type, string asset, string post)
        {
            if (type == "postCover")
            {
                await _db.BlogPosts.SaveCover(int.Parse(post), asset);
            }
            else if (type == "appCover")
            {
                await _db.Settings.SaveSetting("app-cover", asset);
            }
            else if (type == "appLogo")
            {
                await _db.Settings.SaveSetting("app-logo", asset);
            }
            else if (type == "avatar")
            {
                var user = await _um.FindByNameAsync(User.Identity.Name);
                user.Avatar = asset;
                await _um.UpdateAsync(user);
            }

            var item = await _ss.Find(a => a.Url == asset, new Pager(1));
            return item.FirstOrDefault();
        }

        public IActionResult Remove(string url)
        {
            _ss.DeleteFile(url);

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

            var asset = await _ss.UploadFormFile(file, Url.Content("~/"), path);
        }
    }
}