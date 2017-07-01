using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Models
{
    public class AdminBaseModel
    {
		public Profile Profile { get; set; }
		public bool BlogExists { get { return Profile != null; }  }
    }

    public class AdminPostsModel : AdminBaseModel
    {
        public Pager Pager { get; set; }
        public IEnumerable<PostListItem> BlogPosts { get; set; }
    }

    public class AdminEditorModel : AdminBaseModel
    {
        public BlogPost  BlogPost { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }

    public class AdminProfileModel : AdminBaseModel
	{
		public IList<SelectListItem> BlogThemes { get; set; }
    }

	public class AdminToolsModel : AdminBaseModel
	{
		public RssImportModel RssImportModel { get; set; }
        public CustomField DisqusModel { get; set; }
	}

    public class RssImportModel
    {
        public int ProfileId { get; set; }
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

    public class AdminPostList
    {
        public Pager Pager { get; set; }
        public IEnumerable<PostListItem> BlogPosts { get; set; }
    }

    public class AdminAssetList
    {
        public Pager Pager { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
    }

    public class PostEditModel
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public bool IsPublished { get; set; }
        public DateTime Published { get; set; }
    }

    public class CategoryItem
    {
        public string Id { get; set; }
        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        [StringLength(450)]
        public string Description { get; set; }
        public string Slug { get; set; }
        public int ViewCount { get; set; }
        public int PostCount { get; set; }
        public bool Selected { get; set; }
    }

    public class AdminCategoryModel
    {
        public string Title { get; set; }
        public string PostId { get; set; }
        public string CategoryId { get; set; }
    }
}
