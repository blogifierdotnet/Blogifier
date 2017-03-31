using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;

namespace Blogifier.Core.Data.Repositories
{
    public class BlogRepository : Repository<Profile>, IBlogRepository
    {
        BlogifierDbContext _db;
        public BlogRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
