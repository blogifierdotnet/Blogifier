using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;

namespace Blogifier.Core.Data.Repositories
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        BlogifierDbContext _db;
        public AssetRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }
    }
}