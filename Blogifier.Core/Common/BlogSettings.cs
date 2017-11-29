using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Blogifier.Core.Common
{
    public class BlogSettings
    {
        public static string Title { get; set; } = "Blog Title";
        public static string Description { get; set; } = "Short description of the blog";
        public static string Logo { get; set; } = "/embedded/lib/img/logo.png";
        public static string Cover { get; set; } = "/embedded/lib/img/cover.png";
        public static string Theme { get; set; } = "OneFour";
        public static string Head { get; set; } = "";
        public static string Footer { get; set; } = "";

        public static IList<SelectListItem> BlogThemes { get; set; }

        // posts
        public static int ItemsPerPage { get; set; } = 10;
        public static string PostCover { get; set; } = "/embedded/lib/img/cover.png";
        public static string PostFooter { get; set; } = "";

        public static string SupportedStorageFiles { get; set; } = "zip,txt,mp3,mp4,pdf,doc,docx,xls,xlsx,xml";       
    }
}