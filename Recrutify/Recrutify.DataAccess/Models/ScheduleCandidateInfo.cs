using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class ScheduleCandidateInfo
    {
        public IEnumerable<int> BestTimeToConnect { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public ScheduleCandidateProjectResult ProjectResult { get; set; }
    }
}
