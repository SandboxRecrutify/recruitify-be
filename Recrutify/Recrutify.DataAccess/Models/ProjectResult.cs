using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public class ProjectResult
    {
        public List<Feedback> Feedbacks { get; set; }

        public Guid ProjectId { get; set; }

        public Status Status { get; set; }

        public string Cause { get; set; }
    }
}
