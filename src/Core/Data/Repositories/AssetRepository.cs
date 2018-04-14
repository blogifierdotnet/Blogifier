using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IAssetRepository : IRepository<Asset>
    {
        Task<IEnumerable<Asset>> Find(Expression<Func<Asset, bool>> predicate, Pager pager);
        Task<Asset> SavePostCover(int postId, int assetId);
    }

    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        AppDbContext _db;
        UserManager<AppUser> _um;

        public AssetRepository(AppDbContext db, UserManager<AppUser> um) : base(db)
        {
            _db = db;
            _um = um;
        }

        public async Task<IEnumerable<Asset>> Find(Expression<Func<Asset, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var items = _db.Assets.Where(predicate)
                .OrderByDescending(p => p.Published).ToList();

            pager.Configure(items.Count);

            var page = items.Skip(skip).Take(pager.ItemsPerPage).ToList();

            return await Task.FromResult(page);
        }

        public async Task<Asset> SavePostCover(int postId, int assetId)
        {
            var post = _db.BlogPosts.Single(p => p.Id == postId);
            var asset = _db.Assets.Single(a => a.Id == assetId);

            post.Cover = asset.Url;
            await _db.SaveChangesAsync();

            return await Task.FromResult(asset);
        }
    }
}