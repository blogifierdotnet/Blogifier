using Blogifier.Core.Data;
using Blogifier.Core.Extensions;
using Blogifier.Shared;
using Blogifier.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
    public interface ICommentsLikeProvider
    {
        Task<int> GetCommentsLikeById(long id);
        Task<bool> Add(long id, string guid);
        Task<bool> Revoke(long id, string guid);
        Task<bool> Check(long id, string guid);
    }
    public class CommentsLikeProvider : ICommentsLikeProvider
    {
        private readonly AppDbContext _db;
        public CommentsLikeProvider(AppDbContext db)
        {
            _db = db;
        }
        public async Task<int> GetCommentsLikeById(long id)
        {
            return await Task.Run(() => _db.CommentsLikes.AsNoTracking().Where(cl => cl.CommentId == id).Count());
        }

        public async Task<bool> Add(long id, string guid)
        {
            var commentsLike = new CommentsLike()
            {
                CommentId = id,
                LikedByGuid = guid,
                ExpressDate = DateTime.UtcNow
            };

            await _db.CommentsLikes.AddAsync(commentsLike);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> Revoke(long id, string guid)
        {
            var existing = await _db.CommentsLikes.Where(cl => cl.CommentId == id && cl.LikedByGuid == guid).FirstOrDefaultAsync();
            if (existing == null)
                return false;

            _db.CommentsLikes.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Check(long id, string guid)
        {
            var existing = await _db.CommentsLikes.AsNoTracking()
                .Where(cl => cl.CommentId == id && cl.LikedByGuid == guid).FirstOrDefaultAsync();
            if (existing == null)
                return false;
            return true;
        }
    }
}