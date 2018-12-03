using Core.Helpers;
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
        Task<IEnumerable<Author>> GetList(Expression<Func<Author, bool>> predicate, Pager pager);
        Task Save(Author author);
        Task Remove(int id);
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
            try
            {
                var author = _db.Authors.Single(predicate);

                if (author != null)
                {
                    author.Avatar = author.Avatar ?? AppSettings.Avatar;
                }

                return await Task.FromResult(author);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Author>> GetList(Expression<Func<Author, bool>> predicate, Pager pager)
        {
            var take = pager.ItemsPerPage == 0 ? 10 : pager.ItemsPerPage;
            var skip = pager.CurrentPage * take - take;

            var users = _db.Authors.Where(predicate)
                .OrderBy(u => u.DisplayName).ToList();

            pager.Configure(users.Count);

            var list = users.Skip(skip).Take(take).ToList();

            return await Task.FromResult(list);
        }

        public async Task Save(Author author)
        {
            if (author.Created == DateTime.MinValue)
            {
                author.DisplayName = author.AppUserName;
                author.Avatar = AppSettings.Avatar;
                author.Created = SystemClock.Now();
                await _db.Authors.AddAsync(author);
            }
            else
            {
                var dbAuthor = _db.Authors.Single(a => a.Id == author.Id);

                dbAuthor.DisplayName = author.DisplayName;
                dbAuthor.Avatar = author.Avatar;
                dbAuthor.Email = author.Email;
                dbAuthor.IsAdmin = author.IsAdmin;
                dbAuthor.Created = SystemClock.Now();

                _db.Authors.Update(dbAuthor);
            }

            await _db.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var authorPosts = _db.BlogPosts
                .Where(p => p.AuthorId == id).ToList();

            if (authorPosts != null && authorPosts.Any())
                _db.BlogPosts.RemoveRange(authorPosts);

            _db.Authors.Remove(_db.Authors.Single(a => a.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}