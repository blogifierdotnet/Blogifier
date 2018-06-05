using Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Core
{
    public static class AppConfig
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            try
            {
                foreach (string dll in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        var assembly = Assembly.LoadFile(dll);
                        var product = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;

                        if (product.StartsWith("Blogifier."))
                        {
                            assemblies.Add(assembly);
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return assemblies;
        }

        public static void SetSettings(AppItem app)
        {
            AppSettings.Title = app.Title;
            AppSettings.Description = app.Description;
            AppSettings.Logo = app.Logo;
            AppSettings.Cover = app.Cover;
            AppSettings.Theme = app.Theme;
            AppSettings.PostListType = app.PostListType;
            AppSettings.ItemsPerPage = app.ItemsPerPage;
        }
    }
}