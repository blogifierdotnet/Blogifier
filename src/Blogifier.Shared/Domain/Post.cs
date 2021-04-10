using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared
{
	public class Post
	{
		public Post() { }

		public int Id { get; set; }
		public int AuthorId { get; set; }

		public PostType PostType { get; set; }

		[Required]
		[StringLength(160)]
		public string Title { get; set; }
		[Required]
		[StringLength(160)]
		public string Slug { get; set; }
		[Required]
		[StringLength(450)]
		public string Description { get; set; }
		[Required]
		public string Content { get; set; }
		[StringLength(160)]
		public string Cover { get; set; }
		public int PostViews { get; set; }
		public double Rating { get; set; }
		public bool IsFeatured { get; set; }
		public bool Selected { get; set; }

		public DateTime Published { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }

		public Blog Blog { get; set; }
        public List<PostCategory> PostCategories { get; set; }
    }
}
