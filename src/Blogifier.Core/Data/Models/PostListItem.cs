using System;

namespace Blogifier.Core.Data.Models
{
    public class PostListItem : IEquatable<PostListItem>
    {
        public int BlogPostId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }

        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string BlogSlug { get; set; }

        public int PostViews { get; set; }

        public bool Equals(PostListItem other)
        {
            if (BlogPostId == other.BlogPostId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return BlogPostId.GetHashCode();
        }
    }
}
