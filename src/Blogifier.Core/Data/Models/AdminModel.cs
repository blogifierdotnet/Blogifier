using System.Collections.Generic;
using Blogifier.Core.Data.Domain;

namespace Blogifier.Core.Data.Models
{
	public class AdminBaseModel
    {
		public bool BlogExists { get; set; }
    }

	public class AdminPostsModel : AdminBaseModel
	{
		public IEnumerable<Publication> Publications { get; set; }
	}
}
