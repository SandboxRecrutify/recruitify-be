using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class CourseResult
    {
        public List<Feedback> Feedbacks { get; set; }

        public Guid CourseId { get; set; }

        public Status Status { get; set; }
    }
}
