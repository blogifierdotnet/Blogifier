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
                            if (cf.CustomKey == Constants.ProfileAvatar)
                                ApplicationSettings.ProfileAvatar = cf.CustomValue;

                            // blog
                            if (cf.CustomKey == Constants.Title)
                                BlogSettings.Title = cf.CustomValue;

                            if (cf.CustomKey == Constants.Description)
                                BlogSettings.Description = cf.CustomValue;

                            if (cf.CustomKey == Constants.ProfileLogo)
                                BlogSettings.Logo = cf.CustomValue;

                            if (cf.CustomKey == Constants.ProfileImage)
                                BlogSettings.Cover = cf.CustomValue;

                            if (cf.CustomKey == Constants.BlogTheme)
                                BlogSettings.Theme = cf.CustomValue;

                            if (cf.CustomKey == Constants.HeadCode)
                                BlogSettings.Head = cf.CustomValue;

                            if (cf.CustomKey == Constants.FooterCode)
                                BlogSettings.Footer = cf.CustomValue;

                            // posts
                            if (cf.CustomKey == Constants.ItemsPerPage)
                                BlogSettings.ItemsPerPage = int.Parse(cf.CustomValue);

                            if (cf.CustomKey == Constants.PostImage)
                                BlogSettings.PostCover = cf.CustomValue;

                            if (cf.CustomKey == Constants.PostCode)
                                BlogSettings.PostFooter = cf.CustomValue;
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