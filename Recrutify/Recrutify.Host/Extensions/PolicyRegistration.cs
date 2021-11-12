using Microsoft.AspNetCore.Authorization;
using Recrutify.Host.ProjectAuthorize;

namespace Recrutify.Host.Extensions
{
    public static class PolicyRegistration
    {
        public static void RequireProjectRole(this AuthorizationPolicyBuilder policy, params string[] roles)
        {
            policy.Requirements.Add(new ProjectRolesPolicyRequirement(roles));
        }
    }
}
