using Microsoft.AspNetCore.Builder;
using Recrutify.Host.CustomExceptionMiddleware;

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
