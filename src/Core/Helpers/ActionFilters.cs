using Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Core.Helpers
{
    public class Administrator : ActionFilterAttribute
    {
        DbContextOptions<AppDbContext> _options;

        public Administrator()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            AppSettings.DbOptions(builder);
            _options = builder.Options;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var context = new AppDbContext(_options))
            {
                var user = filterContext.HttpContext.User.Identity.Name;
                var author = context.Authors.SingleOrDefaultAsync(a => a.AppUserName == user).Result;

                if (author == null)
                {
                    filterContext.Result = new UnauthorizedObjectResult("Unauthenticated");
                }
                else
                {
                    if (!author.IsAdmin)
                    {
                        filterContext.Result = new UnauthorizedObjectResult("Unauthorized");
                    }
                }
            }
        }
    }
}