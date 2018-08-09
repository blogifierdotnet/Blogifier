using Core.Data;
using Core.Data.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Produces("application/json")]
    public class ApiController : Controller
    {
        IDataService _db;
        IStorageService _storage;
        IFeedImportService _feed;

        public ApiController(IDataService db, IStorageService storage, IFeedImportService feed)
        {
            _db = db;
            _storage = storage;
            _feed = feed;
        }

        [HttpGet, Authorize, Route("[controller]/author/{id}")]
        public async Task<Author> GetAuthor(string id)
        {
            return await _db.Authors.GetItem(a => a.AppUserId == id);
        }

        [HttpPost, Authorize, Route("[controller]/author")]
        public async Task CreateAuthor([FromBody]RegisterModel model)
        {
            if (!IsAdmin())
                Redirect("~/error/403");

            var author = _db.Authors.Single(a => a.AppUserName == model.UserName);
            if (author == null)
            {
                author = new Author
                {
                    AppUserName = model.UserName,
                    Email = model.Email
                };
                await _db.Authors.Save(author);
            }
        }

        [HttpPut, Authorize, Route("[controller]/author")]
        public async Task UpdateAuthor(int id, [FromBody]Author model)
        {
            var author = _db.Authors.Single(a => a.Id == model.Id);
            author.DisplayName = model.DisplayName;
            author.Email = model.Email;

            await _db.Authors.Save(author);
        }

        [HttpDelete, Authorize, Route("[controller]/author/{id}")]
        public async Task RemoveAuthor(int id)
        {
            var author = _db.Authors.Single(a => a.Id == id);

            if (!IsAdmin() || author.AppUserName == User.Identity.Name)
                Redirect("~/error/403");

            await _db.Authors.Remove(id);
            _storage.DeleteFolder(author.AppUserName);
        }

        [HttpPost, Authorize, Route("[controller]/feedimport")]
        public async Task<IEnumerable<ImportMessage>> ImportFeed(IFormFile file)
        {
            if (!IsAdmin())
                Redirect("~/error/403");

            var user = _db.Authors.Single(a => a.AppUserName == User.Identity.Name);

            return await _feed.Import(file, User.Identity.Name);
        }

        bool IsAdmin()
        {
            return _db.Authors.Single(a => a.AppUserName == User.Identity.Name).IsAdmin;
        }
    }
}