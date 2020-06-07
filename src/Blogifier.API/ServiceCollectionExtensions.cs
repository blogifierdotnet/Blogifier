using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Blogifier.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Blogifier API",
                    Version = "v1"
                });
                setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CoreAPI.xml"));
            });
            return services;
        }
    }
}
