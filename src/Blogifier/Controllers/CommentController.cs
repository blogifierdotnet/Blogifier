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

        public CommentController(ICommentsProvider commentProvider)
        {
            _commentProvider = commentProvider;
        }

        // [Authorize]
        [HttpGet("{slug}")]
        public async Task<ActionResult<List<CommentDTO>>> GetComments(string slug)
        {
            // System.Console.WriteLine("Get OK");
            System.Console.WriteLine("==============Get Method Here===========");
            System.Console.WriteLine(User.Claims.Count());
            foreach (var item in User.Claims)
            {
                System.Console.WriteLine("{0}=>{1}", item.Type, item.Value);
            }
            return new ActionResult<List<CommentDTO>>(await _commentProvider.GetCommentsBySlug(slug));
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<bool>> Add([FromForm] Comment comment)
        {
            System.Console.WriteLine("Front End pass !!!!!");
            // var tempComment = Request.Form["tempComment"].ToString();
            // System.Console.WriteLine(tempComment);
            System.Console.WriteLine("Add Method Here===========");
            System.Console.WriteLine(User.Claims.Count());
            foreach (var item in User.Claims)
            {
                System.Console.WriteLine("{0}=>{1}", item.Type, item.Value);
            }
            // comment.CommentedUserId = User.FindFirst(c => c.Type == JwtClaimTypes.Subject).Value;
            // comment.CommentedUserName = User.FindFirst(c => c.Type == JwtClaimTypes.Name).Value;
            // System.Console.WriteLine(comment.CommentedUserId);
            // System.Console.WriteLine(comment.CommentedUserName);
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
