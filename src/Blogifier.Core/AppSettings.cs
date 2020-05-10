using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Blogifier.Core
{
    public class AppSettings
    {
        public static string Avatar { get; set; }
        public static bool DemoMode { get; set; }     
        public static string ImageExtensions { get; set; }
        public static string ImportTypes { get; set; }
        public static bool SeedData { get; set; }

        public static string SiteRoot { get; set; } = "/";

        public static string WebRootPath { get; set; }
        public static string ContentRootPath { get; set; }

        public static int ThumbWidth { get; set; } = 432;
        public static int ThumbHeight { get; set; } = 200;

        public static Action<DbContextOptionsBuilder> DbOptions { get; set; }

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