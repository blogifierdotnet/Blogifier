namespace Blogifier.Core.Data.Domain
{
    public class PublicationCategory : BaseEntity
    {
        public int PublicationId { get; set; }
        public Publication Publications { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}