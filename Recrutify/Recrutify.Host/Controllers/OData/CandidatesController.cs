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
            AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy
            | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [ODataAuthorize]
        [ODataRoute]
        public IQueryable<CandidateDTO> Get()
        {
            return _candidateService.Get();
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter
            | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top
            | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        //[ODataAuthorize]
        [HttpGet]
        public IEnumerable<CandidateDTO> ByProject([FromBody] ODataQueryOptions options, [FromQuery] Guid projectId)
        {
            var candidates = _candidateService.GetByProject(projectId);
            IEnumerable<CandidateDTO> candidatesFilter = (IEnumerable<CandidateDTO>)options.ApplyTo(candidates);
            var result = candidatesFilter.OrderByDescending(x => x.ProjectResults.FirstOrDefault(x => x.ProjectId == projectId)
                                        .Feedbacks.Where(x => x.Type != FeedbackTypeDTO.Test)
                                        .Sum(х => х.Rating))
                                   .ThenByDescending(x => x.ProjectResults.FirstOrDefault(x => x.ProjectId == projectId)
                                        .Feedbacks.FirstOrDefault(x => x.Type == FeedbackTypeDTO.Test)?.Rating);
            return result;
        }
    }
}
