using Core.Data;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Core
{
    public class AppSettings
    {
        public static void Load(BlogItem item)
        {
            Title = item.Title;
            Description = item.Description;
            Logo = item.Logo;
            Cover = item.Cover;
            Theme = item.Theme;
            PostListType = item.PostListType;
            ItemsPerPage = item.ItemsPerPage;
        }

        [Required]
        [StringLength(160)]
        public static string Title { get; set; }
        [Required]
        [StringLength(255)]
        public static string Description { get; set; }
        [Required]
        [StringLength(120)]
        public static string Theme { get; set; }
        [StringLength(160)]
        public static string Logo { get; set; }
        [StringLength(160)]
        public static string Cover { get; set; }
        public static int ItemsPerPage { get; set; }
        [StringLength(15)]
        public static string PostListType { get; set; }
        public static string DefaultCover { get; set; }

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