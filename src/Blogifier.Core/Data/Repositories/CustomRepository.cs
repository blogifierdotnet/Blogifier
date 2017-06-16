using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;

namespace Blogifier.Core.Data.Repositories
{
    public class CustomRepository : Repository<CustomField>, ICustomRepository
    {
        BlogifierDbContext _db;
        public CustomRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }
    }
}