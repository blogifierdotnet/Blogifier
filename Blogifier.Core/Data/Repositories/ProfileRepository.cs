using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        BlogifierDbContext _db;
        public ProfileRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }

        public  async Task<IEnumerable<ProfileListItem>> ProfileList(Expression<Func<Profile, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var all = _db.Profiles.Include(p => p.Assets).Include(p => p.BlogPosts).Where(predicate);

            pager.Configure(await all.CountAsync());

            var posts = await all.OrderBy(p => p.Id).Skip(skip).Take(pager.ItemsPerPage).ToListAsync();

            return posts.Select(p => new ProfileListItem
            {
                ProfileId = p.Id,
                Title = p.Title,
                Email = p.AuthorEmail,
                Url = ApplicationSettings.BlogRoute + "/" + p.Slug,

                IdentityName = p.IdentityName,
                AuthorName = p.AuthorName,
                IsAdmin = p.IsAdmin,

                PostCount = p.BlogPosts.Count,
                PostViews = _db.BlogPosts.Where(bp => bp.Profile.Id == p.Id).Sum(bp => bp.PostViews),
                DbUsage = _db.BlogPosts.Where(bp => bp.Profile.Id == p.Id).Sum(bp => Convert.ToInt32(bp.Content.Length)),

                AssetCount = p.Assets.Count,
                DownloadCount = _db.Assets.Where(a => a.ProfileId == p.Id).Sum(a => a.DownloadCount),
                DiskUsage = _db.Assets.Where(a => a.ProfileId == p.Id).Sum(a => a.Length),

                LastUpdated = p.LastUpdated
            });
        }
    }
}
