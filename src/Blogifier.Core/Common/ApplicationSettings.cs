using System.Reflection;

namespace Blogifier.Core.Common
{
    public class ApplicationSettings
    {
        #region Application settings

        public static bool SingleBlog { get; set; } = false;
        public static bool EnableLogging { get; set; }
        public static bool UseInMemoryDatabase { get; set; }
        public static string ConnectionString { get; set; } = @"Server=.\SQLEXPRESS;Database=Blogifier;Trusted_Connection=True;";
        public static string BlogStorageFolder { get; set; } = "blogifier/data";
        public static string SupportedStorageFiles { get; set; } = "zip,txt,mp3,mp4,pdf,doc,docx,xls,xlsx,xml";
        
        public static string Title { get; set; } = "Blog Name";
        public static string Description { get; set; } = "Short description of the blog";
        public static int ItemsPerPage { get; set; } = 10;

        public static string AdminTheme { get; set; } = "Standard";
        public static string BlogTheme { get; set; } = "Standard";
        public static string ProfileAvatar { get; set; } = "/embedded/lib/img/avatar.jpg";
        public static string ProfileLogo { get; set; } = "/embedded/lib/img/logo.png";
        public static string ProfileImage { get; set; } = "/embedded/lib/img/cover.png";
        public static string PostImage { get; set; } = "/embedded/lib/img/cover.png";

        #endregion

        #region Troubleshooting

        public static bool AddContentTypeHeaders { get; set; } = true;
        public static bool AddContentLengthHeaders { get; set; }
        public static bool PrependFileProvider { get; set; }

        #endregion

        #region System only overwritable and read-only settings

        public static string WebRootPath { get; set; }
        public static string ContentRootPath { get; set; }
        public static string Version
		{
			get
			{
				return typeof(ApplicationSettings)
					.GetTypeInfo()
					.Assembly
					.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
					.InformationalVersion;
			}
		}
		public static string OSDescription
		{
			get
			{
				return System.Runtime.InteropServices.RuntimeInformation.OSDescription;
			}
		}

        #endregion
    }
}
