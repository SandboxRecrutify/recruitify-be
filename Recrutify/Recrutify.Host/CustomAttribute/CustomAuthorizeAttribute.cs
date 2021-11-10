using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Recrutify.Host.CustomAttribute
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var projectid = context.HttpContext.Request.Query.FirstOrDefault(p => p.Key == "projectId").Value;
            Dictionary<Guid, IEnumerable<string>> userReq = new Dictionary<Guid, IEnumerable<string>>();

            foreach (var role in user.Claims)
            {
                if (role.Type == "projectroles")
                {
                    var values = JsonConvert.DeserializeObject<ParseJsonToDict>(role.Value);
                    userReq.Add(values.ProjectId, values.Role);
                }
            }

            if (!userReq.Any(p => p.Key.ToString() == projectid) || !user.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
