﻿using System;

namespace Recrutify.Services.EmailModels
{
    public class Interview
    {
        public UserEmailInfo User { get; set; }

        public CandidateEmailInfo Candidate { get; set; }

        public DateTime AppointDateTimeUtc { get; set; }

        public InterviewType InterviewType { get; set; }
    }
}
