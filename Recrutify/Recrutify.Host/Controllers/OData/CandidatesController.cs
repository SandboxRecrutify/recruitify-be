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
    [ODataRoutePrefix("Candidates")]
    public class CandidatesController : ODataController
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [EnableQuery(
            HandleNullPropagation = HandleNullPropagationOption.False,
            AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [ODataAuthorize(Policy = Constants.Policies.AdminPolicy)]
        public IQueryable<CandidateDTO> Get()
        {
            return _candidateService.Get();
        }

        [EnableQuery(
            HandleNullPropagation = HandleNullPropagationOption.False,
            AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [ODataAuthorize(Policy = Constants.Policies.AllAccessPolicy)]
        public IEnumerable<CandidateDTO> GetByProject(ODataQueryOptions<CandidateDTO> options, [FromQuery] Guid projectId)
        {
            var candidates = _candidateService.GetByProject(projectId);
            var filteredCandidates = options.ApplyTo(candidates) as IEnumerable<CandidateDTO>;
            var result = filteredCandidates!.OrderByDescending(x => x.ProjectResults
                                            ?.FirstOrDefault(x => x.ProjectId == projectId)
                                            ?.Feedbacks
                                            ?.Where(x => x.Type != FeedbackTypeDTO.Test)
                                            .Sum(х => х.Rating))
                                        .ThenByDescending(x => x.ProjectResults
                                            ?.FirstOrDefault(x => x.ProjectId == projectId)
                                            ?.Feedbacks
                                            ?.FirstOrDefault(x => x.Type == FeedbackTypeDTO.Test)
                                            ?.Rating);
            return result;
        }
    }
}
