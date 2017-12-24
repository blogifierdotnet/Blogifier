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
    /// redirected to setup page that must be completed
    /// </summary>
    public class VerifyProfile : ActionFilterAttribute
    {
        DbContextOptions<BlogifierDbContext> _options;

        public VerifyProfile()
        {
            var builder = new DbContextOptionsBuilder<BlogifierDbContext>();

            ApplicationSettings.DatabaseOptions(builder);

            _options = builder.Options;
        }

        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var context = new BlogifierDbContext(_options))
            {
                var user = filterContext.HttpContext.User.Identity.Name;
                if (await context.Profiles.SingleOrDefaultAsync(p => p.IdentityName == user) == null)
                {
                    filterContext.Result = new RedirectResult("~/admin/setup");
                }
            }
        }
    }

    public class MustBeAdmin : ActionFilterAttribute
    {
        DbContextOptions<BlogifierDbContext> _options;

        public MustBeAdmin()
        {
            var builder = new DbContextOptionsBuilder<BlogifierDbContext>();

            ApplicationSettings.DatabaseOptions(builder);

            _options = builder.Options;
        }

        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var context = new BlogifierDbContext(_options))
            {
                var loggedUser = filterContext.HttpContext.User.Identity.Name;
                var profile = await context.Profiles.SingleOrDefaultAsync(p => p.IdentityName == loggedUser);

                if(profile == null || !profile.IsAdmin)
                {
                    filterContext.Result = new RedirectResult("~/Error/403");
                }
            }
        }
    }
}