namespace Blogifier.Shared
{
	public class CustomField
	{
		public int Id { get; set; }
		public int AuthorId { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }
	}

	public class SocialField : CustomField
	{
		public string Title { get; set; }
		public string Icon { get; set; }
		public int Rank { get; set; }
	}
}
