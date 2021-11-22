using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<AssignedCandidateDTO> GetNewCandidateByProject(ODataQueryOptions<AssignedCandidateDTO> options, [FromQuery] Guid projectId)
        {
            var candidates = _candidateService.GetAssignedCandidateByProject(projectId);
            var filteredCandidates = options.ApplyTo(candidates) as IEnumerable<AssignedCandidateDTO>;
            var result = filteredCandidates?.Where(x => x.ProjectResults.Where(x => x.ProjectId == projectId)
                                            .Select(x => x.Status == StatusDTO.New).FirstOrDefault());
            return result;
        }

        [EnableQuery(
           HandleNullPropagation = HandleNullPropagationOption.False,
           AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        public IEnumerable<AssignedCandidateDTO> GetAssignedCandidateByProject(ODataQueryOptions<AssignedCandidateDTO> options, [FromQuery] Guid projectId)
        {
            var candidates = _candidateService.GetAssignedCandidateByProject(projectId);
            var filteredCandidates = options.ApplyTo(candidates) as IEnumerable<AssignedCandidateDTO>;
            var result = filteredCandidates?.Where(x => x.ProjectResults.Where(x => x.ProjectId == projectId && !x.IsAssigned)
                                           .Select(x => x.Status == StatusDTO.RecruiterInterview || x.Status == StatusDTO.TechInterviewOneStep || x.Status == StatusDTO.TechInterviewSecondStep).FirstOrDefault());
            return result;
        }
    }
}
