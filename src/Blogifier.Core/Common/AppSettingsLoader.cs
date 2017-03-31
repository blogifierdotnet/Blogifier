using Microsoft.Extensions.Configuration;
using System.IO;

namespace Blogifier.Core.Common
{
    public class AppSettingsLoader
    {
        public void LoadFromConfigFile()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            try
            {
                var config = builder.Build();
                if (config != null)
                {
                    var section = config.GetSection("Blogifier");
                    if (section != null)
                    {
                        if (section["UseInMemoryDatabase"] != null)
                            ApplicationSettings.UseInMemoryDatabase = section.GetValue<bool>("UseInMemoryDatabase");

                        if (section["ConnectionString"] != null)
                            ApplicationSettings.ConnectionString = section.GetValue<string>("ConnectionString");

                        if (section["AddContentTypeHeaders"] != null)
                            ApplicationSettings.AddContentTypeHeaders = section.GetValue<bool>("AddContentTypeHeaders");

                        if (section["AddContentLengthHeaders"] != null)
                            ApplicationSettings.AddContentLengthHeaders = section.GetValue<bool>("AddContentLengthHeaders");

                        if (section["PrependFileProvider"] != null)
                            ApplicationSettings.PrependFileProvider = section.GetValue<bool>("PrependFileProvider");
                    }
                }
            }
            catch { }
        }
    }
}
