using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Recrutify.Host.Infrastructure;

namespace Recrutify.Host.ProjectAuthorize
{
    public class ProjectRolesPolicyHandler : AuthorizationHandler<ProjectRolesPolicyRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectRolesPolicyHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectRolesPolicyRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = context.User;
            var projectId = httpContext.Request.Query.FirstOrDefault(p => p.Key == Constants.Roles.ProjectId).Value;
            var projectRoles = user.Claims
                .Where(c => c.Type == Constants.Roles.ProjectRoles)
                .Select(c => JsonConvert.DeserializeObject<ProjectRoles>(c.Value))
                .ToDictionary(c => c.ProjectId, c => c.Roles);

            if (projectRoles.TryGetValue(Guid.Parse(projectId), out var rolesValue) && rolesValue.Any(v => requirement.Roles.Contains(v)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}