using Microsoft.AspNetCore.Authorization;

namespace Recrutify.Host.Infrastructure.Authorization
{
    public class RolesPolicyRequirement : IAuthorizationRequirement
    {
        public RolesPolicyRequirement(params string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; set; }
    }
}
