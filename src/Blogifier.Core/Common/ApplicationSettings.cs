using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Reflection;

namespace Blogifier.Core.Common
{
    public class ApplicationSettings
    {
        #region Application settings

        // default value is "blog/" for blogifier to use "site.com/blog"
        // if empty string, blog takes over the application
        // so instead of "site.com/blog" blogifier will be using "site.com" 
        public static string BlogRoute { get; set; } = "blog/";

        public static bool SingleBlog { get; set; } = false;
        public static bool EnableLogging { get; set; }
        public static bool UseInMemoryDatabase { get; set; }
        public static bool InitializeDatabase { get; set; } = true;
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

        #region database

        // this is not set directly from the appsettings.json file. Instead, this is passed from the host appplication to configure the appropriate database
        public static System.Action<DbContextOptionsBuilder> DatabaseOptions { get; set; } = options =>
        {
            var memoryExtension = options.Options.FindExtension<InMemoryOptionsExtension>();
            if (memoryExtension != null && !string.IsNullOrWhiteSpace(memoryExtension.StoreName))
            {
                options.UseInMemoryDatabase(memoryExtension.StoreName);
            }
            else
            {
                options.UseInMemoryDatabase(Constants.Blogifier);
            }
        };

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
