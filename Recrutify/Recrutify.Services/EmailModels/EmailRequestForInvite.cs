using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.EmailModels
{
    public class EmailRequestForInvite : EmailRequest
    {
        public string InterviewType { get; set; }

        public DateTime DateTimeInterview { get; set; }
    }
}
