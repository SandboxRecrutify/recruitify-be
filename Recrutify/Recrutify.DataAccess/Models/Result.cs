using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    class Result
    {
        public List<Feedback> Feetbacks { get; set; }
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }

    }
}
