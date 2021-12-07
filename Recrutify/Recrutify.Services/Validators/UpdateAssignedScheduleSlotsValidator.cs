using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class UpdateAssignedScheduleSlotsValidator : AbstractValidator<IEnumerable<InterviewDTO>>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public UpdateAssignedScheduleSlotsValidator(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;

            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(interviews => interviews)
                .NotNull()
                .NotEmpty()
                .MustAsync(InterviewsСheckAsync)
                .WithMessage("Slots are not editable.");
        }

        private async Task<bool> InterviewsСheckAsync(IEnumerable<InterviewDTO> interviews, CancellationToken cancellationToken)
        {
            var removedInterviews = interviews.Where(x => x.IsAppointment == false).ToList();
            if (removedInterviews.Any())
            {
                var dictionaryRemoved = removedInterviews.GroupBy(x => x.UserId).ToDictionary(k => k.Key, v => v.Select(b => _mapper.Map<ScheduleSlotShort>(b)).ToList());
                var scheduleShortsForRemovedInterviews = await _scheduleRepository.GetNotFreeShuduleSlotsBySlotsAsync(dictionaryRemoved);
                if (scheduleShortsForRemovedInterviews.Sum(x => x.ScheduleSlots.Count()) != removedInterviews.Count())
                {
                    return false;
                }
            }

            var addedInterviews = interviews
                        .Where(
                            x => x.IsAppointment == true
                            && !removedInterviews.Any(
                                y => y.UserId == x.UserId
                                && y.AppoitmentDateTime == x.AppoitmentDateTime))
                        .GroupBy(x => x.UserId)
                        .ToDictionary(k => k.Key, v => v.Select(b => _mapper.Map<ScheduleSlotShort>(b)).ToList());
            if (addedInterviews.Any())
            {
                var scheduleShortsForAddedInterviews = await _scheduleRepository.GetFreeShuduleSlotsBySlotsAsync(addedInterviews);
                if (scheduleShortsForAddedInterviews.Sum(x => x.ScheduleSlots.Count()) != addedInterviews.Count())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
