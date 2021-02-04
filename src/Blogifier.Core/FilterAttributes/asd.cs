using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blogifier.Core.FilterAttributes
{
	public class RestrictToLocalhostAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
			if (!IPAddress.IsLoopback(remoteIp)) {
				context.Result = new UnauthorizedResult();
				return;
			}
			base.OnActionExecuting(context);
		}
	}
}