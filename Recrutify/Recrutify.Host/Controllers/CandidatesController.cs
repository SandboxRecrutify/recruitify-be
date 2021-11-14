using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutify.DataAccess;
using Recrutify.Services.DTOs;
using Recrutify.Services.Exceptions;
using Recrutify.Services.Services.Abstract;
using ValidationException = FluentValidation.ValidationException;

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

        [ApiExplorerSettings(IgnoreApi = true)]
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

        [Authorize(Policy = Constants.Policies.FeedbackPolicy)]
        [HttpPut("feedback")]
        public async Task<ActionResult> UpsertFeedbackAsync(Guid id, Guid projectId, UpsertFeedbackDTO feedbackDto)
        {
            try
            {
                await _candidateService.UpsertFeedbackAsync(id, projectId, feedbackDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(new ValidationProblemDetails(
                      ex.Errors
                       .GroupBy(o => o.PropertyName)
                       .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray())));
            }

            return NoContent();
        }

        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
        [HttpGet("{id:guid}/{projectId:guid}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidateWithProjectAsync(Guid id, Guid projectId)
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
