using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Http;

namespace Blogifier.Core.Middleware
{
	public class AdminResources
	{
		readonly RequestDelegate _next;
		List<string> _resources;
		Assembly _assembly;

		public AdminResources(RequestDelegate next)
		{
			_next = next;
			_resources = new List<string>();
			_assembly = typeof(AdminResources).GetTypeInfo().Assembly;

			// var xxx = _assembly.GetManifestResourceNames();

			foreach (var name in _assembly.GetManifestResourceNames())
			{
				// System.Diagnostics.Debug.WriteLine("RESOURCE: " + name);
				if (name.Contains("Blogifier.Content") && Include(name))
				{
					_resources.Add(name);
				}
			}
		}

		public async Task Invoke(HttpContext context)
		{
			var path = context.Request.Path.ToString().ToLower().Replace("/", ".");

			// System.Diagnostics.Debug.WriteLine("PATH: " + path);

			if (path.Contains(".blogifier.", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					var resource = _resources.Where(r => r.Contains(path, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
					if (!string.IsNullOrEmpty(resource))
					{
						if (resource.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
						{
							//context.Response.ContentType = "text/css";
							context.Response.Headers.Remove("Content-Type");
							context.Response.Headers.Add("Content-Type", "text/css");
						}

						if (resource.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
						{
							//context.Response.ContentType = "application/javascript";
							context.Response.Headers.Remove("Content-Type");
							context.Response.Headers.Add("Content-Type", "application/javascript");
						}

						if (resource.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
							context.Response.ContentType = "image/jpeg";

						if (resource.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
						{
							//context.Response.ContentType = "image/png";
							context.Response.Headers.Remove("Content-Type");
							context.Response.Headers.Add("Content-Type", "image/png");
						}

						var stream = _assembly.GetManifestResourceStream(resource);

						context.Response.Headers.Add("Content-Length", stream.Length.ToString());
						//context.Response.ContentLength = stream.Length;

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
				resource.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase)
				)
				return true;

			return false;
		}
	}
}
