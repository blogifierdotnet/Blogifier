using Core.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetItem(Expression<Func<Author, bool>> predicate);
        Task<IEnumerable<Author>> GetItems(Expression<Func<Author, bool>> predicate, Pager pager);
        Task<IdentityResult> SaveUser(Author user, string pwd = "");
        Task RemoveUser(Author user);
        Task ChangePassword(ChangePasswordModel model);
    }

    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        AppDbContext _db;

        public AuthorRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Author> GetItem(Expression<Func<Author, bool>> predicate)
        {
            var author = _db.Authors.Single(predicate);

            author.Avatar = author.Avatar ?? AppSettings.Avatar;

            return await Task.FromResult(author);
        }

        public async Task<IEnumerable<Author>> GetItems(Expression<Func<Author, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var users = _db.Authors.Where(predicate)
                .OrderBy(u => u.DisplayName).ToList();

            pager.Configure(users.Count);

            var list = users.Skip(skip).Take(pager.ItemsPerPage).ToList();

            return await Task.FromResult(list);
        }

        public async Task<IdentityResult> SaveUser(Author user, string pwd = "")
        {
            //if (user.Created == DateTime.MinValue)
            //{
            //    user.DisplayName = user.UserName;
            //    user.Created = SystemClock.Now();
            //    return await _um.CreateAsync(user, pwd);
            //}
            //else
            //{
            //    return await _um.UpdateAsync(user);
            //} 
            return null;
        }

        public async Task ChangePassword(ChangePasswordModel model)
        {
            //var user = _db.Users.Single(u => u.UserName == model.UserName);

            //var result = await _um.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            //if (!result.Succeeded)
            //{
            //    throw new ApplicationException(result.Errors.First().Description);
            //}

            //await _sm.SignInAsync(user, isPersistent: false);
        }

        public async Task RemoveUser(Author user)
        {
            //var authorPosts = _db.BlogPosts
            //    .Where(p => p.UserId == user.Id).ToList();

            //if (authorPosts != null && authorPosts.Any())
            //    _db.BlogPosts.RemoveRange(authorPosts);

            //_db.Users.Remove(user);
            //await _db.SaveChangesAsync();
        }
    }
}