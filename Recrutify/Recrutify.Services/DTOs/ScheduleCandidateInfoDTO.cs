using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class ScheduleCandidateInfoDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public IEnumerable<int> BestTimeToConnect { get; set; }

        public string Email { get; set; }

        public ScheduleCandidateProjectResultDTO ProjectResult { get; set; }
    }
}
