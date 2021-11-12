using Microsoft.AspNetCore.Authorization;

namespace Recrutify.Host.ProjectAuthorize
{
    public class ProjectRolesPolicyRequirement : IAuthorizationRequirement
    {
        public ProjectRolesPolicyRequirement(params string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; set; }
    }
}
