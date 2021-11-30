using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class BulkAppointInterviewsValidator : AbstractValidator<List<BulkAppointInterviewsDTO>>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public BulkAppointInterviewsValidator(ICandidateRepository candidateRepository, IScheduleRepository scheduleRepository)
        {
            _candidateRepository = candidateRepository;
            _scheduleRepository = scheduleRepository;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(x => x.Select(c => c.CandidateId))
                .NotNull()
                .NotEmpty()
                .MustAsync(CandidatesAreExistingAsync)
                .WithMessage("One or more candidates doesn't exist");
            RuleFor(x => x)
                .MustAsync(ScheduleSlotsOrFreeAndExistingAsync)
                .WithMessage("Schedule slots aren't free or doesn't existing");
        }

        private async Task<bool> CandidatesAreExistingAsync(IEnumerable<Guid> candidatsIds, CancellationToken cancellationToken)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(candidatsIds);
            var filteredCandidatesCount = candidates.Count(c => c.ProjectResults
                                                       ?.Any(p => p.Status == Status.TechInterviewOneStep) ?? false);
            return filteredCandidatesCount == candidatsIds.Count();
        }

        private async Task<bool> ScheduleSlotsOrFreeAndExistingAsync(List<BulkAppointInterviewsDTO> dtos, CancellationToken cancellationToken)
        {
            var dict = dtos.ToDictionary(k => k.UserId, v => v.AppointDateTime);

            return await _scheduleRepository.FreeOrExistByDictAsync(dict);
        }
    }
}
