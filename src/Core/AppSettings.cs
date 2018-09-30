using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core
{
    public class AppSettings
    {
        public static string Title { get; set; }
        public static string Description { get; set; }
        public static string Theme { get; set; }

        public static string Logo { get; set; }
        public static string Avatar { get; set; }
        public static string Cover { get; set; }

        public static int ItemsPerPage { get; set; }
        public static bool UseDescInPostList { get; set; }
        public static string DefaultCover { get; set; }

        public static string ImportTypes { get; set; }
        public static string ImageExtensions { get; set; }
        public static bool DemoMode { get; set; }

        public static string WebRootPath { get; set; }
        public static string ContentRootPath { get; set; }

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