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

                        // if (Common.ApplicationSettings.OSDescription.Contains("Linux", StringComparison.OrdinalIgnoreCase))
                        if (ApplicationSettings.AddContentTypeHeaders)
						{
							context.Response.Headers.Add("Content-Length", stream.Length.ToString());
							context.Response.Headers.Remove("Content-Type");

							if (resource.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
							{
								context.Response.Headers.Add("Content-Type", "text/css");
							}
							if (resource.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
							{
								context.Response.Headers.Add("Content-Type", "application/javascript");
							}
							if (resource.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
							{
								context.Response.Headers.Add("Content-Type", "image/jpeg");
							}
							if (resource.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
							{
								context.Response.Headers.Add("Content-Type", "image/png");
							}
						}
						await stream.CopyToAsync(context.Response.Body);
					}
				}
				catch(Exception ex)
				{
					throw new Exception("Exception invoking embedded resources: " + ex.Message);
				}
			}
			await _next.Invoke(context);
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
