﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		[RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Slug format not valid.")]
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

		// TOTO The problem is not utc time
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime DateUpdated { get; set; }

		public Blog Blog { get; set; }
		public virtual ICollection<Category> Categories { get; set; }
	}
}
