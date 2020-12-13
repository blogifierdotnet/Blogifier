using System;

namespace Blogifier.Shared
{
	public class Newsletter
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public int SentCount { get; set; }
		public int FailCount { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
	}
}
