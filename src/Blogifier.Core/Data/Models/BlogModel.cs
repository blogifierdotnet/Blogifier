using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Blogifier.Core.Data.Models
{
    public class BlogBaseModel
    {
        public List<SelectListItem> Categories { get; set; }
    }

    public class BlogPostDetailModel : BlogBaseModel
    {
        public Profile Profile { get; set; }
        public BlogPost BlogPost { get; set; }
        public List<SelectListItem> BlogCategories { get; set; }
        public CustomField DisqusScript { get; set; }
    }

    public class BlogPostsModel : BlogBaseModel
    {
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }

    public class BlogCategoryModel : BlogBaseModel
    {
        public Category Category { get; set; }
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }

    public class BlogAuthorModel : BlogBaseModel
    {
        public Profile Profile { get; set; }
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }
}
