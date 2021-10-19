using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.Servises.Abstract
{
    public interface ICourseService
    {
        public Task Creat(CourseDto courseDto);

        Task<List<CourseDto>> GetAllAsync();
    }
}
