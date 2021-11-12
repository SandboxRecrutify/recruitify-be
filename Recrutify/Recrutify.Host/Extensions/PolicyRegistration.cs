using Microsoft.Extensions.DependencyInjection;
using Recrutify.DataAccess.Models;
using Recrutify.Host.ProjectAuthorize;

namespace Recrutify.Host.Extensions
{
    public static class PolicyRegistration
    {
        public static void AddPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.AllAccessPolicy, policy => policy.RequireRole(nameof(Role.Admin), nameof(Role.Recruiter), nameof(Role.Mentor), nameof(Role.Manager), nameof(Role.Interviewer)));
                options.AddPolicy(Constants.Policies.FeedbackPolicy, policy => policy.Requirements.Add(new ProjectRolesPolicyRequirement(nameof(Role.Recruiter), nameof(Role.Mentor), nameof(Role.Interviewer))));
                options.AddPolicy(Constants.Policies.AdminPolicy, policy => policy.RequireRole(nameof(Role.Admin)));
            });
        }
    }
}
