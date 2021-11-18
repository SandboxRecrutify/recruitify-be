using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
            var projectIdIsInQuery = _httpContextAccessor.HttpContext.Request.Query.TryGetValue(Constants.Roles.ProjectIdParam, out var queryProjectId);
            var projectIdIsInRoute = _httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue(Constants.Roles.ProjectIdParam,  out var routeProjectId);
            var projectId = projectIdIsInQuery ? Guid.Parse(queryProjectId) : projectIdIsInRoute ? Guid.Parse(routeProjectId.ToString()) : DataAccess.Constants.GlobalProject.GlobalProjectId;

            var projectRoles = context.User.Claims
                .Where(c => c.Type == JwtClaimTypes.Role)
                .Select(c => JsonConvert.DeserializeObject<ProjectRoles>(c.Value))
                .ToDictionary(c => c.ProjectId, c => c.Roles);

            if (projectRoles.TryGetValue(projectId, out var globalRolesValue)
                    && globalRolesValue.Any(r => requirement.Roles.Contains(r)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}