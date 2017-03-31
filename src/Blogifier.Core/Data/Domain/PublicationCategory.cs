namespace Blogifier.Core.Data.Domain
{
    public class BlogPostCategory : BaseEntity
    {
        public int BlogPostId { get; set; }
        public BlogPost BlogPosts { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}