using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Blogifier.Core.Common
{
    public class PostListItem
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

        public ICollection<SelectListItem> Categories { get; set; }
    }
}
