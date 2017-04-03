using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;

namespace Blogifier.Core.Data.Repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        BlogifierDbContext _db;
        public ProfileRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
