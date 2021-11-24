using Microsoft.Extensions.DependencyInjection;
using Recrutify.Services.Events;
using Recrutify.Services.Events.Abstract;

namespace Recrutify.Host.Extensions
{
    public static class EventExtensions
    {
        public static void AddEventHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IUpdateStatusEvent, UpdateStatusEvent>();
            services.AddSingleton<StatusChangeEventProcessor>();
        }
    }
}
