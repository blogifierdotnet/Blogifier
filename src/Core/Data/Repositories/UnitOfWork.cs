using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository BlogPosts { get; }
        IBlogRepository Blogs { get; }
        IAuthorRepository Authors { get; }

        int Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _um;
        private readonly SignInManager<AppUser> _sm;

        public UnitOfWork(AppDbContext db, UserManager<AppUser> um, SignInManager<AppUser> sm)
        {
            _db = db;
            _um = um;
            _sm = sm;

            BlogPosts = new PostRepository(_db, _um);
            Blogs = new BlogRepository(_db);
            Authors = new AuthorRepository(_db, _um, _sm);
        }

        public IPostRepository BlogPosts { get; private set; }
        public IBlogRepository Blogs { get; private set; }
        public IAuthorRepository Authors { get; private set; }

        public int Complete()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}