using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Host.Infrastructure.CustomsAuthorizationFilter;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

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
        [ODataAuthorize]
        [ODataRoute]
        public IQueryable<ProjectDTO> Get()
        {
            return _projectService.Get();
        }

        [EnableQuery(
            HandleNullPropagation = HandleNullPropagationOption.False,
            AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [ODataAuthorize(Policy = Constants.Policies.AllAccessPolicy)]
        public IEnumerable<ProjectDTO> GetAllProject(ODataQueryOptions<ProjectDTO> options)
        {
            var candidates = _projectService.Get();
            var filteredCandidates = options.ApplyTo(candidates) as IEnumerable<ProjectDTO>;
            var result = filteredCandidates!.Where(x => x.IsActive = true).Where(x => x.StartDate >= DateTime.Now)
                                            .OrderBy(x => x.StartDate);
            return result;
        }
    }
}
