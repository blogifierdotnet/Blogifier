using Core.Data.Models;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IAuthorRepository : IRepository<AppUser>
    {
        Task<AuthorItem> GetItem(Expression<Func<AppUser, bool>> predicate);
        Task<IEnumerable<AuthorItem>> GetItems(Expression<Func<AppUser, bool>> predicate, Pager pager);
        Task<IdentityResult> SaveUser(AppUser user, string pwd = "");
        Task RemoveUser(AppUser user);
        Task ChangePassword(ChangePasswordModel model);
    }

    public class AuthorRepository : Repository<AppUser>, IAuthorRepository
    {
        AppDbContext _db;
        UserManager<AppUser> _um;
        SignInManager<AppUser> _sm;
        ItemMapper _map;

        public AuthorRepository(AppDbContext db, UserManager<AppUser> um, SignInManager<AppUser> sm) : base(db)
        {
            _db = db;
            _um = um;
            _sm = sm;
            _map = new ItemMapper(db, um);
        }

        public async Task<AuthorItem> GetItem(Expression<Func<AppUser, bool>> predicate)
        {
            var user = _um.Users.Single(predicate);
            return await Task.FromResult(_map.MapUserToAuthor(user));
        }

        public async Task<IEnumerable<AuthorItem>> GetItems(Expression<Func<AppUser, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var users = _db.Users.Where(predicate)
                .OrderByDescending(u => u.Created).ToList();

            pager.Configure(users.Count);

            var list = users.Skip(skip).Take(pager.ItemsPerPage).ToList();

            return await Task.FromResult(_map.MapUsersToAuthors(list));
        }

        public async Task<IdentityResult> SaveUser(AppUser user, string pwd = "")
        {
            if (user.Created == DateTime.MinValue)
            {
                user.DisplayName = user.UserName;
                user.Created = SystemClock.Now();
                return await _um.CreateAsync(user, pwd);
            }
            else
            {
                return await _um.UpdateAsync(user);
            } 
        }

        public async Task ChangePassword(ChangePasswordModel model)
        {
            var user = _db.Users.Single(u => u.UserName == model.UserName);

            var result = await _um.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                throw new ApplicationException(result.Errors.First().Description);
            }

            await _sm.SignInAsync(user, isPersistent: false);
        }

        public async Task RemoveUser(AppUser user)
        {
            var storage = new BlogStorage("");
            storage.DeleteFolder(user.UserName);

            var authorPosts = _db.BlogPosts
                .Where(p => p.UserId == user.Id).ToList();

            if (authorPosts != null && authorPosts.Any())
                _db.BlogPosts.RemoveRange(authorPosts);

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}