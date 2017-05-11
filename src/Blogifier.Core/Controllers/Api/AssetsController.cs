using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Services.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AssetsController(IUnitOfWork db)
        {
            _db = db;
        }

        // GET: api/assets
        [HttpGet]
        public IEnumerable<Asset> Get()
        {
            var profile = GetProfile();
            return _db.Assets.Find(a => a.ProfileId == profile.Id).OrderByDescending(a => a.LastUpdated);
        }

        // POST api/assets/single/{type}
        [HttpPost]
        [Route("single/{type}")]
        public async Task<Asset> Post(IFormFile file, string type)
        {
            if (file == null)
                return null;

            var asset = await SaveFile(file);

            if (!string.IsNullOrEmpty(type))
            {
                var profile = GetProfile();

                if (type == "profileLogo")
                {
                    profile.Logo = asset.Url;
                }
                if (type == "profileAvatar")
                {
                    profile.Avatar = asset.Url;
                }
                if (type == "profileImage")
                {
                    profile.Image = asset.Url;
                }
                _db.Complete();
            }
            return asset;
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

        async Task<Asset> SaveFile(IFormFile file)
        {
            var profile = GetProfile();
            var storage = new BlogStorage(profile.Slug);
            var path = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Month);

            var asset = await storage.UploadFormFile(file, Url.Content("~/"), path);
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
