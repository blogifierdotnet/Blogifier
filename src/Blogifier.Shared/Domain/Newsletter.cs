using System;

namespace Blogifier.Shared
{
	public class Newsletter
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public bool Success { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }

		public Post Post { get; set; }
	}
}
