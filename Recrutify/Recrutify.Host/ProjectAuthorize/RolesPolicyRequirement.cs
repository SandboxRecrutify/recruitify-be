using Microsoft.AspNetCore.Authorization;

namespace Recrutify.Host.ProjectAuthorize
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
