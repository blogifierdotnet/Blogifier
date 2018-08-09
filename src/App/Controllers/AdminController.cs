using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IDataService _db;
        IFeedImportService _feed;

        public AdminController(IDataService db, IFeedImportService feed)
        {
            _db = db;
            _feed = feed;
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

        [HttpPost, Route("[controller]/importfeed")]
        public async Task<IEnumerable<ImportMessage>> ImportFeed(IFormFile file)
        {
            var author = _db.Authors.Single(a => a.AppUserName == User.Identity.Name);

            if(!author.IsAdmin)
                Redirect("~/pages/shared/_error/403");

            return await _feed.Import(file, User.Identity.Name);
        }
    }
}