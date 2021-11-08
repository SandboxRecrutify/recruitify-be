using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Recrutify.DataAccess.Models;

namespace Recrutify.Host.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        private static log4net.ILog log;

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            log = log4net.LogManager.GetLogger("log");
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        log.Error($"Error: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Server Error"
                        }.ToString());
                    }
                });
            });
        }
    }
}
