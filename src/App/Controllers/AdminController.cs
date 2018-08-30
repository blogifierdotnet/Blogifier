using Core;
using Core.Data;
using Core.Helpers;
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
    public class AdminController : Controller
    {
        IDataService _db;
        IStorageService _ss;
        IAppService<AppItem> _app;
        UserManager<AppUser> _um;
        IImportService _feed;

        public AdminController(IDataService db, IImportService feed, IStorageService ss, UserManager<AppUser> um, IAppService<AppItem> app)
        {
            _db = db;
            _feed = feed;
            _um = um;
            _ss = ss;
            _app = app;
        }

        public IActionResult Index()
        {
            return Redirect("~/admin/posts");
        }

        [HttpDelete]
        public async Task RemovePost(int id)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            _db.BlogPosts.Remove(post);
            _db.Complete();
            await Task.CompletedTask;
        }

        [HttpPut]
        public async Task PublishPost(int id, string flag)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            if (!string.IsNullOrEmpty(flag))
            {
                if (flag == "P") post.Published = DateTime.UtcNow;
                if (flag == "U") post.Published = DateTime.MinValue;
                _db.Complete();
            }
            await Task.CompletedTask;
        }

        [HttpPut]
        public async Task FeaturePost(int id, string flag)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            if (!string.IsNullOrEmpty(flag))
            {
                if (flag == "F") post.IsFeatured = true;
                if (flag == "U") post.IsFeatured = false;
                _db.Complete();
            }
            await Task.CompletedTask;
        }

        [Route("assets")]
        public async Task<AssetsModel> GetAssetList(int page = 1, string filter = "", string search = "")
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
                else if (filter == "filterAttachments")
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

        [Route("assets/pick")]
        public async Task<AssetItem> Pick(string type, string asset, string post)
        {
            if (type == "postCover")
            {
                await _db.BlogPosts.SaveCover(int.Parse(post), asset);
            }
            else if (type == "appCover")
            {
                _app.Update(opt => { opt.Cover = asset; });
                AppSettings.Cover = asset;
            }
            else if (type == "appLogo")
            {
                _app.Update(opt => { opt.Logo = asset; });
                AppSettings.Logo = asset;
            }
            else if (type == "avatar")
            {
                var user = _db.Authors.Single(a => a.AppUserName == User.Identity.Name);
                user.Avatar = asset;
                _db.Complete();
            }

            var item = await _ss.Find(a => a.Url == asset, new Pager(1));
            return item.FirstOrDefault();
        }

        [HttpDelete, Route("assets/remove")]
        public IActionResult Remove(string url)
        {
            _ss.DeleteFile(url);

            return Ok("Deleted");
        }

        [HttpPost, Route("assets/upload")]
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

        [HttpPost, Route("[controller]/importfeed")]
        public async Task<IEnumerable<ImportMessage>> ImportFeed(IFormFile file)
        {
            var author = _db.Authors.Single(a => a.AppUserName == User.Identity.Name);

            if(!author.IsAdmin)
                Redirect("~/pages/shared/_error/403");

            var webRoot = Url.Content("~/");

            return await _feed.Import(file, User.Identity.Name, webRoot);
        }
    }
}