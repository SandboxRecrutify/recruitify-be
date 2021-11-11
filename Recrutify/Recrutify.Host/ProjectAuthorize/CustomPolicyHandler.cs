using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Recrutify.Host.ProjectAuthorize
{
    public class CustomPolicyHandler : AuthorizationHandler<CustomPolicyRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor = null;

        public CustomPolicyHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomPolicyRequirement requirement)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            var user = context.User;
            var projectid = httpContext.Request.Query.FirstOrDefault(p => p.Key == Constants.Roles.ProjectId).Value;
            var projectRoles = user.Claims
                .Where(c => c.Type == Constants.Roles.ProjectRoles)
                .Select(c => JsonConvert.DeserializeObject<ParseJsonToDict>(c.Value))
                .ToDictionary(c => c.ProjectId, c => c.Roles);

            var rolesValue = projectRoles.SingleOrDefault(k => k.Key == Guid.Parse(projectid)).Value;
            if (rolesValue != null)
            {
                var flag = rolesValue.Where(v => requirement.Policy.Contains(v));

                if (flag != null)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.FromResult(0);
        }
    }
}