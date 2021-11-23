using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Host.Infrastructure.CustomsAuthorizationFilter;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers.OData
{
    [ODataRoutePrefix("Schedule")]
    public class ScheduleController : ODataController
    {
        private readonly ICandidateService _candidateService;

        public ScheduleController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [EnableQuery(
          HandleNullPropagation = HandleNullPropagationOption.False,
          AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [ODataAuthorize(Policy = Constants.Policies.RecruiterPolicy)]
        public IEnumerable<AssignedCandidateDTO> GetNewCandidatesSlots(ODataQueryOptions<AssignedCandidateDTO> options, [FromQuery] Guid projectId)
        {
            var candidates = _candidateService.GetNewCandidateByProject(projectId);
            return candidates;
        }

        [EnableQuery(
           HandleNullPropagation = HandleNullPropagationOption.False,
           AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [ODataAuthorize(Policy = Constants.Policies.RecruiterPolicy)]
        public IEnumerable<AssignedCandidateDTO> GetUnassignedCandidatesSlots(ODataQueryOptions<AssignedCandidateDTO> options, [FromQuery] Guid projectId)
        {
            var candidates = _candidateService.GetUnassignedCandidateByProject(projectId);
            return candidates;
        }
    }
}
