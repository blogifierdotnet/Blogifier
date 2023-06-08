using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Shared
{
	public class ImportMessage
   {
      public Status Status { get; set; }
      public string Message { get; set; }
   }

   public class ImportModel
	{
      [Required]
      [Url]
      public string FeedUrl { get; set; }
      [Required]
      [Url]
      public string BaseUrl { get; set; }
      List<string> FileExtensions { get; set; } = new List<string>() { "zip", "7z", "xml", "pdf", "doc", "docx", "xls", "xlsx", "mp3", "mp4", "avi" };
   }
}
