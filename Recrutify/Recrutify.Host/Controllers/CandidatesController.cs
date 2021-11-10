﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;
using Recrutify.Services.Exceptions;
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

        // [Authorize(Policy = Constants.Constants.Policies.AllAccessPolicy)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<ActionResult<List<CandidateDTO>>> GetAsync()
        {
            var result = await _candidateService.GetAllAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> CreateAsync(CandidateCreateDTO candidateCreateDTO)
        {
            var result = await _candidateService.CreateAsync(candidateCreateDTO);
            return Created(string.Empty, result);
        }

        // [Authorize(Policy = Constants.Constants.Policies.FeedbackPolicy)]
        [HttpPut("feedback")]
        public async Task<ActionResult<CandidateDTO>> UpsertFeedbackAsync(Guid id, Guid projectId, CreateFeedbackDTO feedbackDto)
        {
            try
            {
                await _candidateService.UpsertFeedbackAsync(id, projectId, feedbackDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors.FirstOrDefault());
            }

            return NoContent();
        }

        [HttpGet("{id:guid}/{projectId:guid}")]
        public async Task<ActionResult<CandidateDTO>> UpsertAllFeedbackAsync(Guid id, Guid projectId)
        {
            try
            {
                var result = await _candidateService.GetCandidateWithProjectAsync(id, projectId);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // [Authorize(Policy = Constants.Constants.Policies.AllAccessPolicy)]
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
