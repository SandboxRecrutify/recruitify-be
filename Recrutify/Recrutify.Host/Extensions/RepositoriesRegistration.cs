using Microsoft.Extensions.DependencyInjection;
using Recrutify.DataAccess.Repositories;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.Host.Extensions
{
    public static class RepositoriesRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<ICandidateRepository, CandidateRepository>();
            services.AddSingleton<IPrimarySkillRepository, PrimarySkillRepository>();
        }
    }
}
