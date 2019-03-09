using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MimeMapping;
using Newtonsoft.Json;

namespace Sibintek.BeerMachine.ErrorHandling
{
    public static class ExceptionMiddlewareExtensions
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                var loggerFactory = (ILoggerFactory) appError.ApplicationServices.GetService(typeof(ILoggerFactory));
                var logger = loggerFactory.CreateLogger("ExceptionHandler");
                
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = KnownMimeTypes.Json;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var output = JsonConvert.SerializeObject(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Exception = contextFeature.Error
                        }, _settings);
                        
                        logger.LogError(contextFeature.Error, contextFeature.Error.Message);

                        await context.Response.WriteAsync(output);
                    }
                });
            });
        }
    }
}