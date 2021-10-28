using Microsoft.Extensions.DependencyInjection;
using Recrutify.DataAccess.Repositories;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.Services.Extensions
{
    public static class RegistrationRepositorie
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<ICandidateRepository, CandidateRepository>();
            services.AddSingleton<IPrimarySkillRepository, PrimarySkillRepository>();
        }
    }
}
