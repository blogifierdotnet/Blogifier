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
    public interface ICommentsProvider
    {
        // Task<List<Post>> GetPosts(PublishedStatus filter, PostType postType);
        // Task<List<Post>> SearchPosts(string term);
        Task<IEnumerable<CommentDTO>> GetCommentsById(int id);
        Task<IEnumerable<CommentDTO>> GetCommentsBySlug(string slug);
        // Task<string> GetSlugFromTitle(string title);
        Task<bool> Add(Comment comment);
        Task<bool> Update(Comment comment);
        // Task<bool> Publish(int id, bool publish);
        // Task<bool> Featured(int id, bool featured);
        // Task<IEnumerable<PostItem>> GetPostItems();
        // Task<PostModel> GetPostModel(string slug);
        // Task<IEnumerable<PostItem>> GetPopular(Pager pager, int author = 0);
        // Task<IEnumerable<PostItem>> Search(Pager pager, string term, int author = 0, string include = "", bool sanitize = false);
        // Task<IEnumerable<PostItem>> GetList(Pager pager, int author = 0, string category = "", string include = "", bool sanitize = true);
        Task<bool> Remove(int id);
    }
    public class CommentsProvider : ICommentsProvider
    {
        private readonly AppDbContext _db;

        public CommentsProvider(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<CommentDTO>> GetCommentsBySlug(string slug)
        {
            var commentDTOs = new List<CommentDTO>();
            var tempPost = await _db.Posts.AsNoTracking().Where(p => p.Slug == slug).FirstOrDefaultAsync();
            int tempId;
            try { tempId = tempPost.Id; }
            finally
            {
                if (tempPost is not null)
                    ((IDisposable)tempPost).Dispose();
            }

            System.Console.WriteLine(tempId);
            return await GetCommentsById(tempId);
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsById(int id)
        {
            var commentDTOs = new List<CommentDTO>();
            var mainComments = await _db.Comments.AsNoTracking()
                .Where(c => c.PostId == id)
                .Where(c => c.ParentId == null)
                .OrderBy(c => c.PostDate)
                .ToListAsync();

            foreach (var comment in mainComments)
            {
                var subComments = await _db.Comments.AsNoTracking()
                    .Where(c => c.PostId == id)
                    .Where(c => c.ParentId == comment.Id)
                    .OrderBy(c => c.PostDate)
                    .ToListAsync();

                commentDTOs.Add(new CommentDTO() { MainComment = comment, SubComments = subComments });
            }
            return commentDTOs;
        }
        public async Task<bool> Add(Comment comment)
        {
            comment.PostDate = DateTime.UtcNow;
            comment.UpdatedDate = DateTime.UtcNow;

            await _db.Comments.AddAsync(comment);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> Update(Comment comment)
        {
            var existing = await _db.Comments.Where(c => c.Id == comment.Id).FirstOrDefaultAsync();
            if (existing == null)
                return false;

            existing.CommentContent = comment.CommentContent;
            existing.UpdatedDate = DateTime.UtcNow;

            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> Remove(int id)
        {
            var existing = await _db.Comments.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existing == null)
                return false;

            // _db.Comments.Remove(existing);
            existing.Hidden = true;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}