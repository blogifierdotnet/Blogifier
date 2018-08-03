using Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IUnitOfWork _db;

        public AdminController(IUnitOfWork db)
        {
            _db = db;
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
    }
}