using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Blogifier.Core.Common;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Middleware
{
	public class EmbeddedResources
	{
		readonly RequestDelegate _next;
        readonly ILogger _logger;
        Dictionary<string, CachedResource> _resources;

		public EmbeddedResources(RequestDelegate next, ILogger<EmbeddedResources> logger)
		{
			_next = next;
            _logger = logger;
            _resources = new Dictionary<string, CachedResource>();
			var assembly = typeof(EmbeddedResources).GetTypeInfo().Assembly;

			foreach (var name in assembly.GetManifestResourceNames())
			{
				if (name.Contains("Blogifier.Core.Embedded") && Include(name))
				{
                    var path = name.ReplaceIgnoreCase("Blogifier.Core", "").ToLower();
                    var resource = GetResource(name, assembly);
					_resources.Add(path, resource);
                    _logger.LogInformation("EMBEDDED: " + path);
				}
			}
		}

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower().Replace("/", ".");

            if (path.Contains(".embedded.", StringComparison.OrdinalIgnoreCase))
            {
                var resource = _resources[path];
                Stream stream = new MemoryStream(resource.Content);

                SetContextHeaders(context, resource.ContentType, stream.Length);

                await stream.CopyToAsync(context.Response.Body);
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        void SetContextHeaders(HttpContext context, string resType, long length)
        {
            // marker to identify embedded resource for troublshooting
            context.Response.Headers.Add("Embedded-Content", "true");

            if (ApplicationSettings.AddContentTypeHeaders)
                context.Response.ContentType = resType;

            if (ApplicationSettings.AddContentLengthHeaders)
                context.Response.ContentLength = length;
        }

        CachedResource GetResource(string path, Assembly assembly)
        {
            var stream = assembly.GetManifestResourceStream(path);

            var resource = new CachedResource();
            resource.ContentType = GetContentType(path);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                resource.Content = memoryStream.ToArray();
            }
            return resource;
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

    public class CachedResource
    {
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
