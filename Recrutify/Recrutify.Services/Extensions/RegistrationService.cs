using Microsoft.Extensions.DependencyInjection;
using Recrutify.Services.Services;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Extensions
{
    public static class RegistrationService
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<ICandidateService, CandidateService>();
            services.AddSingleton<IPrimarySkillService, PrimarySkillService>();
        }
    }
}
