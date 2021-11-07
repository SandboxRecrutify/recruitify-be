using System;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Recrutify.Host.Controllers.OData
{
    [ODataRoutePrefix("Projects")]
    public class ProjectsController : ODataController
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter
            | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top
            | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[MyAuthorize]
        [ODataRoute]
        public IQueryable<ProjectDTO> Get()
        {
            return _projectService.Get();
        }
    }

    public class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}
