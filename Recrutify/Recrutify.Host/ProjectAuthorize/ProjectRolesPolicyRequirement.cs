using Microsoft.AspNetCore.Authorization;

namespace Recrutify.Host.ProjectAuthorize
{
    public class ProjectRolesPolicyRequirement : IAuthorizationRequirement
    {
        public ProjectRolesPolicyRequirement(params string[] policy)
        {
            Policy = policy;
        }

        public string[] Policy { get; set; }
    }
}
