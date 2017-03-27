using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogifier.Core.Data.Models
{
	public class AdminBaseModel
    {
		public Publisher Blog { get; set; }
		public bool BlogExists { get { return Blog != null; }  }
    }

	public class AdminPostsModel : AdminBaseModel
	{
		public IEnumerable<Publication> Publications { get; set; }
	}

	public class AdminProfileModel : AdminBaseModel
	{
		public IList<SelectListItem> AdminThemes { get; set; }
	}

	public class AdminSyndicationModel : AdminBaseModel
	{
		public int PublisherId { get; set; }
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
}
