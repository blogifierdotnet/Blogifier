using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Blogifier.Core.Data.Models
{
    public class BlogPostDetailModel
    {
        public Profile Profile { get; set; }
        public BlogPost BlogPost { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }

    public class BlogPostsModel
    {
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }

    public class BlogCategoryModel
    {
        public Category Category { get; set; }
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }

    public class BlogAuthorModel
    {
        public Profile Profile { get; set; }
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }
}
