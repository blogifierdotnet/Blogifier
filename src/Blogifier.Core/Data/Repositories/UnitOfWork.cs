using Blogifier.Core.Data.Interfaces;

namespace Blogifier.Core.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogifierDbContext _db;

        public UnitOfWork(BlogifierDbContext db)
        {
            _db = db;
            Assets = new AssetRepository(_db);
            Blogs = new BlogRepository(_db);
            Categories = new CategoryRepository(_db);
            Posts = new PostRepository(_db);
        }

        public IAssetRepository Assets { get; private set; }
        public IBlogRepository Blogs { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IPostRepository Posts { get; private set; }

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
