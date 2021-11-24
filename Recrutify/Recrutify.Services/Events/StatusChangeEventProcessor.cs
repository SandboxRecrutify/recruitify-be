using System.Threading.Tasks;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.Events.Abstract;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Events
{
    public class StatusChangeEventProcessor
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISendEmailQueueService _sendQueueEmailService;
        private readonly IProjectService _projectService;
        private readonly IUpdateStatusEventPublisher _updateStatusEventPublisher;

        public StatusChangeEventProcessor(IProjectService projectService, ISendEmailQueueService sendQueueEmailService, ICandidateRepository candidateRepository, IUpdateStatusEventPublisher updateStatusEventPublisher)
        {
            _projectService = projectService;
            _sendQueueEmailService = sendQueueEmailService;
            _candidateRepository = candidateRepository;
            _updateStatusEventPublisher = updateStatusEventPublisher;
        }

        public void Subscribe()
        {
            _updateStatusEventPublisher.StatusCompleted += UpdateCandidatesStatusesAsync;
        }

        public async Task UpdateCandidatesStatusesAsync(UpdateStatusEventArgs e)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(e.CandidatesIds);
            var project = await _projectService.GetAsync(e.ProjectId);
            _sendQueueEmailService.SendEmailQueue(candidates, e.CandidateStatus, project);
        }
    }
}
