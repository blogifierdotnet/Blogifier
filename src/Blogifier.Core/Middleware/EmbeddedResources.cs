using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Blogifier.Core.Common;

namespace Blogifier.Core.Middleware
{
	public class EmbeddedResources
	{
		readonly RequestDelegate _next;
		List<string> _resources;
		Assembly _assembly;

		public EmbeddedResources(RequestDelegate next)
		{
			_next = next;
			_resources = new List<string>();
			_assembly = typeof(EmbeddedResources).GetTypeInfo().Assembly;

			foreach (var name in _assembly.GetManifestResourceNames())
			{
				if (name.Contains("Blogifier.Core.Embedded") && Include(name))
				{
					_resources.Add(name);
				}
			}
		}

		public async Task Invoke(HttpContext context)
		{
			var path = context.Request.Path.ToString().ToLower().Replace("/", ".");

			if (path.Contains(".embedded.", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					var resource = _resources.Where(r => r.Contains(path, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

					if (!string.IsNullOrEmpty(resource))
					{
						var stream = _assembly.GetManifestResourceStream(resource);

                        if (ApplicationSettings.AddContentTypeHeaders)
						{
                            context.Response.Headers.Add("Content-Length", stream.Length.ToString());
                            context.Response.Headers.Add("Embedded-Content", "true");

                            var contentType = GetContentType(resource);

                            if (!string.IsNullOrEmpty(contentType))
                            {
                                context.Response.Headers.Remove("Content-Type");
                                context.Response.Headers.Add("Content-Type", contentType);
                            }
						}

						await stream.CopyToAsync(context.Response.Body);
					}
				}
                catch { }
			}
			await _next.Invoke(context);
		}

        string GetContentType(string url)
        {
            if (url.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
            {
                return "text/css";
            }
            if (url.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
            {
                return "application/javascript";
            }
            if (url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            {
                return "image/jpeg";
            }
            if (url.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                return "image/png";
            }
            return "";
        }

		bool Include(string resource)
		{
			if (resource.EndsWith(".css", StringComparison.OrdinalIgnoreCase) ||
				resource.EndsWith(".js", StringComparison.OrdinalIgnoreCase) ||
				resource.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
				resource.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
				resource.EndsWith(".woff", StringComparison.OrdinalIgnoreCase) ||
                resource.EndsWith(".woff2", StringComparison.OrdinalIgnoreCase) ||
                resource.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase)
				)
				return true;

			return false;
		}
	}
}
