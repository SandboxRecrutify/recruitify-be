using System.Collections.Generic;
using AutoMapper;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class StatusChangeEventHandler : IStatusChangeEventHandler
    {
        private readonly ICandidateRepository _candidateRopository;
        private readonly ISendQueueEmailService _sendQueueEmailService;
        private readonly IMapper _mapper;

        public StatusChangeEventHandler(ICandidateRepository candidateRepository, ISendQueueEmailService sendQueueEmailService, IMapper mapper)
        {
            _candidateRopository = candidateRepository;
            _mapper = mapper;
            _sendQueueEmailService = sendQueueEmailService;
        }

        public event SaveDetailsHandler UpdateStatusByIdsAsyncComlited;

        public async void UpdateStatusComplited(object sender, SaveArgsDTO e)
        {
            var candidates = await _candidateRopository.GetByIdsAsync(e.Ids);
            await _sendQueueEmailService.SendEmail(_mapper.Map<List<CandidateDTO>>(candidates), e.Status);
        }

        public void OnUpdateStatusByIdsAsyncComlited(SaveArgsDTO e)
        {
            UpdateStatusByIdsAsyncComlited?.Invoke(this, e);
        }
    }
}
