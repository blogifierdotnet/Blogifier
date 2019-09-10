using Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Core
{
    public static class AppConfig
    {
        public static IEnumerable<Assembly> GetAssemblies(bool includeApp = false)
        {
            var assemblies = new List<Assembly>();
            try
            {
                foreach (string dll in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        var assembly = Assembly.LoadFile(dll);

                        if ((dll.Contains("App.dll")) && includeApp)
                        {
                            assemblies.Add(assembly);
                            continue;
                        }

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
            AppSettings.Avatar = app.Avatar;
            AppSettings.DemoMode = app.DemoMode;
            AppSettings.ImageExtensions = app.ImageExtensions;
            AppSettings.ImportTypes = app.ImportTypes;
            AppSettings.SeedData = app.SeedData;
        }
    }
}