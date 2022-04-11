using Blogifier.Core.Providers;
using Blogifier.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityModel;

namespace Blogifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentsProvider _commentProvider;

        public CommentController(ICommentsProvider commentProvider)
        {
            _commentProvider = commentProvider;
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments(string slug)
        {
            return new ActionResult<IEnumerable<CommentDTO>>(await _commentProvider.GetCommentsBySlug(slug));
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<bool>> Add(Comment comment)
        {
            System.Console.WriteLine("Front End pass !!!!!");
            comment.CommentedUserId = User.FindFirst(c => c.Type == JwtClaimTypes.Subject).Value;
            comment.CommentedUserName = User.FindFirst(c => c.Type == JwtClaimTypes.Name).Value;
            System.Console.WriteLine(comment.CommentedUserId);
            System.Console.WriteLine(comment.CommentedUserName);
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
    }
}
