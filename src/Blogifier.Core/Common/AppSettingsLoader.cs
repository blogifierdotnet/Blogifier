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
                        if (section["AddContentTypeHeaders"] != null)
                            ApplicationSettings.AddContentTypeHeaders = section.GetValue<bool>("AddContentTypeHeaders");
                    }
                }
            }
            catch { }
        }
    }
}
