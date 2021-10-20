using System;
using System.Collections.Generic;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Dtos
{
    public class CourseDto : CreateCourseDto
    {
        public Guid Id { get; set; }
    }
}
