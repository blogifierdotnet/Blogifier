using Blogifier.Core.Common;
using Blogifier.Core.Data;
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
    }
}