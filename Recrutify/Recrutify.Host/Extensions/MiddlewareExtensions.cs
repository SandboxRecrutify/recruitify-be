using Microsoft.AspNetCore.Builder;
using Recrutify.Host.Infrastructure;

namespace Recrutify.Host.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseHttpStatusExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
