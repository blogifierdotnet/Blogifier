using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blogifier.Core.Data.Repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        BlogifierDbContext _db;
        public ProfileRepository(BlogifierDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<ProfileListItem> ProfileList(Expression<Func<Profile, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var all = _db.Profiles.Include(p => p.Assets).Include(p => p.BlogPosts);
            pager.Configure(all.Count());

            var posts = all.Where(predicate).Skip(skip).Take(pager.ItemsPerPage).ToList();

            // this is work around EF 1.1 not handling "count" fields
            // and can be simplified when moved to EF 2
            // update: instead of fixing count, EF 2.0 also broke workaround... searching for solution.

            var p2 = posts.Select(p => new
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
                DbUsage = 0,

                // DbUsage = _db.BlogPosts.Where(bp => bp.Profile.Id == p.Id).Sum(bp => (System.Int64)bp.Content.Length),

                AssetCount = p.Assets.Count,
                DownloadCount = _db.Assets.Where(a => a.ProfileId == p.Id).Sum(a => a.DownloadCount),
                DiskUsage = _db.Assets.Where(a => a.ProfileId == p.Id).Sum(a => a.Length),

                LastUpdated = p.LastUpdated
            }).ToList();
           
            return p2.Select(p => new ProfileListItem
            {
                ProfileId = p.ProfileId,
                Title = p.Title,
                Email = p.Email,
                Url = p.Url,

                IdentityName = p.IdentityName,
                AuthorName = p.AuthorName,
                IsAdmin = p.IsAdmin,

                PostCount = p.PostCount,
                PostViews = p.PostViews,
                DbUsage = p.DbUsage,

                AssetCount = p.AssetCount,
                DownloadCount = p.DownloadCount,
                DiskUsage = p.DiskUsage,

                LastUpdated = p.LastUpdated
            });
        }
    }
}
