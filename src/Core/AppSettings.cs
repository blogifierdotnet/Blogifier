using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Core
{
    public class AppSettings
    {
        [Required]
        [StringLength(160)]
        public static string Title { get; set; } = "Blog title";
        [Required]
        [StringLength(255)]
        public static string Description { get; set; } = "Short blog description";
        [Required]
        [StringLength(120)]
        public static string Theme { get; set; } = "Standard";
        [StringLength(160)]
        public static string Logo { get; set; } = "lib/img/logo-white.png";
        [StringLength(160)]
        public static string Cover { get; set; } = "lib/img/cover.png";
        public static int ItemsPerPage { get; set; } = 10;
        [StringLength(15)]
        public static string PostListType { get; set; } = "description";

        public static string DefaultCover { get { return "lib/img/cover.png"; } }
        public static string WebRootPath { get; set; }
        public static string ContentRootPath { get; set; }
        public static string Version
        {
            get
            {
                return typeof(AppSettings)
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
    }
}