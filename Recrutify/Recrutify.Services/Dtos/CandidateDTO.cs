using System;

namespace Recrutify.Services.Dtos
{
    public class CandidateDTO : CandidateCreateDTO
    {
        public Guid Id { get; set; }
    }
}
