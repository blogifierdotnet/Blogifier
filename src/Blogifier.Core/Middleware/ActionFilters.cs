using Blogifier.Core.Common;
using Blogifier.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Middleware
{
    /// <summary>
    /// There are two-level authorization for admin panel
    /// 1. [Authorize] - Verify that user is authenticated
    /// 2. [VerifyProfile] - Verify that user has profile
    /// If user does not have profile - any admin actions
    /// redirected to profile page that must be completed
    /// </summary>
    public class VerifyProfile : ActionFilterAttribute
    {
        DbContextOptions<BlogifierDbContext> _options;

        public VerifyProfile()
        {
            var builder = new DbContextOptionsBuilder<BlogifierDbContext>();

            if (ApplicationSettings.UseInMemoryDatabase)
                builder.UseInMemoryDatabase();
            else
                builder.UseSqlServer(ApplicationSettings.ConnectionString);

            _options = builder.Options;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var context = new BlogifierDbContext(_options))
            {
                var user = filterContext.HttpContext.User.Identity.Name;
                if (context.Profiles.SingleOrDefaultAsync(p => p.IdentityName == user).Result == null)
                {
                    filterContext.Result = new RedirectResult("/admin/settings/profile");
                }
            }
        }
    }
}