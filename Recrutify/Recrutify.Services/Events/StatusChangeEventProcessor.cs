using System.Threading.Tasks;
using Recrutify.Services.Events.Abstract;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Events
{
    public class StatusChangeEventProcessor
    {
        private readonly ICandidateService _candidateService;
        private readonly ISendEmailQueueService _sendQueueEmailService;
        private readonly IProjectService _projectService;
        private readonly IUpdateStatusEventPublisher _updateStatusEventPublisher;

        public StatusChangeEventProcessor(IProjectService projectService, ISendEmailQueueService sendQueueEmailService, ICandidateService candidateService, IUpdateStatusEventPublisher updateStatusEventPublisher)
        {
            _projectService = projectService;
            _sendQueueEmailService = sendQueueEmailService;
            _candidateService = candidateService;
            _updateStatusEventPublisher = updateStatusEventPublisher;
        }

        public void Subscribe()
        {
            _updateStatusEventPublisher.StatusCompleted += UpdateCandidatesStatusesAsync;
        }

        public async Task UpdateCandidatesStatusesAsync(UpdateStatusEventArgs e)
        {
            var candidates = await _candidateService.GetCandidatesDTOByIdsAsync(e.CandidatesIds);
            var project = await _projectService.GetAsync(e.ProjectId);
            _sendQueueEmailService.SendEmailQueueForStatusChange(candidates, e.CandidateStatus, project);
        }
    }
}
