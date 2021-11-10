using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Recrutify.Host.CustomAttribute
{
    public class ProjectAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var projectid = context.HttpContext.Request.Query.FirstOrDefault(p => p.Key == Constants.Roles.ProjectId).Value;

            var projectRoles = user.Claims
                .Where(c => c.Type == Constants.Roles.ProjectRoles)
                .Select(c => JsonConvert.DeserializeObject<ParseJsonToDict>(c.Value))
                .ToDictionary(c => c.ProjectId, c => c.Role);

            if (!projectRoles.ContainsKey(Guid.Parse(projectid)) || !user.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
