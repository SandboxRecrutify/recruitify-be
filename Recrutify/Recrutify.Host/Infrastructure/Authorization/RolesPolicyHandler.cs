using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Recrutify.DataAccess;

namespace Recrutify.Host.Infrastructure.Authorization
{
    public class RolesPolicyHandler : AuthorizationHandler<RolesPolicyRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RolesPolicyHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesPolicyRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = context.User;
            var projectId = httpContext.Request.Query.FirstOrDefault(p => p.Key == Constants.Roles.ProjectIdParam).Value;
            var projectRoles = user.Claims
                .Where(c => c.Type == Constants.Roles.Role)
                .Select(c => JsonConvert.DeserializeObject<ProjectRoles>(c.Value))
                .ToDictionary(c => c.ProjectId, c => c.Roles);

            if (projectId != StringValues.Empty
                && projectRoles.TryGetValue(Guid.Parse(projectId), out var globalRolesValue)
                && globalRolesValue.Any(r => requirement.Roles.Contains(r)))
            {
                context.Succeed(requirement);
            }
            else if (projectRoles.TryGetValue(GlobalID.GlobalProjectId, out var projectRolesValue)
                && projectRolesValue.Any(r => requirement.Roles.Contains(r)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}