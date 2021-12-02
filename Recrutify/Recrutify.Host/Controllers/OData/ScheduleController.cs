﻿using System;
using System.Collections.Generic;
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
        public IEnumerable<ScheduleCandidateInfoDTO> GetCandidatesPassedTest([FromQuery] Guid projectId)
        {
            return _candidateService.GetCandidatesPassedTestSlots(projectId);
        }

        [EnableQuery(
           HandleNullPropagation = HandleNullPropagationOption.False,
           AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [ODataAuthorize(Policy = Constants.Policies.RecruiterPolicy)]
        public IEnumerable<ScheduleCandidateInfoDTO> GetUnassignedCandidates([FromQuery] Guid projectId)
        {
            return _candidateService.GetUnassignedCandidatesSlots(projectId);
        }
    }
}
