using Core.Data;
using Core.Helpers;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        [HttpGet]
        public async Task<AssetsModel> Get(int page = 1, string filter = "", string search = "")
        {
            var pager = new Pager(page);
            IEnumerable<AssetItem> items;

            if (string.IsNullOrEmpty(search))
            {
                if (filter == "filterImages")
                {
                    items = await _store.Find(a => a.AssetType == AssetType.Image, pager);
                }
                else if (filter == "filterAttachments")
                {
                    items = await _store.Find(a => a.AssetType == AssetType.Attachment, pager);
                }
                else
                {
                    items = await _store.Find(null, pager);
                }
            }
            else
            {
                items = await _store.Find(a => a.Title.Contains(search), pager);
            }

            if (page < 1 || page > pager.LastPage)
                return null;

            return new AssetsModel
            {
                Assets = items,
                Pager = pager
            };
        }

        [HttpGet("pick")]
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

        [HttpPost("upload")]
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

        [HttpDelete("remove")]
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