using Core.Data;
using Core.Data.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Produces("application/json")]
    public class ApiController : Controller
    {
        IUnitOfWork _db;
        IStorageService _storage;

        public ApiController(IUnitOfWork db, IStorageService storage)
        {
            _db = db;
            _storage = storage;
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

            var user = _db.Authors.Single(a => a.AppUserName == model.UserName);
            //if (user == null)
            //{
            //    user = new AppUser
            //    {
            //        UserName = model.UserName,
            //        Email = model.Email
            //    };
            //    await _db.Authors.SaveUser(user, model.Password);
            //}
        }

        [HttpPut, Authorize, Route("[controller]/author")]
        public async Task UpdateAuthor(int id, [FromBody]Author model)
        {
            var user = _db.Authors.Single(a => a.Id == model.Id);
            user.DisplayName = model.DisplayName;
            //user.Email = model.Email;

            await _db.Authors.SaveUser(user);
        }

        [HttpDelete, Authorize, Route("[controller]/author/{id}")]
        public async Task RemoveAuthor(string id)
        {
            var author = _db.Authors.Single(a => a.AppUserId == id);

            if (!IsAdmin() || author.AppUserName == User.Identity.Name)
                Redirect("~/error/403");

            await _db.Authors.RemoveUser(author);
            _storage.DeleteFolder(author.AppUserName);
        }

        bool IsAdmin()
        {
            return _db.Authors.Single(a => a.AppUserName == User.Identity.Name).IsAdmin;
        }
    }
}