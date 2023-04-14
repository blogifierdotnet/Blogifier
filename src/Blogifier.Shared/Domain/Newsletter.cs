using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared
{
	public class Newsletter
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public bool Success { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateUpdated { get; set; }
        public Post Post { get; set; }
	}
}
