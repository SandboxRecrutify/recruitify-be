using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.DTOs
{
    public class InterviewDTO
    {
        public Guid UserId { get; set; }

        public Guid CandidateId { get; set; }

        public DateTime AppoitmentDateTime { get; set; }

        public bool IsAppointment { get; set; }
    }
}
