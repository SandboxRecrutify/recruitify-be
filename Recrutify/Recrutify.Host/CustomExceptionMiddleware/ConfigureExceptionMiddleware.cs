using Microsoft.AspNetCore.Builder;
using Recrutify.Host.Exceptions;

namespace Recrutify.Host.CustomExceptionMiddleware
{
    public static class ConfigureExceptionMiddleware
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
