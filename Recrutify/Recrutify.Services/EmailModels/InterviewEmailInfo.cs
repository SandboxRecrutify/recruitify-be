using System;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.EmailModels
{
    public class InterviewEmailInfo
    {
        public UserShort User { get; set; }

        public CandidateShort Candidate { get; set; }

        public DateTime AppoitmentDateTime { get; set; }

        public InterviewType InterviewType { get; set; }
    }
}
