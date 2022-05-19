using System.Linq;
using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;

namespace Blogifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentsProvider _commentProvider;
        private readonly ICommentsLikeProvider _commentsLikeProvider;

        public CommentController(ICommentsProvider commentProvider, ICommentsLikeProvider commentsLikeProvider)
        {
            _commentProvider = commentProvider;
            _commentsLikeProvider = commentsLikeProvider;
        }

        [HttpGet("get/{slug}")]
        public async Task<ActionResult<List<CommentDTO>>> GetComments(string slug)
        {
            return new ActionResult<List<CommentDTO>>(await _commentProvider.GetCommentsBySlug(slug));
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<bool>> Add(Comment comment)
        {
            System.Console.WriteLine("Front End pass to Add Method!!!!!");
            foreach (var item in User.Claims)
            {
                System.Console.WriteLine("{0}===>{1}", item.Type, item.Value);
            }

            comment.Hidden = false;
            return await _commentProvider.Add(comment);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<bool>> Update(Comment comment)
        {
            return await _commentProvider.Update(comment);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Remove(int id)
        {
            return await _commentProvider.Remove(id);
        }

        [HttpGet("getlikes/{id}")]
        public async Task<ActionResult<int>> GetLikes(long id)
        {
            return new ActionResult<int>(await _commentsLikeProvider.GetCommentsLikeById(id));
        }

        [Authorize]
        [HttpGet("checklikes/{id}")]
        public async Task<ActionResult<bool>> Check(long id)
        {
            var guid = User.FindFirstValue(JwtClaimTypes.Subject);
            return new ActionResult<bool>(await _commentsLikeProvider.Check(id, guid));
        }

        [Authorize]
        [HttpPost("addlikes/{id}")]
        public async Task<ActionResult<bool>> Add(long id)
        {
            var guid = User.FindFirstValue(JwtClaimTypes.Subject);
            return await _commentsLikeProvider.Add(id, guid);
        }
        [Authorize]
        [HttpPost("revokelikes/{id}")]
        public async Task<ActionResult<bool>> Revoke(long id)
        {
            var guid = User.FindFirstValue(JwtClaimTypes.Subject);
            return await _commentsLikeProvider.Revoke(id, guid);
        }
    }
}
