using Microsoft.AspNetCore.Authorization;
using Recrutify.Host.Infrastructure.Authorization;

namespace Recrutify.Host.Extensions
{
    public static class PolicyRegistration
    {
        public static void RequireProjectRole(this AuthorizationPolicyBuilder policy, params string[] roles)
        {
            policy.Requirements.Add(new RolesPolicyRequirement(roles));
        }
    }
}
