﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<ActionResult<List<CandidateDTO>>> GetAsync()
        {
            var result = await _candidateService.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> CreateAsync(CandidateCreateDTO candidateCreateDTO)
        {
            var result = await _candidateService.CreateAsync(candidateCreateDTO);
            return Created(string.Empty, result);
        }

        [Authorize(Policy = Constants.Policies.FeedbackPolicy)]
        [HttpPut]
        public async Task<ActionResult> UpsertFeedbackAsync(Guid id, Guid projectId, CreateFeedbackDTO feedbackDto)
        {
            var candidateExist = await _candidateService.ExistsAsync(id);
            if (!candidateExist)
            {
                return NotFound();
            }

            await _candidateService.UpsertFeedbackAsync(id, projectId, feedbackDto);
            return NoContent();
        }

        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CandidateDTO>> GetByIdAsync(Guid id)
        {
            var result = await _candidateService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
        [HttpGet("all/{projectId:guid}")]
        public async Task<ActionResult<CandidateDTO>> GetByPrjectAsync(Guid projectId)
        {
            var candidates = await _candidateService.GetByProjectAsync(projectId);
            var result = candidates.OrderByDescending(x => x.ProjectResults.FirstOrDefault(x => x.ProjectId == projectId)
                                        .Feedbacks.Where(x => x.Type != FeedbackTypeDTO.Test)
                                        .Sum(х => х.Rating))
                                   .ThenByDescending(x => x.ProjectResults.FirstOrDefault(x => x.ProjectId == projectId)
                                        .Feedbacks.FirstOrDefault(x => x.Type == FeedbackTypeDTO.Test)?.Rating);
            return Ok(result);
        }
    }
}
