using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Recrutify.Host.Infrastructure;
using Recrutify.Services.Events;

namespace Recrutify.Host.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseHttpStatusExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void UseStatusChangeEventProcessor(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var statusChangeEventProcessor = serviceProvider.GetService<StatusChangeEventProcessor>();
            statusChangeEventProcessor.Subscribe();
        }

        public static void UseInviteEventProcessor(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var inviteEventProcessor = serviceProvider.GetService<InviteEventProcessor>();
            inviteEventProcessor.Subscribe();
        }
    }
}
