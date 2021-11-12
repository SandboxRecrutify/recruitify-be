using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Recrutify.Host.Infrastructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly log4net.ILog log;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            log = log4net.LogManager.GetLogger("log");
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                log.Error($"Error: {ex}");
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
