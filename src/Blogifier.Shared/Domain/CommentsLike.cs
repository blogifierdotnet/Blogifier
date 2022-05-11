using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared
{
    public class CommentsLike
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long CommentId { get; set; }
        public Comment Comment { get; set; }
        public DateTime ExpressDate { get; set; }
        public string LikedByGuid { get; set; }
    }
}