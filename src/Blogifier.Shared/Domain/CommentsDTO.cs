using System;
using System.Collections.Generic;

namespace Blogifier.Shared
{
    public class CommentDTO
    {
        public Comment MainComment { get; set; }
        public IEnumerable<Comment> SubComments { get; set; }
    }
}