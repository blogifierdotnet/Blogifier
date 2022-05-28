using System;
using System.Collections.Generic;

namespace Blogifier.Shared
{
    public class CommentDTO
    {
        public Comment MainComment { get; set; }
        public List<Comment> SubComments { get; set; }
        // public List<Comment> All()
        // {
        //     List<Comment> all = SubComments;
        //     all.Add(MainComment);
        //     return all;
        // }
    }
}