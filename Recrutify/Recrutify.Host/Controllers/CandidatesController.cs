using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<CandidateDTO>> CreateAsync([FromBody] CandidateCreateDTO candidateCreateDTO, [FromQuery, Required] Guid projectId)
        {
            var result = await _candidateService.CreateAsync(candidateCreateDTO, projectId);
            return Created(string.Empty, result);
        }

        [Authorize(Policy = Constants.Policies.FeedbackPolicy)]
        [HttpPut("feedback")]
        public async Task<ActionResult> UpsertFeedbackAsync([FromQuery, Required] Guid id, [FromQuery, Required] Guid projectId, [FromBody] UpsertFeedbackDTO feedbackDto)
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
        public async Task<ActionResult<CandidateDTO>> GetCandidateWithProjectAsync([FromRoute] Guid id, [FromRoute] Guid projectId)
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

        [Authorize(Policy = Constants.Policies.HighAccessPolicy)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CandidateDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            var result = await _candidateService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Policy = Constants.Policies.FeedbackPolicy)]
        [HttpPut("bulk/test_feedbacks")]
        public async Task<ActionResult> BulkCreateTestFeedbacksAsync([FromBody] BulkCreateTestFeedbackDTO bulkCreateTestFeedbackDTO, [FromQuery, Required] Guid projectId)
        {
            await _candidateService.BulkCreateTestFeedbacksAsync(bulkCreateTestFeedbackDTO, projectId);
            return NoContent();
        }

        [Authorize(Policy = Constants.Policies.ManagerPolicy)]
        [HttpPut("bulk/update_status_reason")]
        public async Task<ActionResult> BulkUpdateStatusByIdsAsync([FromBody] BulkUpdateStatusDTO bulkUpdateStatusDTO, [FromQuery, Required] Guid projectId)
        {
            await _candidateService.BulkUpdateStatusReasonAsync(bulkUpdateStatusDTO, projectId);

            return NoContent();
        }
    }
}
