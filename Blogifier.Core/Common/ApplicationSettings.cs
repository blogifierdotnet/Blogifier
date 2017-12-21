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

        public static bool EnableLogging { get; set; }
        public static bool UseInMemoryDatabase { get; set; }
        public static bool InitializeDatabase { get; set; } = true;
        public static string ConnectionString { get; set; } = "DataSource=Data\\app.db";

        public static string BlogStorageFolder { get; set; } = "blogifier/data";
        public static string BlogAdminFolder { get; set; } = "Views/Embedded/Admin";
        public static string BlogThemesFolder { get; set; } = "Views/Blogifier/Themes";

        public static string ProfileAvatar { get; set; } = "/embedded/lib/img/avatar.jpg";

        public static string PkgSettingsLayout { get; set; } = "~/Views/Blogifier/Admin/_Layout/_Packages.cshtml";
        public static string SupportedStorageFiles { get; set; } = "zip,txt,mp3,mp4,pdf,doc,docx,xls,xlsx,xml";

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
