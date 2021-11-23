using Microsoft.Extensions.DependencyInjection;
using Recrutify.Services.Events;
using Recrutify.Services.Events.Abstract;

namespace Recrutify.Host.Extensions
{
    public static class AddEventHandlers
    {
        public static void AddEvents(this IServiceCollection services)
        {
            services.AddTransient<IStatusChangeEventHandler, StatusChangeEventHandler>();
        }
    }
}
