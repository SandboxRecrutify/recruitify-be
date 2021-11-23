using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Events.Abstract;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Events
{
    public class StatusChangeEventHandler : IStatusChangeEventHandler
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISendQueueEmailService _sendQueueEmailService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly IUpdateStatusEventArgs _updateStatusEvent;

        public StatusChangeEventHandler(IProjectService projectService, ISendQueueEmailService sendQueueEmailService, ICandidateRepository candidateRepository, IMapper mapper, IUpdateStatusEventArgs updateStatusEvent)
        {
            _projectService = projectService;
            _sendQueueEmailService = sendQueueEmailService;
            _candidateRepository = candidateRepository;
            _mapper = mapper;
            _updateStatusEvent = updateStatusEvent;
            _updateStatusEvent.UpdateStatusByIdsAsyncComlited += async (e) => await UpdateCandidatesStatusesAsync(e);
        }

        public async Task UpdateCandidatesStatusesAsync(UpdateStatusEventArgs e)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(e.Ids);
            var project = await _projectService.GetAsync(e.ProjectId);
            await _sendQueueEmailService.SendEmail(_mapper.Map<List<CandidateDTO>>(candidates), e.Status, project);
        }
    }
}