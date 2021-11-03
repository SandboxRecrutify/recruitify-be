using Microsoft.AspNetCore.Builder;
using Recrutify.Host.CustomExceptionMiddleware;

namespace Recrutify.Host.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
