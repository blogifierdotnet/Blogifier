using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Controllers.Api
{
    [Authorize]
    [Route("blogifier/api/[controller]")]
    public class AssetsController : Controller
    {
        IUnitOfWork _db;
        ILogger _logger;

        public AssetsController(IUnitOfWork db, ILogger<AssetsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // GET: api/assets/2?search=foo&filter=filterImages
        [HttpGet("{page:int?}")]
        public AdminAssetList Get(int page, string search, string filter)
        {
            var profile = GetProfile();
            var pager = new Pager(page);
            IEnumerable<Asset> assets;

            var term = search == null || search == "null" ? "" : search;
            var fltr = filter == null || filter == "null" ? "" : filter;

            if (filter == "filterImages")
            {
                assets = _db.Assets.Find(a => a.ProfileId == profile.Id && a.Title.Contains(term) && a.AssetType == AssetType.Image, pager);
            }
            else if (filter == "filterAttachments")
            {
                assets = _db.Assets.Find(a => a.ProfileId == profile.Id && a.Title.Contains(term) && a.AssetType == AssetType.Attachment, pager);
            }
            else
            {
                assets = _db.Assets.Find(a => a.ProfileId == profile.Id && a.Title.Contains(term), pager);
            }

            return new AdminAssetList { Assets = assets, Pager = pager };
        }

        // GET api/assets/asset/5
        [HttpGet("asset/{id:int}")]
        public async Task<Asset> GetSingle(int id)
        {
            var model = _db.Assets.Single(a => a.Id == id);
            return await Task.Run(() => model);
        }

        // GET: api/assets/profilelogo/3
        [HttpGet]
        [Route("{type}/{id:int}")]
        public Asset UpdateProfileImage(string type, int id)
        {
            var asset = _db.Assets.Single(a => a.Id == id);
            type = type.ToLower();

            if (!string.IsNullOrEmpty(type))
            {
                var profile = GetProfile();

                if (type == "profilelogo")
                {
                    profile.Logo = asset.Url;
                }
                if (type == "profileavatar")
                {
                    profile.Avatar = asset.Url;
                }
                if (type == "profileimage")
                {
                    profile.Image = asset.Url;
                }
                _db.Complete();
            }
            return asset;
        }

        // GET: api/assets/postimage/3/5
        [HttpGet]
        [Route("postimage/{assetId:int}/{postId:int}")]
        public Asset UpdatePostImage(string type, int assetId, int postId)
        {
            var asset = _db.Assets.Single(a => a.Id == assetId);
            if(postId > 0)
            {
                var post = _db.BlogPosts.Single(p => p.Id == postId);
                post.Image = asset.Url;
                _db.Complete();
            }
            return asset;
        }

        // POST: api/assets/upload
        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult> TinyMceUpload(IFormFile file)
        {
            var asset = await SaveFile(file);
            var location = asset.Url;
            return Json(new { location });
        }

        // POST api/assets/multiple
        [HttpPost]
        [Route("multiple")]
        public async Task<IActionResult> Multiple(ICollection<IFormFile> files)
        {
            foreach (var file in files)
            {
                await SaveFile(file);
            }
            return Ok("Created");
        }

        // DELETE api/assets/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var asset = _db.Assets.Single(a => a.Id == id);
            if (asset == null)
                return NotFound();

            var blog = GetProfile();
            try
            {
                var storage = new BlogStorage(blog.Slug);
                storage.DeleteFile(asset.Path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            _db.Assets.Remove(asset);
            _db.Complete();

            // reset profile image to default
            // if asset was removed
            var profiles = _db.Profiles.Find(p => p.Image == asset.Url || p.Avatar == asset.Url || p.Logo == asset.Url).ToList();
            if(profiles != null)
            {
                foreach (var item in profiles)
                {
                    if (item.Image == asset.Url) item.Image = null;
                    if (item.Avatar == asset.Url) item.Avatar = null;
                    if (item.Logo == asset.Url) item.Logo = null;
                    _db.Complete();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/assets/profilelogo
        [HttpDelete("{type}")]
        public IActionResult Delete(string type)
        {
            var profile = GetProfile();

            if (type == "profileAvatar")
                profile.Avatar = null;

            if(type == "profileLogo")
                profile.Logo = null;

            if (type == "profileImage")
                profile.Image = null;

            _db.Complete();
            return new NoContentResult();
        }

        // DELETE api/assets/resetpostimage/5
        [HttpDelete("resetpostimage/{id:int}")]
        public IActionResult ResetPostImage(int id)
        {
            var post = _db.BlogPosts.Single(p => p.Id == id);
            post.Image = null;
            _db.Complete();
            return Json("admin/editor/" + id);
        }

        async Task<Asset> SaveFile(IFormFile file)
        {
            var profile = GetProfile();
            var storage = new BlogStorage(profile.Slug);
            var path = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Month);

            var asset = await storage.UploadFormFile(file, Url.Content("~/"), path);

            // sometimes we just want to override uploaded file
            // only add DB record if asset does not exist yet
            var existingAsset = _db.Assets.Find(a => a.Path == asset.Path).FirstOrDefault();
            if (existingAsset == null)
            {
                asset.ProfileId = profile.Id;
                asset.LastUpdated = SystemClock.Now();

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
                existingAsset.LastUpdated = SystemClock.Now();
            }
            _db.Complete();
            return asset;
        }

        Profile GetProfile()
        {
            try
            {
                return _db.Profiles.Single(p => p.IdentityName == User.Identity.Name);
            }
            catch
            {
                RedirectToAction("Login", "Account");
            }
            return null;
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
