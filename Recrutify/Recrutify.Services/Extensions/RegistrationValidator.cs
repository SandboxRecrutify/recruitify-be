using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Recrutify.Services.DTOs;
using Recrutify.Services.Validators;

namespace Recrutify.Services.Extensions
{
    public static class RegistrationValidator
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<CreateProjectDTO>, CreateProjectValidator>();
            services.AddSingleton<IValidator<ProjectDTO>, UpdateProjectValidator>();
        }
    }
}
