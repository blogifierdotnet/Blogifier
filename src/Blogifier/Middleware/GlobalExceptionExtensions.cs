using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Blogifier.Shared.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace Blogifier.Middleware
{
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static void UseConfiguredExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(errorOptions =>
            {
                errorOptions.Run(async context =>
                {
                    try
                    {
                        context.Response.ContentType = "application/json";

                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                        if (exceptionHandlerFeature != null)
                        {
                            Log.Information("Error reason: {@message} route: {@route}", exceptionHandlerFeature.Error.Message, context?.Request?.Path.Value);

                            context.Response.StatusCode = exceptionHandlerFeature.Error switch
                            {
                                NotFoundExсeption => (int)HttpStatusCode.NotFound,
                                NotAcceptableException => (int)HttpStatusCode.NotAcceptable,
                                BusinessLogicException => (int)HttpStatusCode.InternalServerError,
                                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                                SpecifyHttpStatusCodeException => (int)((SpecifyHttpStatusCodeException)exceptionHandlerFeature.Error).HttpStatusCode,
                                _ => (int)HttpStatusCode.InternalServerError
                            };

                            await context.Response.WriteAsync(
                                JsonConvert.SerializeObject(
                                    new
                                    {
                                        StatusCode = context.Response.StatusCode,
                                        Message = exceptionHandlerFeature.Error.Message
                                    }
                                ));
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Fatal(ex, "Fatal error in the GlobalExceptionMiddlewareExtensions WriteResponse");
                    }

                });
            });
        }
    }
}
