namespace Blogifier.Core.Data.Domain
{
    public class PostCategory : BaseEntity
    {
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}