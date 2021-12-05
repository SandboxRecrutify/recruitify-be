﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class Interview
    {
        public Guid UserId { get; set; }

        public Guid CandidateId { get; set; }

        public DateTime AppointDateTimeUtc { get; set; }

        public bool IsAppointment { get; set; }
    }
}
