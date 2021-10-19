﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.Services.Dtos;

namespace Recrutify.Services.Servises.Abstract
{
    public interface ICourseService
    {
        public Task Creat(CourseDto courseDto);

        public Task<List<CourseDto>> GetAllAsync();
    }
}
