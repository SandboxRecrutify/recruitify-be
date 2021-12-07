using Microsoft.Extensions.DependencyInjection;
using Recrutify.Services.Helpers;
using Recrutify.Services.Helpers.Abstract;

namespace Recrutify.Host.Extensions
{
    public static class HelpersRegistration
    {
        public static void AddHelpers(this IServiceCollection services)
        {
            services.AddSingleton<IStaffHelper, StaffHelper>();
            services.AddSingleton<IScheduleSlotHelper, ScheduleSlotHelper>();
            services.AddSingleton<IStatusHelper, StatusHelper>();
        }
    }
}
