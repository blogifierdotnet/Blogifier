using System;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared
{
	public class Subscriber
	{
		public Subscriber() { }

		public int Id { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(160)]
		public string Email { get; set; }
		[StringLength(80)]
		public string Ip { get; set; }
		[StringLength(120)]
		public string Country { get; set; }
		[StringLength(120)]
		public string Region { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }

		public Blog Blog { get; set; }
	}
}
