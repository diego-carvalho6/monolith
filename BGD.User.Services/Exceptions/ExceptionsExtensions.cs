using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BGD.User.Services.Exceptions
{
    public static  class ExceptionsHandlerExtensions
    {

            public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                            logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");

                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            context.Response.ContentType = "application/json";

                            var json = new
                            {
                                context.Response.StatusCode,
                                exceptionHandlerFeature.Error.Message,
                                Detailed = exceptionHandlerFeature.Error.Source
                            };

                            await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
                        }
                    });
                });
            }
        }
}
