using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class Interview
    {
        public UserByEmail User { get; set; }

        public CandidateByEmail Candidate { get; set; }

        public DateTime AppointDateTime { get; set; }

        public string InterviewType { get; set; } ///enum??
    }
}
