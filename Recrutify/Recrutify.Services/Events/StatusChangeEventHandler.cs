using System.Threading.Tasks;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.Events.Abstract;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Events
{
    public class StatusChangeEventHandler : IStatusChangeEventHandler
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISendQueueEmailService _sendQueueEmailService;
        private readonly IProjectRepository _projectRepository;
        private readonly IUpdateStatusEventProcessor _updateStatusEvent;

        public StatusChangeEventHandler(IProjectRepository projectRepository, ISendQueueEmailService sendQueueEmailService, ICandidateRepository candidateRepository, IUpdateStatusEventProcessor updateStatusEvent)
        {
            _projectRepository = projectRepository;
            _sendQueueEmailService = sendQueueEmailService;
            _candidateRepository = candidateRepository;
            _updateStatusEvent = updateStatusEvent;
            _updateStatusEvent.UpdateStatusByIdsAsyncComlited += async (e) => await UpdateCandidatesStatusesAsync(e);
        }

        public async Task UpdateCandidatesStatusesAsync(UpdateStatusEventArgs e)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(e.Ids);
            var project = await _projectRepository.GetAsync(e.ProjectId);
            _sendQueueEmailService.SendEmail(candidates, e.Status, project);
        }
    }
}