using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Recrutify.DataAccess.Repositories;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Host.ProjectAuthorize;

namespace Recrutify.Services.Extensions
{
    public static class RepositoriesRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, CustomPolicyHandler>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<ICandidateRepository, CandidateRepository>();
            services.AddSingleton<IPrimarySkillRepository, PrimarySkillRepository>();
        }
    }
}
