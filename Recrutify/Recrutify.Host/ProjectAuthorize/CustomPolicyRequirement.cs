using Microsoft.AspNetCore.Authorization;

namespace Recrutify.Host.ProjectAuthorize
{
    public class CustomPolicyRequirement : IAuthorizationRequirement
    {
        public CustomPolicyRequirement(params string[] policy)
        {
            Policy = policy;
        }

        public string[] Policy { get; set; }
    }
}
