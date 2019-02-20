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
        UserManager<AppUser> _um;
        IImportService _feed;

        public AdminController(IDataService db, IImportService feed, IStorageService ss, UserManager<AppUser> um)
        {
            _db = db;
            _feed = feed;
            _um = um;
            _ss = ss;
        }

        public IActionResult Index()
        {
            return Redirect("~/admin/posts");
        }

        [HttpDelete]
        public async Task RemovePost(int id)
        {
            
            var post = _db.BlogPosts.Single(p => p.Id == id);
            var author = _db.Authors.Single(a => a.Id == post.AuthorId);
            var user = _db.Authors.Single(a => a.AppUserName == User.Identity.Name);

            //prevents users from removing other users posts or admin posts --manuta 9-16-2018
            if (user.IsAdmin || author.AppUserName == User.Identity.Name)
            {
                _db.BlogPosts.Remove(post);
                _db.Complete();
            }    
            await Task.CompletedTask;
        }

        [HttpPut]
        public async Task PublishPost(int id, string flag)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            var author = _db.Authors.Single(a => a.Id == post.AuthorId);
            var user = _db.Authors.Single(a => a.AppUserName == User.Identity.Name);
            if (!string.IsNullOrEmpty(flag) && (user.IsAdmin || author.AppUserName == User.Identity.Name))
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
            var author = _db.Authors.Single(a => a.Id == post.AuthorId);
            var user = _db.Authors.Single(a => a.AppUserName == User.Identity.Name);
            if (!string.IsNullOrEmpty(flag) && (user.IsAdmin || author.AppUserName == User.Identity.Name))
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
                var cover = _db.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogCover);
                if (cover == null)
                    _db.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogCover, Content = asset });
                else
                    cover.Content = asset;
                _db.Complete();
            }
            else if (type == "appLogo")
            {
                var logo = _db.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogLogo);
                if (logo == null)
                    _db.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogLogo, Content = asset });
                else
                    logo.Content = asset;
                _db.Complete();
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

        [HttpDelete, Route("[controller]/notifications/remove/{id}")]
        public async Task RemoveNotification(int id)
        {
            var note = _db.Notifications.Single(n => n.Id == id);
            _db.Notifications.Remove(note);
            _db.Complete();
            await Task.CompletedTask;
        }
    }
}