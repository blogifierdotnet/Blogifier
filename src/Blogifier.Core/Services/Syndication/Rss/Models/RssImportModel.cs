using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Services.Syndication.Rss
{
    public class RssImportModel
    {
        public int PublisherId { get; set; }
        [Required]
        [StringLength(450)]
        public string FeedUrl { get; set; }
        [StringLength(150)]
        public string Domain { get; set; }
        [StringLength(150)]
        public string SubDomain { get; set; }

        public bool ImportImages { get; set; }
        public bool ImportAttachements { get; set; }
    }
}
