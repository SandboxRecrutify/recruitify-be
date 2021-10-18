using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Repositories.IRepository
{
    public interface ICourseRepository
    {
        public Course Create(Course course);

        public List<Course> ReadAll();
    }
}
