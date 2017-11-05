using Blogifier.Core.Common;
using Blogifier.Core.Data;
using Blogifier.Core.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Middleware
{
    public class AppSettingsLoader
    {
        private readonly RequestDelegate _next;
        private static bool _loaded;

        public AppSettingsLoader(RequestDelegate next, IConfiguration config)
        {
            if (!_loaded)
            {
                LoadFromConfigFile(config);

                var builder = new DbContextOptionsBuilder<BlogifierDbContext>();
                ApplicationSettings.DatabaseOptions(builder);

                using (var db = new BlogifierDbContext(builder.Options))
                {
                    var fields = db.CustomFields.Where(c => c.ParentId == 0);
                    if(fields != null && fields.Count() > 0)
                    {
                        foreach (var cf in fields)
                        {
                            if (cf.CustomKey == "Title")
                                ApplicationSettings.Title = cf.CustomValue;

                            if (cf.CustomKey == "Description")
                                ApplicationSettings.Description = cf.CustomValue;

                            if (cf.CustomKey == "ItemsPerPage")
                                ApplicationSettings.ItemsPerPage = int.Parse(cf.CustomValue);


                            if (cf.CustomKey == "ProfileLogo")
                                ApplicationSettings.ProfileLogo = cf.CustomValue;

                            if (cf.CustomKey == "ProfileAvatar")
                                ApplicationSettings.ProfileAvatar = cf.CustomValue;

                            if (cf.CustomKey == "ProfileImage")
                                ApplicationSettings.ProfileImage = cf.CustomValue;

                            if (cf.CustomKey == "PostImage")
                                ApplicationSettings.PostImage = cf.CustomValue;


                            if (cf.CustomKey == "BlogTheme")
                                ApplicationSettings.BlogTheme = cf.CustomValue;
                        }
                    }
                }
                _loaded = true;
            }
            
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // we only need this middleware run on app start
            // nothing to do in the invoke, pass it though
            await _next(httpContext);
        }

        void LoadFromConfigFile(IConfiguration config)
        {
            try
            {
                if (config != null)
                {
                    if (!string.IsNullOrEmpty(config.GetConnectionString("DefaultConnection")))
                        ApplicationSettings.ConnectionString = config.GetConnectionString("DefaultConnection");

                    var section = config.GetSection("Blogifier");
                    if (section != null)
                    {
                        // system settings

                        if (section["BlogRoute"] != null)
                            ApplicationSettings.BlogRoute = section.GetValue<string>("BlogRoute");

                        if (section["SingleBlog"] != null)
                            ApplicationSettings.SingleBlog = section.GetValue<bool>("SingleBlog");

                        if (section["UseInMemoryDatabase"] != null)
                            ApplicationSettings.UseInMemoryDatabase = section.GetValue<bool>("UseInMemoryDatabase");

                        if (section["ConnectionString"] != null)
                            ApplicationSettings.ConnectionString = section.GetValue<string>("ConnectionString");

                        if (section["EnableLogging"] != null)
                            ApplicationSettings.EnableLogging = section.GetValue<bool>("EnableLogging");

                        if (section["BlogStorageFolder"] != null)
                            ApplicationSettings.BlogStorageFolder = section.GetValue<string>("BlogStorageFolder");

                        if (section["SupportedStorageFiles"] != null)
                            ApplicationSettings.SupportedStorageFiles = section.GetValue<string>("SupportedStorageFiles");

                        if (section["AdminTheme"] != null)
                            ApplicationSettings.AdminTheme = section.GetValue<string>("AdminTheme");

                        if (section["BlogTheme"] != null)
                            ApplicationSettings.BlogTheme = section.GetValue<string>("BlogTheme");

                        // applicatin settings

                        if (section["Title"] != null)
                            ApplicationSettings.Title = section.GetValue<string>("Title");

                        if (section["Description"] != null)
                            ApplicationSettings.Description = section.GetValue<string>("Description");

                        if (section["ItemsPerPage"] != null)
                            ApplicationSettings.ItemsPerPage = section.GetValue<int>("ItemsPerPage");


                        if (section["ProfileAvatar"] != null)
                            ApplicationSettings.ProfileAvatar = section.GetValue<string>("ProfileAvatar");

                        if (section["ProfileLogo"] != null)
                            ApplicationSettings.ProfileLogo = section.GetValue<string>("ProfileLogo");

                        if (section["ProfileImage"] != null)
                            ApplicationSettings.ProfileImage = section.GetValue<string>("ProfileImage");

                        if (section["PostImage"] != null)
                            ApplicationSettings.PostImage = section.GetValue<string>("PostImage");

                        // troubleshooting

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