using Microsoft.Extensions.DependencyInjection;
using Recrutify.Host.Providers;
using Recrutify.Services.Providers;
using Recrutify.Services.Services;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Extensions
{
    public static class ServicesRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISendEmailService, SendEmailService>();
            services.AddTransient<ISendQueueEmailService, SendQueueEmailService>();
            services.AddTransient<IFormEmailService, FormEmailService>();
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<ICandidateService, CandidateService>();
            services.AddSingleton<IPrimarySkillService, PrimarySkillService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserProvider, UserProvider>();
        }
    }
}