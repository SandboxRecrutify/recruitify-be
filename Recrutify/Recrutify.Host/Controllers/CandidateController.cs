﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [Authorize(Policy = Constants.Constants.Policies.AllAccessPolicy)]
        [HttpGet]
        public async Task<ActionResult<List<CandidateDTO>>> GetAsync()
        {
            var result = await _candidateService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> CreateAsync(CandidateCreateDTO candidateCreateDTO)
        {
            var result = await _candidateService.CreateAsync(candidateCreateDTO);
            return Created(string.Empty, result);
        }

        [Authorize(Policy = Constants.Constants.Policies.FeedbackPolicy)]
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

        [Authorize(Policy = Constants.Constants.Policies.AllAccessPolicy)]
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
    }
}
