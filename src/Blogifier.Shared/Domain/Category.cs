using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared
{
	public class Category
	{
		public Category()	{ }

		public int Id { get; set; }
		[Required]
		[StringLength(120)]
		public string Content { get; set; }

		public DateTime DateCreated { get; set; }

		// TOTO The problem is not utc time
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime DateUpdated { get; set; }

		public virtual ICollection<Post> Posts { get; set; }
	}
}
