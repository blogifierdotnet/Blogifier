using Core.Data;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        IDataService _data;
        IStorageService _store;

        public AssetsController(IDataService data, IStorageService store)
        {
            _data = data;
            _store = store;
        }

        /// <summary>
        /// Get list of assets - user saved images and files
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="filter">filterImages or filterAttachments</param>
        /// <param name="search">Search term</param>
        /// <returns>Model containing collection of assets and Pager object</returns>
        [HttpGet]
        public async Task<AssetsModel> Get(int page = 1, string filter = "", string search = "")
        {
            var blog = await _data.CustomFields.GetBlogSettings();
            var pager = new Pager(page, blog.ItemsPerPage);
            IEnumerable<AssetItem> items;

            if (string.IsNullOrEmpty(search))
            {
                if (filter == "filterImages")
                {
                    items = await _store.Find(a => a.AssetType == AssetType.Image, pager, "", !User.Identity.IsAuthenticated);
                }
                else if (filter == "filterAttachments")
                {
                    items = await _store.Find(a => a.AssetType == AssetType.Attachment, pager, "", !User.Identity.IsAuthenticated);
                }
                else
                {
                    items = await _store.Find(null, pager, "", !User.Identity.IsAuthenticated);
                }
            }
            else
            {
                items = await _store.Find(a => a.Title.Contains(search), pager, "", !User.Identity.IsAuthenticated);
            }

            if (page < 1 || page > pager.LastPage)
                return null;

            return new AssetsModel
            {
                Assets = items,
                Pager = pager
            };
        }

        /// <summary>
        /// Select an asset in the File Manager (authentication required)
        /// </summary>
        /// <param name="type">Type of asset (post cover, logo, avatar or post image/attachment)</param>
        /// <param name="asset">Selected asset</param>
        /// <param name="post">Post ID</param>
        /// <returns>Asset Item</returns>
        [HttpGet("pick")]
        [Authorize]
        public async Task<AssetItem> Pick(string type, string asset, string post)
        {
            if (type == "postCover")
            {
                await _data.BlogPosts.SaveCover(int.Parse(post), asset);
            }
            else if (type == "appCover")
            {
                var cover = _data.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogCover);
                if (cover == null)
                    _data.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogCover, Content = asset });
                else
                    cover.Content = asset;
                _data.Complete();
            }
            else if (type == "appLogo")
            {
                var logo = _data.CustomFields.Single(f => f.AuthorId == 0 && f.Name == Constants.BlogLogo);
                if (logo == null)
                    _data.CustomFields.Add(new CustomField { AuthorId = 0, Name = Constants.BlogLogo, Content = asset });
                else
                    logo.Content = asset;
                _data.Complete();
            }
            else if (type == "avatar")
            {
                var user = _data.Authors.Single(a => a.AppUserName == User.Identity.Name);
                user.Avatar = asset;
                _data.Complete();
            }

            var item = await _store.Find(a => a.Url == asset, new Pager(1));
            return item.FirstOrDefault();
        }

        /// <summary>
        /// Upload file(s) to user data store (authentication required)
        /// </summary>
        /// <param name="files">Selected files</param>
        /// <returns>Success or internal error</returns>
        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            try
            {
                foreach (var file in files)
                {
                    await SaveFile(file);
                }
                return Ok("Created");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "File upload error");
            }
        }

        /// <summary>
        /// Remove file from user data store (authentication required)
        /// </summary>
        /// <param name="url">Relative URL of the file to remove</param>
        /// <returns></returns>
        [HttpDelete("remove")]
        [Authorize]
        public IActionResult Remove(string url)
        {
            try
            {
                _store.DeleteFile(url);
                return Ok("Deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "File delete error");
            }
        }

        async Task<AssetItem> SaveFile(IFormFile file)
        {
            var path = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Month);
            return await _store.UploadFormFile(file, Url.Content("~/"), path);
        }
    }
}