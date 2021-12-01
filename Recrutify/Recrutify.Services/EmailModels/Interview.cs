using System;

namespace Recrutify.Services.EmailModels
{
    public class Interview
    {
        public UserByEmail User { get; set; }

        public CandidateByEmail Candidate { get; set; }

        public DateTime AppointDateTimeUtc { get; set; }

        public InterviewType InterviewType { get; set; }
    }
}
